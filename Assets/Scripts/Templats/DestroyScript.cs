using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{
    float destroyTime;
    // Start is called before the first frame update
    void Start()
    {
        destroyTime = 3;
    }

    // Update is called once per frame
    void Update()
    {
        destroyTime -= Time.deltaTime;
        if ( destroyTime < 0 )
        {
            Destroy( this.gameObject );
        }
    }
}
