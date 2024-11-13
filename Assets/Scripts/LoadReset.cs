using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadReset : MonoBehaviour
{
    static public bool reset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!reset)
        {
            SceneManager.LoadScene("GameScene");
            reset = true;
        }
    }
}
