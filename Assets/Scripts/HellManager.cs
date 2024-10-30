using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellManager : MonoBehaviour
{
    [Header("パラメーター")]
    [SerializeField] float initCoolTime;
    [SerializeField] int hitPoint;

    float curCoolTime;
    float curHitPoint;
    public bool isOpen;
    [Header("プレハブ")]
    [SerializeField] GhostScript[] ghosts;
    [SerializeField] GameObject spwanPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        OpenClose();
        SpawnGhost();
    }

    void OpenClose()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            isOpen = true;
        }
        else
        {
            isOpen = false;
        }
    }

    void SpawnGhost()
    {
        curCoolTime-=Time.deltaTime;
        if (curCoolTime <= 0)
        {
            Instantiate(ghosts[Random.Range(0, ghosts.Length)], spwanPoint.transform);
            curCoolTime = initCoolTime;
        }
    }


    //アクセッサ
    public bool GetIsOpen() { return isOpen; }
}
