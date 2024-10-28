using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverText;
    public int endCount;
    public SetTextScript gatext;
    public void EndCountUp(int value) { endCount += value; gatext.SetText(endCount); }

    public float totalTime;
    float curTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (endCount <= 0)
        {
            curTime += Time.deltaTime;
            if (curTime>totalTime)
            {
                gameOverText.SetActive(true);

            }

        }
        else
        {
            curTime = totalTime;
        }
    }
}
