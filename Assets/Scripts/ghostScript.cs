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

    [SerializeField] GameObject normalTex;
    [SerializeField] GameObject evilTex;
    [SerializeField] float typeRatio;//���l�̊���
    Ghost_Type ghostType;
    private Animator animation;
    public Ghost_Type GetType() { return ghostType; }
    // Start is called before the first frame update
    void Start()
    {
        normalTex.SetActive(false);
        evilTex.SetActive(false);

        float randomNum = Random.Range(0, 100);
        if (randomNum < typeRatio)
        {
            ghostType = Ghost_Type.NORMAL;

        }
        else
        {
            ghostType = Ghost_Type.EVIL;
        }

        if (ghostType== Ghost_Type.NORMAL)
        {
            normalTex.SetActive(true);
        }
        else if (ghostType == Ghost_Type.EVIL)
        {
            evilTex.SetActive(true);
        }
        animation = GetComponent<Animator>();
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
