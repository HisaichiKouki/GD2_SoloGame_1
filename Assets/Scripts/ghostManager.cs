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

    float curMoveTime;
    float curmaxStandbyTime;
    float curStandbyTime;
    int curHitPoint;
    int score;
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



    List<ghostScript> ghosts = new List<ghostScript>();
    bool nextWaveFlag;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("GameScene");
        }

        if (curHitPoint<=0) { return;}
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
        debugText += "ghosts.Count=" + ghosts.Count + "\ncurMoveTime=" + curMoveTime + "\nTime.timeScale" + Time.timeScale;
    }

    void GhostInit()
    {
        if (!nextWaveFlag) { return; }
        nextWaveFlag = false;
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
        if (curMoveTime > 0) { return; }
        if (Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetKeyDown(KeyCode.A))
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
        else if (Input.GetKeyDown(KeyCode.RightArrow)|| Input.GetKeyDown(KeyCode.D))
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

    bool Discrimination(Ghost_Type type)
    {
        if (ghosts[0].GetType() == type)
        {
            Debug.Log("True");
            score++;
            scoreText.SetText(score);
            return true;
        }
        else
        {
            Debug.Log("False");
            Damage();
            return false;
        }
    }
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
    void DestroyObj()
    {
        // Destroy(ghosts[0].gameObject);
        ghosts.RemoveAt(0);
        //Debug.Log("After removal: " + string.Join(", ", ghosts));
    }

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

    void EvaluationAnime(Ghost_Type type)
    {
        if (Discrimination(type))
        {
            Instantiate(goodText);
        }
        else
        {
            Instantiate(badText);
        }
    }

    void GaugeChange()
    {
        if (curMoveTime > 0) { return; }
        curStandbyTime -= Time.deltaTime;
        remainingTimeGauge.SetCurrentValue(curStandbyTime);
        if (curStandbyTime <= 0)
        {
            Damage();
            curStandbyTime = curmaxStandbyTime;
        }
    }

}
