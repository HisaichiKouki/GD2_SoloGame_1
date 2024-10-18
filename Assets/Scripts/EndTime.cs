using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTime : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float totalTime;
    [SerializeField] float addTime;
    [SerializeField] SetTextScript remainingText;
    float remainingTime;
    public float GetRemaingTime() { return remainingTime; }
    public void ResetTime() { totalTime += addTime; remainingTime = totalTime; }
    void Start()
    {
        remainingTime = totalTime;
    }

    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0)
        {
            remainingTime = 0;
        }
        remainingText.SetText((int)(remainingTime));
    }
}
