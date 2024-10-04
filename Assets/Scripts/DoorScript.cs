using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float speedUpRaito;
    [SerializeField] GameObject[] doorObj;

    Vector2[] doorInitPos = new Vector2[2];
    public float currentMove;
    public float currentSpeedUpRaito;

    bool isClose;

    

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < doorObj.Length; i++)
        {
            doorInitPos[i] = doorObj[i].transform.position;
        }
        currentSpeedUpRaito = 1;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isClose = true;
            currentSpeedUpRaito += speedUpRaito;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isClose = false;
        }
        if (isClose)
        {
            currentMove -= moveSpeed * currentSpeedUpRaito * Time.deltaTime;
        }
        else
        {
            currentMove += moveSpeed * currentSpeedUpRaito * Time.deltaTime;         
        }
        currentMove = Mathf.Clamp(currentMove, 0, 2);
        doorObj[0].transform.position = new Vector2(doorInitPos[0].x - currentMove, doorInitPos[0].y);
        doorObj[1].transform.position = new Vector2(doorInitPos[1].x + currentMove, doorInitPos[1].y);
    }
}
