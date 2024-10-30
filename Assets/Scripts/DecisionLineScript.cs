using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionLineScript : MonoBehaviour
{

    [SerializeField] GameObject openTex;
    [SerializeField] GameObject closeTex;
    HellManager hellManager;
    // Start is called before the first frame update
    void Start()
    {
        hellManager=FindAnyObjectByType<HellManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (hellManager.GetIsOpen())
        {
            openTex.SetActive(true);
            closeTex.SetActive(false);
        }
        else
        {
            openTex.SetActive(false);
            closeTex.SetActive(true);
        }
    }
}
