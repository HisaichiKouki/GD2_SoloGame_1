using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomVel : MonoBehaviour
{

    // Start is called before the first frame update
    ghostManager ghostManager;
    Rigidbody2D rigidbody2D;
    void Start()
    {
        ghostManager=FindAnyObjectByType<ghostManager>();
        rigidbody2D=GetComponent<Rigidbody2D>();
        RandomVelocity(4f);
    }

    void RandomVelocity(float value)
    {
        rigidbody2D.velocity = new Vector2(Random.Range(-value, value), Random.Range(-value, value));
        
    }
    void RandomImpulse(float value)
    {
        rigidbody2D.AddForce(new Vector2(Random.Range(-value, value), Random.Range(-value, value)), ForceMode2D.Impulse);

        Debug.Log("rigidbody2D.velocity" + rigidbody2D.velocity);
    }
    // Update is called once per frame
    void Update()
    {
        if (ghostManager.GetMissPlaying())
        {
            RandomImpulse(20);
        }
    }
}
