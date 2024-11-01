using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRandomStart : MonoBehaviour
{
     Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(animator.GetCurrentAnimatorStateInfo(0).shortNameHash, 0, Random.Range(0f, 1f));
    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
