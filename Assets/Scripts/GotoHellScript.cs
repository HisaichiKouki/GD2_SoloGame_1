using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoHellScript : MonoBehaviour
{
    [SerializeField] GameObject gotoHellPrefab;

    public void EffectGotoHell() { Instantiate(gotoHellPrefab); }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
