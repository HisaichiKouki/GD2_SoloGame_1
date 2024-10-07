using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YtoZPosition : MonoBehaviour
{
    Vector3 newPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         newPos=this.transform.localPosition;
        newPos.z = this.transform.localPosition.y;
        this.transform.localPosition = newPos;
    }
}
