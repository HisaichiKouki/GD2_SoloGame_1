using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanScript : MonoBehaviour
{
    [SerializeField] HumanColliderScript[] sideColliders;
    bool isDead;
    DoorScript doorScript;

    [SerializeField] float moveSpeed;
    [SerializeField] bool randomSpeedFlag;
    [SerializeField] float min;
    [SerializeField] float max;
    Rigidbody2D mRigidbody;
    public string debugText;
    Vector2 dif;
    bool getOnFlag;//乗車したかフラグ
    TrainManager trainManager;
    Vector2 moveVec;

    // Start is called before the first frame update
    void Start()
    {
        doorScript = FindAnyObjectByType<DoorScript>();
        trainManager = FindAnyObjectByType<TrainManager>();
        mRigidbody = GetComponent<Rigidbody2D>();
        mRigidbody.velocity = new Vector2(0, 0);

        if (randomSpeedFlag)
        {
            moveSpeed = Random.Range(min, max);
        }

        moveVec=doorScript.transform.position- transform.position;
        moveVec=moveVec.normalized*moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //debugText = "";
        Move();
        GotoTrain();
        Dead();
        //debugText += mRigidbody.velocity;
        debugText = "\nlength=" + dif.magnitude;
    }
    //移動処理
    void Move()
    {
        if (isDead) { return; }
        if (getOnFlag) { return; }
        if (doorScript.GetOpenRatio() < 0.5f)
        {
            mRigidbody.velocity = new Vector2(0, 0);

            return;
        }
        mRigidbody.velocity = moveVec;

    }
    //死亡処理
    void Dead()
    {
        if (getOnFlag) { return; }
        //サイドのコライダーがどっちもドアに触れてたら
        if (sideColliders[0].GetIsHit() && sideColliders[1].GetIsHit())
        {
            if (!isDead)
            {
                Debug.Log("Dead");
            }
            isDead = true;
            mRigidbody.velocity = new Vector2(0, 0);
        }
    }

    //乗車処理
    void GotoTrain()
    {
        if (isDead) { return; }
        
        dif = this.transform.position - doorScript.transform.position;
        
        if (dif.magnitude <= doorScript.GetGetonLength())
        {
            if (!getOnFlag)
            {
                trainManager.TrainIn();
            }
            getOnFlag = true;
            //テスト用に人を削除する
            Destroy(this.gameObject);

        }

    }
}
