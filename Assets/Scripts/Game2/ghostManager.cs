using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class ghostManager : MonoBehaviour
{
    [Header("�p�����[�^�[")]
    [SerializeField] float addGameTime;//�Q�[�����Ԃ���������l
    [SerializeField] int initNum;//����������
    [SerializeField] int addGhostNum;//���̐����ő����鐔
    [SerializeField] float interval;//��̊Ԋu
    [SerializeField] float maxStandbyTime;//�ő�P�\����
    [SerializeField] float initMoveTime;
    [SerializeField] int maxHitPoint;
    [SerializeField] float waveStandbyTime;
    [SerializeField] float parfectRatio;
    [SerializeField] float goodRatio;


    float curMoveTime;
    float curmaxStandbyTime;
    float curStandbyTime;
    float curWaveStandbyTime;
    int curHitPoint;
    int score;
    int curWaveCount;
    int curConbo;
    [Header("�v���n�u")]
    [SerializeField] GameObject ghostSpownPoint;
    [SerializeField] GameObject goodText;
    [SerializeField] GameObject niceText;
    [SerializeField] GameObject parfectText;
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
    [SerializeField] GameObject evalutionCharaSpownPoitnt;
    [SerializeField] GameObject[] evalutionCharas;
    //[SerializeField] ShakeScript charsShake;



    List<ghostScript> ghosts = new List<ghostScript>();
    bool nextWaveFlag;
    bool spawnNextWaveText;
    bool ghostInitFlag;
    bool missPlaying;
    bool preSuccesFlag;
    bool curSuccesFlag;
    public string debugText;
    // Start is called before the first frame update
    void Start()
    {
        //�P�\���Ԃ̏�����
        GhostInit();
        //�ŏ���1�ɂ���
        Time.timeScale = 1;

        curHitPoint = maxHitPoint;
        hitPointGauge.SetMaxValue(maxHitPoint);
        hitPointGauge.SetCurrentValue(curHitPoint);
        score = 0;
        scoreText.SetText(score);

        curWaveStandbyTime = waveStandbyTime;
        curWaveCount = 0;
        nextWaveFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        missPlaying = false;//�~�X�������̃t���O���ŏ���false�ɂ��Ă���
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("GameScene");
        }

        if (curHitPoint <= 0) { return; }
        debugText = "";
        //�S�[�X�g�����Ȃ��Ȃ��Ĉړ��J�E���g���I��������Ɏ��̃t�F�[�Y�֍s��
        if (curMoveTime <= 0 && ghosts.Count <= 0)
        {
            nextWaveFlag = true;
        }
        Sorting();
        ProgressMove();
        GaugeChange();
        //GhostInit();
        WaveStandby();
        debugText += "ghosts.Count=" + ghosts.Count + "\ncurMoveTime=" + curMoveTime + "\nTime.timeScale" + Time.timeScale;
    }

    //������
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
        //�P�\���Ԃ̏�����
        RemainginTimeinit();

        //�Q�[�����Ԃ̉���
        AddTimeRatio();
    }
    //�P�\���Ԃ̏�����
    void RemainginTimeinit()
    {

        curmaxStandbyTime = maxStandbyTime;
        curStandbyTime = curmaxStandbyTime;
        remainingTimeGauge.SetMaxValue(curmaxStandbyTime);
    }
    //�Q�[�������Ԃ���������
    void AddTimeRatio()
    {
        Time.timeScale += addGameTime;
    }

    //�d�����̑���
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
    //���菈������
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
    //�~�X�������̃_���[�W
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
    //���肪�I������烊�X�g����폜����
    void DestroyObj()
    {
        // Destroy(ghosts[0].gameObject);
        ghosts.RemoveAt(0);
        //Debug.Log("After removal: " + string.Join(", ", ghosts));
    }
    //���O�ɋl�߂鏈��
    void ProgressMove()
    {
        if (curMoveTime <= 0) { return; }
        curMoveTime -= Time.deltaTime;
        float easeT = curMoveTime / initMoveTime;
        //���O�ɋl�߂�
        for (int i = 0; i < ghosts.Count; i++)
        {
            Vector3 newPos = ghosts[i].transform.position;
            newPos.y = Mathf.Lerp(i * interval, (i + 1) * interval, easeT);
            ghosts[i].transform.position = newPos;

        }

    }
    //����̉��o
    void EvaluationAnime(Ghost_Type type)
    {
        preSuccesFlag = curSuccesFlag;
        
        //�������Ƃ�
        if (Discrimination(type))
        { //Debug.Log("True");
            score++;
            scoreText.SetText(score);
            scoreTextResetAnime.ResetAnime();
            curSuccesFlag = true;
            //preSuccesFlagcurSuccesFlag==True�̎���
            //�R���{���p�����鉉�o
            if (preSuccesFlag && curSuccesFlag)
            {
                curConbo++;
            }
            Debug.Log("CurConbo=" + curConbo);

            if (GetStandbyTimeRatio() > parfectRatio)
            {
                Instantiate(parfectText);
                Instantiate(evalutionCharas[0], evalutionCharaSpownPoitnt.transform);
            }
            else if (GetStandbyTimeRatio() > goodRatio)
            {
                Instantiate(goodText);
                Instantiate(evalutionCharas[1], evalutionCharaSpownPoitnt.transform);
            }
            else
            {
                Instantiate(niceText);
                Instantiate(evalutionCharas[2], evalutionCharaSpownPoitnt.transform);
            }
            
        }
        else
        {
            //�ԈႦ���Ƃ�
            //Debug.Log("False");
            Damage();
            curSuccesFlag = false;
            missPlaying = true;
            curConbo = 0;
            cameraShakeScript.ShakeStart();
            //charsShake.ShakeStart();
            Instantiate(badText);
        }
    }

    //�~�܂��Ă�Ƃ��̗P�\����
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
    //�E�F�[�u�J�n���̏���
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
        //�J�E���g�_�E�����n�܂�^�C�~���O�ŃS�[�X�g���o��
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
        }
    }
    float GetStandbyTimeRatio() { return (curStandbyTime / curmaxStandbyTime); }
    public bool GetMissPlaying() { return missPlaying; }
}