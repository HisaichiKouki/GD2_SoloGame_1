using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanColliderScript : MonoBehaviour
{

    bool isHit;
    public bool GetIsHit() { return isHit; }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            isHit = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            isHit = false;
        }
    }
}
