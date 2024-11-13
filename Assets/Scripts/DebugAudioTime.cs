using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugAudioTime : MonoBehaviour
{
    [SerializeField] AudioSource targetAudio;

    [SerializeField] bool debug;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (debug)
        {
            Debug.Log("targetAudio.Time" + targetAudio.time);
            Debug.Log("Time.time" + Time.time);
            debug = false;
        }
    }
}
