using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DoorScript : MonoBehaviour
{

    [SerializeField] DoorMove[] doorObj;
    [SerializeField] float openPos;//”à‚ğ‚Ç‚Ì‚­‚ç‚¢ŠJ‚­‚Ì‚©
    [SerializeField] float totalTime;//ŠJ‚­ŠÔ
    [SerializeField] float timeShortening;//ŠÔ‚ª’Z‚­‚È‚éŠ„‡
    [SerializeField] float minTime;//ŠÔ‚ª’Z‚­‚È‚éŠ„‡
    [SerializeField] float closeTimeEaseT;//ŠÔ‚ª’Z‚­‚È‚éŠ„‡

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
        //”à‚ğ•Â‚ß‚é‚É
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
        //Lerp‚ÌT‚ğ•Ï“®‚·‚é
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
        //0œZ‘Îô
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


