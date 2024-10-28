using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationReset : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
    }
    public void ResetAnime()
    {
        animator.Play("ScoreAnime", 0, 0f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
