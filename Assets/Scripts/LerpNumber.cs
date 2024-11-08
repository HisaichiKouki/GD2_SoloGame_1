using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpNumber : MonoBehaviour
{

    SetTextScript setTextScript;
    public int minNum;
    public int maxNum;

    [SerializeField] float totalEaseT;
    float curEaseT;

    int curNum;
    bool easeStart;
    // Start is called before the first frame update
    void Start()
    {
        setTextScript = GetComponent<SetTextScript>();
        setTextScript.SetText(0);
        easeStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (curEaseT<= totalEaseT)
        {
            curEaseT += Time.deltaTime;

        }

        curNum = (int)Easing.OutCubic(curEaseT, totalEaseT, (float)minNum, (float)maxNum);
        setTextScript.SetText(curNum);
    }
    public void SetTargetNum(int value)
    {
        maxNum = value;
    }
    public void StartEase(int min,int max)
    {
        minNum = min;
        maxNum = max;
        easeStart = false;
    }
    public void ResetEase()
    {
        curEaseT = 0;
        curNum = minNum;
    }
}
