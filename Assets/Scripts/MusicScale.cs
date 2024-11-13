using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScale : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] bool onlyY;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("HandTex").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (onlyY)
        {
            Vector3 scale=this.transform.localScale;
            scale.y=target.localScale.y;
            this.transform.localScale=scale;

        }
        else
        {
            this.transform.localScale = target.localScale;

        }
    }
}
