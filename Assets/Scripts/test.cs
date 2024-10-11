using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    float easet;
    public float totalTime;
    public Vector2 start;
    public Vector2 end;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        easet += Time.deltaTime;
        transform.position = Vector2.Lerp(start, end, easet/ totalTime);
        //Vector2 newPos = transform.position;
        //newPos.x += 1 * Time.deltaTime;
        //transform.position = newPos;
    }
}
