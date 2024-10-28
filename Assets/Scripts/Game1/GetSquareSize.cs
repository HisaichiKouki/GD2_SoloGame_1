using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSquareSize : MonoBehaviour
{
   SpriteRenderer spriteRenderer;
    Vector2 size;
    public string debugText;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer=GetComponent<SpriteRenderer>();
    }
    public Vector2 GetSize()
    {
        size = spriteRenderer.bounds.size;
        return size;
    }
    public (Vector2 leftDown,Vector2 rightUp) GetRange()
    {
        Vector2 leftDown;
        leftDown.x = spriteRenderer.bounds.min.x;
        leftDown.y = spriteRenderer.bounds.min.y;
        Vector2 rightUp;
        rightUp.x = spriteRenderer.bounds.max.x;
        rightUp.y = spriteRenderer.bounds.max.y;
        return (leftDown, rightUp);
    }
    // Update is called once per frame
    void Update()
    {
        debugText = ""+GetRange();
    }
}
