using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DoorScript : MonoBehaviour
{

    [SerializeField] DoorMove[] doorObj;
    [SerializeField] float openPos;//�����ǂ̂��炢�J���̂�
    [SerializeField] float totalTime;//�J������
    [SerializeField] float timeShortening;//���Ԃ��Z���Ȃ銄��
    [SerializeField] float minTime;//���Ԃ��Z���Ȃ銄��
    [SerializeField] float closeTimeEaseT;//���Ԃ��Z���Ȃ銄��
    [SerializeField] float getOnLength;//��Ԕ���ɂȂ�܂ł̋���

    Vector2[] doorInitPos = new Vector2[2];

    HumanSpawnManager humanSpawnManager;
    TrainManager trainManager;
    EndTime endTime;
    GameOver gameOver;
    bool isClose;
    float closeTime;
    float closeTotalTime = 2;
    [Header("debug")]

    public float curTotalTime;
    public float easeCurTotalTime;
    public float currentMove;
    public float debugRatio;
    public float GetGetonLength() { return getOnLength; }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < doorObj.Length; i++)
        {
            doorInitPos[i] = doorObj[i].transform.position;
        }

        curTotalTime = totalTime;
        humanSpawnManager = FindAnyObjectByType<HumanSpawnManager>();
        trainManager = FindAnyObjectByType<TrainManager>();
        endTime = FindAnyObjectByType<EndTime>();
        gameOver=FindAnyObjectByType<GameOver>();
    }

    // Update is called once per frame
    void Update()
    {
        debugRatio = GetOpenRatio();

        StationEnd();
        InputDoor();
        easeCurTotalTime = Mathf.Lerp(easeCurTotalTime, curTotalTime, closeTimeEaseT);
        DoorMove();
    }

    void InputDoor()
    {
        if (endTime.GetRemaingTime() <= 0) { return; }
        if (trainManager.GetTotalCount() <= 0) { return; }
        //����߂鎞��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isClose = true;

            curTotalTime -= timeShortening;
            curTotalTime = Mathf.Clamp(curTotalTime, minTime, totalTime);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isClose = false;
        }
        //Lerp��T��ϓ�����
        if (isClose)
        {
            currentMove += Time.deltaTime;
        }
        else
        {
            currentMove -= Time.deltaTime;
        }
    }
    void DoorMove()
    {
        currentMove = Mathf.Clamp(currentMove, 0, easeCurTotalTime);
        //0���Z�΍�
        if (currentMove > 0)
        {
            doorObj[0].transform.position = Vector2.Lerp(doorInitPos[0], new Vector2(doorInitPos[0].x - openPos, doorInitPos[0].y), currentMove / easeCurTotalTime);
            doorObj[1].transform.position = Vector2.Lerp(doorInitPos[1], new Vector2(doorInitPos[1].x + openPos, doorInitPos[1].y), currentMove / easeCurTotalTime);
        }
        else
        {
            doorObj[0].transform.position = doorInitPos[0];
            doorObj[1].transform.position = doorInitPos[1];
        }
    }

    void StationEnd()
    {
        // if (endTime.GetRemaingTime() > 0) { return; }
        //if (trainManager.GetTotalCount() > 0) { return; }

        if (endTime.GetRemaingTime() <= 0 || trainManager.GetTotalCount() <= 0)
        {
            closeTime += Time.deltaTime;
            currentMove = Mathf.Clamp(currentMove - Time.deltaTime * 2, 0, 1);
            if (closeTime >= closeTotalTime)
            {
                Debug.Log("count=" + trainManager.GetTotalCount());
                gameOver.EndCountUp(trainManager.GetTotalCount()*3);
                if (trainManager.GetTotalCount()<=0)
                {
                    gameOver.EndCountUp(-30);//�S����点�����̃{�[�i�X
                }
                humanSpawnManager.NextStation();
                endTime.ResetTime();
                closeTime = 0;
                isClose = false;
                
            }
        }


    }
    //�J���Ă銄����ς���
    public float GetOpenRatio()
    {
        if (currentMove == 0)
        {
            return 0;
        }
        else
        {
            return (currentMove / easeCurTotalTime);

        }
    }

}


