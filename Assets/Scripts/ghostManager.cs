using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostManager : MonoBehaviour
{
    [SerializeField] ghostScript ghostPrefab;
    [SerializeField] int initNum;//����������
    [SerializeField] float interval;//��̊Ԋu
    [SerializeField] GameObject ghostSpownPoint;
    [SerializeField] float initMoveTime;
    float curMoveTime;
    List<ghostScript> ghosts = new List<ghostScript>();


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < initNum; i++)
        {
            ghostScript ghost = Instantiate(ghostPrefab);
            ghost.transform.position = new Vector3(0, i * interval, 0);
            ghost.transform.parent = ghostSpownPoint.transform;
            ghosts.Add(ghost);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Sorting();

    }

    void Sorting()
    {
        if (curMoveTime > 0) { return; }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (ghosts.Count > 0)
            {
                ghosts[0].SetAnimetion("GoHeaven");
                Discrimination(Ghost_Type.NORMAL);
                DestroyObj();
                curMoveTime = initMoveTime;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (ghosts.Count > 0)
            {
                ghosts[0].SetAnimetion("GoHell");
                Discrimination(Ghost_Type.NORMAL);
                DestroyObj();
                curMoveTime = initMoveTime;
            }
        }
    }

    bool Discrimination(Ghost_Type type)
    {
        if (ghosts[0].GetType() == type)
        {
            Debug.Log("True");
            return true;
        }
        else
        {
            Debug.Log("False");
            return false;
        }
    }

    void DestroyObj()
    {
       // Destroy(ghosts[0].gameObject);
        ghosts.RemoveAt(0);

        

        //Debug.Log("After removal: " + string.Join(", ", ghosts));
    }

    void ProgressMove()
    {
        if(curMoveTime<=0) { return;}
        curMoveTime-=Time.deltaTime;
        float easeT=curMoveTime/ initMoveTime;
        //���O�ɋl�߂�
        for (int i = 0; i < ghosts.Count; i++)
        {
            Vector3 newPos = ghosts[i].transform.position;
            newPos.y -= interval;
            ghosts[i].transform.position = newPos;

        }
    }
}