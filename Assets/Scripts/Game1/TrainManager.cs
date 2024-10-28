using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    [SerializeField] SetTextScript remainingText;
    [SerializeField] SetTextScript gotoTrainText;
    [SerializeField] SetTextScript deadText;
    [SerializeField] SetTextScript badGotoTrainText;
    [SerializeField] SetTextScript badDeadText;
    int totalCount;
    int count;
    int deadCount;
    int badCount;
    int badDeadCount;
    HumanSpawnManager humanSpawnManager;
    GameOver gameOver;

    public int GetTotalCount() { return totalCount; }
    //ìdé‘Ç…èÊÇ¡ÇΩèàóù
    public void TrainIn()
    {
        count++;
        totalCount--;
        remainingText.SetText(totalCount);
        gotoTrainText.SetText(count);
    }
    //êlÇ™éÄÇÒÇæéû
    public void DeadCount()
    {
        totalCount--;
        deadCount++;
        remainingText.SetText(totalCount);
        deadText.SetText(deadCount);

        gameOver.EndCountUp(-1);
    }
    //à´é“Ç™èÊÇ¡ÇΩéû
    public void BadTrainIn()
    {
        //Debug.Log("TrainIn");
        badCount++;
        totalCount--;
        remainingText.SetText(totalCount);
        badGotoTrainText.SetText(badCount);
        gameOver.EndCountUp(-8);

    }
    //à´é“ÇéEÇµÇΩÇ∆Ç´
    public void BadDeadCount()
    {
        totalCount--;
        badDeadCount++;
        remainingText.SetText(totalCount);
        badDeadText.SetText(badDeadCount);
        gameOver.EndCountUp(5);

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
        deadText.SetText(0);

        humanSpawnManager = FindAnyObjectByType<HumanSpawnManager>();
        gameOver=FindAnyObjectByType<GameOver>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
