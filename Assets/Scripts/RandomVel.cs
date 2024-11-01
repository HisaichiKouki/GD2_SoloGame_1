using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomVel : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3f, 3f), 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
