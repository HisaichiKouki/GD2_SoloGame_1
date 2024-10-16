using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmpritudeScale : MonoBehaviour
{
    [SerializeField] float ampritude;
    [SerializeField] float period;
    [SerializeField] float easeTime;
    [SerializeField] float easeT;

    public bool startEasing;

    Vector3 initScale;
    
    // Start is called before the first frame update
    void Start()
    {
        easeT = 0;
        initScale=transform.localScale;
        //initScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(!startEasing) { return; }
        easeT += Time.deltaTime;
        this.transform.localScale= Easing.EaseAmplitudeScale(initScale, easeT,easeTime,ampritude,period);
        if (easeT > easeTime)
        {
            startEasing = false;
            easeT = 0;
        }
    }

    public void EaseStop()
    {
        easeT = 0;
        startEasing = false;
        this.transform.localScale = initScale;
    }
    public void EaseStart()
    {
        //既に起動してたらリスタートする
        if (startEasing)
        {
            easeT = 0;
        }
        startEasing = true;
    }

}
