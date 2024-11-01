using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class SplineStartOfsetRandom : MonoBehaviour
{
    SplineAnimate splineAnimate;
    // Start is called before the first frame update
    void Start()
    {
        splineAnimate=GetComponent<SplineAnimate>();
        splineAnimate.StartOffset = Random.Range(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
