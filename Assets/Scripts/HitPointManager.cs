using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointManager : MonoBehaviour
{
    [SerializeField] MusicScale[] musicscales;
    ghostManager ghostManagerSc;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        ghostManagerSc = FindAnyObjectByType<ghostManager>();
        index = 3;
        musicscales[index].enabled = true;

    }

    // Update is called once per frame
    void Update()
    {



    }

    public void Damage()
    {
        musicscales[index].gameObject.SetActive(false);
        index--;
        if (index >= 0)
        {
            musicscales[index].enabled = true;
        }
    }
}
