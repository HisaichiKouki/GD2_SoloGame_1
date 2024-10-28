using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ghostManager : MonoBehaviour
{
    [Header("�p�����[�^�[")]
    [SerializeField] float addGameTime;//�Q�[�����Ԃ���������l
    [SerializeField] int initNum;//����������
    [SerializeField] int addGhostNum;//���̐����ő����鐔
    [SerializeField] float interval;//��̊Ԋu
    [SerializeField] float maxStandbyTime;//�ő�P�\����
    [SerializeField] float initMoveTime;
    float curMoveTime;
    [Header("�v���n�u")]
    [SerializeField] GameObject ghostSpownPoint;
    [SerializeField] GameObject goodText;
    [SerializeField] GameObject badText;
    [SerializeField] ghostScript ghostPrefab;
    float curmaxStandbyTime;
    float curStandbyTime;
    [SerializeField] GaugeScript remainingTimeGauge;

    List<ghostScript> ghosts = new List<ghostScript>();
    bool nextWaveFlag;

    public string debugText;
    // Start is called before the first frame update
    void Start()
    {
        nextWaveFlag = true;
        //�P�\���Ԃ̏�����
        GhostInit();
        //�ŏ���1�ɂ���
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        debugText = "";
        //�S�[�X�g�����Ȃ��Ȃ��Ĉړ��J�E���g���I��������Ɏ��̃t�F�[�Y�֍s��
        if (curMoveTime <= 0 && ghosts.Count <= 0)
        {
            nextWaveFlag = true;
        }
        Sorting();
        ProgressMove();
        GaugeChange();
        GhostInit();
        debugText += "ghosts.Count="+ghosts.Count+ "\ncurMoveTime=" + curMoveTime;
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
        if (curMoveTime > 0) { return; }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
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
        else if (Input.GetKeyDown(KeyCode.RightArrow))
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
            return true;
        }
        else
        {
            Debug.Log("False");
            return false;
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
        //���O�ɋl�߂�
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
    }

}
