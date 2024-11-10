using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScale : MonoBehaviour
{
    [SerializeField] Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("HandTex").transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale = target.localScale;
    }
}
