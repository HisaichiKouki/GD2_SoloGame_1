using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DoorScript : MonoBehaviour
{

    [SerializeField] DoorMove[] doorObj;
    [SerializeField] float openPos;//扉をどのくらい開くのか
    [SerializeField] float totalTime;//開く時間
    [SerializeField] float timeShortening;//時間が短くなる割合
    [SerializeField] float minTime;//時間が短くなる割合
    [SerializeField] float closeTimeEaseT;//時間が短くなる割合

    Vector2[] doorInitPos = new Vector2[2];


    bool isClose;

    [Header("debug")]

    public float curTotalTime;
    public float easeCurTotalTime;
    public float currentMove;
    public float debugRatio;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < doorObj.Length; i++)
        {
            doorInitPos[i] = doorObj[i].transform.position;
        }

        curTotalTime = totalTime;
    }

    // Update is called once per frame
    void Update()
    {
        debugRatio = GetOpenRatio();
        //扉を閉める時に
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
        //LerpのTを変動する
        if (isClose)
        {
            currentMove += Time.deltaTime;
        }
        else
        {
            currentMove -= Time.deltaTime;
        }

        easeCurTotalTime = Mathf.Lerp(easeCurTotalTime, curTotalTime, closeTimeEaseT);
        currentMove = Mathf.Clamp(currentMove, 0, easeCurTotalTime);
        //0除算対策
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


