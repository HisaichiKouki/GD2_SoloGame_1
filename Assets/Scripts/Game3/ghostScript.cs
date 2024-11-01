using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    [Header("�p�����[�^�[")]
    [SerializeField] float moveSpeed;
    [SerializeField] bool normalFlag;

    bool isNotDecision;
    Vector3 newPosition;
    [Header("�v���n�u")]
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
        //�ړ�����
        newPosition = transform.position;
        newPosition.y -= moveSpeed * Time.deltaTime;
        transform.position = newPosition;
        //���菈��
        Decision();
    }
    //���胉�C���ɗ�����
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
