using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayActive : MonoBehaviour
{

    public GameObject[] targets;
    [SerializeField, Header("初期遅延時間")] float startDelay;
    [SerializeField, Header("間隔遅延時間")] float intervalDelay;

    float currentStartDelay;
    float currentIntervalDelay;
    int count;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject target in targets)
        {
            target.SetActive(false);
        }
        Simple();
    }

    // Update is called once per frame
    void Update()
    {
        currentStartDelay += Time.deltaTime;
        if (currentStartDelay > startDelay)
        {
            Active();
        }

    }

    void Active()
    {
        if (currentIntervalDelay > 0)
        {
            currentIntervalDelay -= Time.deltaTime;
            return;
        }
        if (count >= targets.Length) { return; }

        targets[count].SetActive(true);
        count++;
        currentIntervalDelay = intervalDelay;
    }

    private IEnumerator Simple()
    {

        for (int i = 0; i < targets.Length; i++)
        {
            // 一文字ごとに0.2秒待機
            yield return new WaitForSeconds(0.2f);

            // 文字の表示数を増やしていく
            targets[i].SetActive(false);
        }
    }
}