using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Ghost_Type
{
    NORMAL,    //�P�l
    EVIL,  //���l
    HEINOUS,    //�Ɉ��l
}
public class ghostScript : MonoBehaviour
{
   
    Ghost_Type ghostType;
    private Animator animation;
    public Ghost_Type GetType() { return ghostType; }
    // Start is called before the first frame update
    void Start()
    {
        ghostType=Ghost_Type.NORMAL;
        animation= GetComponent<Animator>();
    }

    public void SetAnimetion(string goType)
    {
        animation.SetTrigger(goType);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
}
