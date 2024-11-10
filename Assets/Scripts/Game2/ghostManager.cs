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
    [SerializeField] float parfectRatio;
    [SerializeField] float goodRatio;
    [SerializeField] float totalTimeScaleLerpTime;
    [SerializeField] float bonusTimeRatio;

    float curMoveTime;
    float curmaxStandbyTime;
    float curStandbyTime;
    float curWaveStandbyTime;
    float curTimeScaleLerpTime;
    float preTimeScale;
    int curHitPoint;
    int score;
    int curWaveCount;
    int curCombo;
    int maxCombo;
    [Header("プレハブ")]
    [SerializeField] GameObject ghostSpownPoint;
    [SerializeField] GameObject goodText;
    [SerializeField] GameObject niceText;
    [SerializeField] GameObject parfectText;
    [SerializeField] GameObject badText;
    [SerializeField] GameObject damageText;
    [SerializeField] GameObject gameOverText;
    [SerializeField] ghostScript ghostPrefab;
    [SerializeField] GaugeScript remainingTimeGauge;
   // [SerializeField] GaugeScript hitPointGauge;
    [SerializeField] SetTextScript scoreText;
    [SerializeField] AnimationReset scoreTextResetAnime;
    [SerializeField] GameObject nextWaveTextPrefab;
    [SerializeField] GameObject goToHevenPrefab;
    [SerializeField] ShakeScript cameraShakeScript;
    [SerializeField] GameObject evalutionCharaSpownPoitnt;
    [SerializeField] GameObject[] evalutionCharas;
    [SerializeField] LerpNumber totalNumText;
    [SerializeField] LerpNumber maxComboText;
    [SerializeField] Animator handAnime;
    [SerializeField] HitPointManager hitPoints;

    //[SerializeField] ShakeScript charsShake;
    AudioPlay audioPlay;


    List<ghostScript> ghosts = new List<ghostScript>();
    bool nextWaveFlag;
    bool spawnNextWaveText;
    bool ghostInitFlag;
    bool missPlaying;
    bool preSuccesFlag;
    bool curSuccesFlag;
    public string debugText;
    bool gameOverFlag;


    // Start is called before the first frame update
    void Start()
    {
        //猶予時間の初期化
        GhostInit();
        //最初は1にする
        Time.timeScale = 1;

        curHitPoint = maxHitPoint;
        //hitPointGauge.SetMaxValue(maxHitPoint);
       // hitPointGauge.SetCurrentValue(curHitPoint);
        score = 0;
        scoreText.SetText(score);

        curWaveStandbyTime = waveStandbyTime;
        curWaveCount = 0;
        nextWaveFlag = false;

        curmaxStandbyTime = maxStandbyTime;
        audioPlay = GetComponent<AudioPlay>();
    }

    // Update is called once per frame
    void Update()
    {
        missPlaying = false;//ミスした時のフラグを最初にfalseにしておく
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("GameScene");
        }

        if (curHitPoint <= 0)
        {
            //GameOver時のTimeScaleの減衰
            AttenuationTimeScale();
            return;
        }
        debugText = "";
        //ゴーストがいなくなって移動カウントも終わった時に次のフェーズへ行く
        if (curMoveTime <= 0 && ghosts.Count <= 0)
        {
            nextWaveFlag = true;
        }
        Sorting();
        ProgressMove();
        GaugeChange();
        //GhostInit();
        WaveStandby();
        debugText += GetStandbyTimeRatio();
    }

    //初期化
    void GhostInit()
    {
        //if (!nextWaveFlag) { return; }
        //if (curWaveStandbyTime > 0) { return; }
        //nextWaveFlag = false;
        // curWaveStandbyTime = waveStandbyTime;
        //spawnNextWaveText = false;
        for (int i = 0; i < initNum; i++)
        {
            ghostScript ghost = Instantiate(ghostPrefab);
            ghost.transform.position = new Vector3(0, i * interval, 0);
            ghost.transform.parent = ghostSpownPoint.transform;
            ghosts.Add(ghost);
        }
        initNum += addGhostNum;
        //猶予時間の初期化
        RemainginTimeinit(1);

        //ゲーム時間の加速
        AddTimeRatio();
    }
    //猶予時間の初期化
    void RemainginTimeinit(float value)
    {

        curmaxStandbyTime = maxStandbyTime * value;
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
        if (curMoveTime > 0) { return; }//移動処理をしていたらreturn
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (ghosts.Count > 0)
            {
                ghosts[0].SetAnimetion("GoHeaven");
                // curStandbyTime = curmaxStandbyTime;
                EvaluationAnime(Ghost_Type.NORMAL);
                DestroyObj();
                curMoveTime = initMoveTime;
                remainingTimeGauge.SetMaxValue(curStandbyTime);
                audioPlay.SE1();

                handAnime.SetInteger("State", 1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (ghosts.Count > 0)
            {
                ghosts[0].SetAnimetion("GoHell");
                //curStandbyTime = curmaxStandbyTime;
                EvaluationAnime(Ghost_Type.EVIL);
                DestroyObj();
                curMoveTime = initMoveTime;
                remainingTimeGauge.SetMaxValue(curStandbyTime);
                audioPlay.SE1();
                handAnime.SetInteger("State", 2);
            }
        }
    }
    //判定処理だけ
    bool Discrimination(Ghost_Type type)
    {
        if (ghosts[0].GetType() == type)
        {

            return true;
        }
        else
        {

            return false;
        }
    }
    //ミスした時のダメージ
    void Damage()
    {
        curHitPoint--;
        hitPoints.Damage();
        cameraShakeScript.ShakeStart();
        //hitPointGauge.SetCurrentValue(curHitPoint);
        Instantiate(damageText);
        if (curHitPoint <= 0)
        {
            // Instantiate(gameOverText);
            gameOverFlag = true;
            preTimeScale = Time.timeScale;
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
        if (curMoveTime <= 0)
        {
            //移動処理が終わったら手を待機状態に
            handAnime.SetInteger("State", 0);
            return;
        }
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
        preSuccesFlag = curSuccesFlag;

        //正しいとき
        if (Discrimination(type))
        { //Debug.Log("True");
            score++;
            scoreText.SetText(score);
            scoreTextResetAnime.ResetAnime();
            curSuccesFlag = true;

            //preSuccesFlagcurSuccesFlag==Trueの時に
            //コンボが継続する演出
            if (preSuccesFlag && curSuccesFlag)
            {
                curCombo++;
            }
            //maxComboの更新
            if (curCombo > maxCombo)
            {
                maxCombo = curCombo;
            }
            //Debug.Log("CurConbo=" + curCombo);

            //ゲージの割合によって表示する文字を変える

            float standbyTimeRatio = GetStandbyTimeRatio();
            if (standbyTimeRatio > parfectRatio)
            {
                Instantiate(parfectText);
                Instantiate(evalutionCharas[0], evalutionCharaSpownPoitnt.transform);
            }
            else if (standbyTimeRatio > goodRatio)
            {
                Instantiate(goodText);
                Instantiate(evalutionCharas[1], evalutionCharaSpownPoitnt.transform);
            }
            else
            {
                Instantiate(niceText);
                Instantiate(evalutionCharas[2], evalutionCharaSpownPoitnt.transform);
            }

            //猶予時間の初期化は評価の文字を出した後
            RemainginTimeinit(1);
        }
        else
        {
            //間違えたとき
            //Debug.Log("False");
            Damage();
            curSuccesFlag = false;
            missPlaying = true;
            curCombo = 0;
            
            //charsShake.ShakeStart();
            Instantiate(badText);
            //curStandbyTime = curmaxStandbyTime * bonusTimeRatio;
            //remainingTimeGauge.SetMaxValue(curStandbyTime);
            RemainginTimeinit(bonusTimeRatio);
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
            //curStandbyTime = curmaxStandbyTime * bonusTimeRatio;
            //remainingTimeGauge.SetMaxValue(curStandbyTime);
            RemainginTimeinit(bonusTimeRatio);
            missPlaying = true;
            handAnime.SetInteger("State", 0);
        }
    }
    //ウェーブ開始時の処理
    void WaveStandby()
    {
        if (!nextWaveFlag) { return; }
        if (!spawnNextWaveText)
        {
            GameObject nextWaveText = Instantiate(nextWaveTextPrefab);
            nextWaveText.transform.GetChild(0).GetComponent<SetTextScript>().SetText(curWaveCount);
            curWaveCount++;
            spawnNextWaveText = true;
            ghostInitFlag = false;
        }
        curWaveStandbyTime -= Time.deltaTime;
        //カウントダウンが始まるタイミングでゴーストを出す
        if (!ghostInitFlag && curWaveStandbyTime <= waveStandbyTime - 1.5f)
        {
            ghostInitFlag = true;
            GhostInit();
        }
        if (curWaveStandbyTime < 0)
        {
            nextWaveFlag = false;
            curWaveStandbyTime = waveStandbyTime;
            spawnNextWaveText = false;
            handAnime.SetInteger("State", 0);
        }
    }

    //GameOver時のTimeScaleの減衰
    void AttenuationTimeScale()
    {
        if (!gameOverFlag) { return; }
        curTimeScaleLerpTime += Time.deltaTime;
        Time.timeScale = Easing.InOutSine(curTimeScaleLerpTime, totalTimeScaleLerpTime, preTimeScale, 1);

        if (curTimeScaleLerpTime > totalTimeScaleLerpTime)
        {
            if (!gameOverText.activeSelf)
            {
                gameOverText.SetActive(true);
                totalNumText.SetTargetNum(score);
                maxComboText.SetTargetNum(maxCombo);
            }
        }

    }
    float GetStandbyTimeRatio() { return (curStandbyTime / curmaxStandbyTime); }
    public bool GetMissPlaying() { return missPlaying; }
    public int GetCurHitPoint() { return curHitPoint; }

    public int GetCurCombo() {  return curCombo; }
}
