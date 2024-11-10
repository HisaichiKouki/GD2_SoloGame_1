using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationScript : MonoBehaviour
{

    Animator animator;
    bool isFlag;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 scale = transform.localScale;
        scale.y = 0;
        transform.localScale = scale;
        animator = GetComponent<Animator>();
    }

    public void SetAnimator(bool flag)
    {
        isFlag = flag;
        animator.SetBool("isDisplay", isFlag);
    }
    public bool GetIsFlag() { return isFlag; }

    // Update is called once per frame
    void Update()
    {

    }
}
