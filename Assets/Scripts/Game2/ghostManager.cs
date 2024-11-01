using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class ghostManager : MonoBehaviour
{
    [Header("パラメーター")]
    [SerializeField] float addGameTime;//ゲーム時間が加速する値
    [SerializeField] int initNum;//初期生成数
    [SerializeField] int addGhostNum;//次の生成で増える数
    [SerializeField] float interval;//列の間隔
    [SerializeField] float maxStandbyTime;//最大猶予時間
    [SerializeField] float initMoveTime;
    [SerializeField] int maxHitPoint;
    [SerializeField] float waveStandbyTime;

    float curMoveTime;
    float curmaxStandbyTime;
    float curStandbyTime;
    float curWaveStandbyTime;
    int curHitPoint;
    int score;
    int curWaveCount;
    [Header("プレハブ")]
    [SerializeField] GameObject ghostSpownPoint;
    [SerializeField] GameObject goodText;
    [SerializeField] GameObject badText;
    [SerializeField] GameObject damageText;
    [SerializeField] GameObject gameOverText;
    [SerializeField] ghostScript ghostPrefab;
    [SerializeField] GaugeScript remainingTimeGauge;
    [SerializeField] GaugeScript hitPointGauge;
    [SerializeField] SetTextScript scoreText;
    [SerializeField] AnimationReset scoreTextResetAnime;
    [SerializeField] GameObject nextWaveTextPrefab;
    [SerializeField] GameObject goToHevenPrefab;
    [SerializeField] ShakeScript cameraShakeScript;



    List<ghostScript> ghosts = new List<ghostScript>();
    bool nextWaveFlag;
    bool spawnNextWaveText;

    public string debugText;
    // Start is called before the first frame update
    void Start()
    {
        nextWaveFlag = true;
        //猶予時間の初期化
        GhostInit();
        //最初は1にする
        Time.timeScale = 1;

        curHitPoint = maxHitPoint;
        hitPointGauge.SetMaxValue(maxHitPoint);
        hitPointGauge.SetCurrentValue(curHitPoint);
        score = 0;
        scoreText.SetText(score);

        curWaveStandbyTime = waveStandbyTime;
        curWaveCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("GameScene");
        }

        if (curHitPoint <= 0) { return; }
        debugText = "";
        //ゴーストがいなくなって移動カウントも終わった時に次のフェーズへ行く
        if (curMoveTime <= 0 && ghosts.Count <= 0)
        {
            nextWaveFlag = true;
        }
        Sorting();
        ProgressMove();
        GaugeChange();
        GhostInit();
        WaveStandby();
        debugText += "ghosts.Count=" + ghosts.Count + "\ncurMoveTime=" + curMoveTime + "\nTime.timeScale" + Time.timeScale;
    }

    //初期化
    void GhostInit()
    {
        if (!nextWaveFlag) { return; }
        if (curWaveStandbyTime > 0) { return; }
        nextWaveFlag = false;
        curWaveStandbyTime = waveStandbyTime;
        spawnNextWaveText = false;
        for (int i = 0; i < initNum; i++)
        {
            ghostScript ghost = Instantiate(ghostPrefab);
            ghost.transform.position = new Vector3(0, i * interval, 0);
            ghost.transform.parent = ghostSpownPoint.transform;
            ghosts.Add(ghost);
        }
        initNum += addGhostNum;
        //猶予時間の初期化
        RemainginTimeinit();

        //ゲーム時間の加速
        AddTimeRatio();
    }
    //猶予時間の初期化
    void RemainginTimeinit()
    {

        curmaxStandbyTime = maxStandbyTime;
        curStandbyTime = curmaxStandbyTime;
        remainingTimeGauge.SetMaxValue(curmaxStandbyTime);
    }
    //ゲーム内時間を加速する
    void AddTimeRatio()
    {
        Time.timeScale += addGameTime;
    }

    //仕分けの操作
    void Sorting()
    {
        if (nextWaveFlag) { return; }
        if (curMoveTime > 0) { return; }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (ghosts.Count > 0)
            {
                ghosts[0].SetAnimetion("GoHeaven");
                EvaluationAnime(Ghost_Type.NORMAL);
                DestroyObj();
                curMoveTime = initMoveTime;
                curStandbyTime = curmaxStandbyTime;

               
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (ghosts.Count > 0)
            {
                ghosts[0].SetAnimetion("GoHell");
                EvaluationAnime(Ghost_Type.EVIL);
                DestroyObj();
                curMoveTime = initMoveTime;
                curStandbyTime = curmaxStandbyTime;
            }
        }
    }
    //判定処理
    bool Discrimination(Ghost_Type type)
    {
        if (ghosts[0].GetType() == type)
        {
            Debug.Log("True");
            score++;
            scoreText.SetText(score);
            scoreTextResetAnime.ResetAnime();
            return true;
        }
        else
        {
            Debug.Log("False");
            Damage();
            return false;
        }
    }
    //ミスした時のダメージ
    void Damage()
    {
        curHitPoint--;
        hitPointGauge.SetCurrentValue(curHitPoint);
        Instantiate(damageText);
        if (curHitPoint <= 0)
        {
            Instantiate(gameOverText);
        }
    }
    //判定が終わったらリストから削除する
    void DestroyObj()
    {
        // Destroy(ghosts[0].gameObject);
        ghosts.RemoveAt(0);
        //Debug.Log("After removal: " + string.Join(", ", ghosts));
    }
    //列を前に詰める処理
    void ProgressMove()
    {
        if (curMoveTime <= 0) { return; }
        curMoveTime -= Time.deltaTime;
        float easeT = curMoveTime / initMoveTime;
        //列を前に詰める
        for (int i = 0; i < ghosts.Count; i++)
        {
            Vector3 newPos = ghosts[i].transform.position;
            newPos.y = Mathf.Lerp(i * interval, (i + 1) * interval, easeT);
            ghosts[i].transform.position = newPos;

        }

    }
    //正誤の演出
    void EvaluationAnime(Ghost_Type type)
    {
        //正しいとき
        if (Discrimination(type))
        {
            Instantiate(goodText);
        }
        else
        {
            //間違えたとき
            cameraShakeScript.ShakeStart();
            Instantiate(badText);
        }
    }
    //止まってるときの猶予時間
    void GaugeChange()
    {
        if (nextWaveFlag) { return; }
        if (curMoveTime > 0) { return; }
        curStandbyTime -= Time.deltaTime;
        remainingTimeGauge.SetCurrentValue(curStandbyTime);
        if (curStandbyTime <= 0)
        {
            Damage();
            curStandbyTime = curmaxStandbyTime;
        }
    }
    //ウェーブ開始時の処理
    void WaveStandby()
    {
        if (!nextWaveFlag) { return; }
        if (!spawnNextWaveText)
        {
            GameObject nextWaveText=Instantiate(nextWaveTextPrefab);
            nextWaveText.transform.GetChild(0).GetComponent<SetTextScript>().SetText(curWaveCount);
            curWaveCount++;
            spawnNextWaveText = true;
        }
        curWaveStandbyTime -= Time.deltaTime;
    }

}
