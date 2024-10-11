using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanScript : MonoBehaviour
{
    [SerializeField] HumanColliderScript[] sideColliders;
    bool idDead;

    [SerializeField] float moveSpeed;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Dead();
    }
    void Move()
    {
        if (idDead) { return; }
        Vector2 newPos = transform.position;
        newPos.y += moveSpeed*Time.deltaTime;

        transform.position = newPos;
    }
    void Dead()
    {
        if (sideColliders[0].GetIsHit() && sideColliders[1].GetIsHit())
        {
            idDead = true;
            Debug.Log("Dead");
        }
    }
}
