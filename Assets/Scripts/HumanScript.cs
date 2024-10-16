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
    [SerializeField] float moveRatio;//ドアから遠い程少しの開閉で止まるように
    [SerializeField] float doorLength;//ドアに入ったくらいの距離では止まらないように
    Rigidbody2D mRigidbody;
    
    Vector2 dif;
    bool getOnFlag;//乗車したかフラグ
    TrainManager trainManager;
    Vector2 moveVec;
    float humanMoveRatio;

    public string debugText;
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
        //移動生成時にドアに向かう速度を計算
        moveVec=doorScript.transform.position- transform.position;
        moveVec=moveVec.normalized*moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //ドアまでの距離
        dif = this.transform.position - doorScript.transform.position;
        humanMoveRatio= Mathf.Clamp((dif.magnitude * dif.magnitude) / moveRatio, 0.4f, 1.0f);
        //debugText = "";
        Move();
        GotoTrain();
        Dead();
        //debugText += mRigidbody.velocity;
        debugText = "\nlength=" + dif.magnitude +"\nRaito="+ (dif.magnitude * dif.magnitude) / moveRatio +"\nhumanMoveRatio="+ humanMoveRatio;
    }
    //移動処理
    void Move()
    {
        if (isDead) { return; }
        if (getOnFlag) { return; }
        if (dif.magnitude < doorLength)
        {
            mRigidbody.velocity = moveVec;
            return;
        }
        if (doorScript.GetOpenRatio() < humanMoveRatio)
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
                trainManager.DeadCount();
            }
            isDead = true;
            mRigidbody.velocity = new Vector2(0, 0);
        }
    }

    //乗車処理
    void GotoTrain()
    {
        if (isDead) { return; }
        
        
        //どの角度からでも対応出来るようにドアからの距離で入ったかを検知する
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
