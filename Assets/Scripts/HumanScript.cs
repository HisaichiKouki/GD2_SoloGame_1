using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanScript : MonoBehaviour
{
    [SerializeField] HumanColliderScript[] sideColliders;
    bool isDead;
    DoorScript doorScript;

    [SerializeField] float moveSpeed;
    Rigidbody2D mRigidbody;
    public string debugText;
    

    // Start is called before the first frame update
    void Start()
    {
        doorScript=FindAnyObjectByType<DoorScript>();
        mRigidbody=GetComponent<Rigidbody2D>();
        mRigidbody.velocity = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        debugText = "";
        Move();
        Dead();
        debugText += mRigidbody.velocity;
        
    }
    //移動処理
    void Move()
    {
        if (isDead) { return; }
        if (doorScript.GetOpenRatio()<0.5f)
        {
            return;
        }
        mRigidbody.velocity=new Vector2(0,moveSpeed);
        
    }
    //死亡処理
    void Dead()
    {
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
     
}
