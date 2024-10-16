using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    [SerializeField] SetTextScript remainingText;
    [SerializeField] SetTextScript gotoTrainText;
    int count;
    int totalCount;

    //ìdé‘Ç…èÊÇ¡ÇΩèàóù
    public void TrainIn()
    {
        count++;
        Debug.Log("TrainIn");
        totalCount--;
        remainingText.SetText(totalCount);
        gotoTrainText.SetText(count);
    }
    public void DeadCount()
    {
        totalCount--;
        remainingText.SetText(totalCount);
    }
    public void SetTotalCount(int value)
    {
        totalCount = value;
        remainingText.SetText(totalCount);
    }
    // Start is called before the first frame update
    void Start()
    {
        remainingText.SetText(totalCount);
        gotoTrainText.SetText(count);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
