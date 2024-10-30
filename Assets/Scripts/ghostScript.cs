using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    [Header("パラメーター")]
    [SerializeField] float moveSpeed;
    [SerializeField] bool normalFlag;

    bool isNotDecision;
    Vector3 newPosition;
    [Header("プレハブ")]
    DecisionLineScript decisionLine;
    HellManager hellManager;
    // Start is called before the first frame update
    void Start()
    {
        hellManager = FindAnyObjectByType<HellManager>();
        decisionLine = FindAnyObjectByType<DecisionLineScript>();
        isNotDecision = false;
    }

    // Update is called once per frame
    void Update()
    {
        //移動処理
        newPosition = transform.position;
        newPosition.y -= moveSpeed * Time.deltaTime;
        transform.position = newPosition;
        //判定処理
        Decision();
    }
    //判定ラインに来た時
    void Decision()
    {
        if (isNotDecision) { return; }
        if (this.transform.position.y > decisionLine.transform.position.y)
        {
            return;
        }
        isNotDecision = true;

        if (hellManager.GetIsOpen())
        {

            if (normalFlag)
            {
                Debug.Log("False");

            }
            else
            {
                Debug.Log("True");

            }
        }
        else
        {
            if (normalFlag)
            {
                Debug.Log("True");

            }
            else
            {
                Debug.Log("False");

            }
        }
    }
}
