using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostManager : MonoBehaviour
{
    [SerializeField] ghostScript ghostPrefab;
    [SerializeField] int initNum;//èâä˙ê∂ê¨êî
    [SerializeField] float interval;//óÒÇÃä‘äu
    [SerializeField] GameObject ghostSpownPoint;
    [SerializeField] float initMoveTime;
    [SerializeField] GameObject goodText;
    [SerializeField] GameObject badText;
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
        ProgressMove();
    }

    void Sorting()
    {
        if (curMoveTime > 0) { return; }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (ghosts.Count > 0)
            {
                ghosts[0].SetAnimetion("GoHeaven");
                EvaluationAnime(Ghost_Type.NORMAL);
                DestroyObj();
                curMoveTime = initMoveTime;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (ghosts.Count > 0)
            {
                ghosts[0].SetAnimetion("GoHell");
                EvaluationAnime(Ghost_Type.EVIL);
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
        //óÒÇëOÇ…ãlÇﬂÇÈ
        for (int i = 0; i < ghosts.Count; i++)
        {
            Vector3 newPos = ghosts[i].transform.position;
            newPos.y = Mathf.Lerp(i*interval,(i+1)*interval,easeT);
            ghosts[i].transform.position = newPos;

        }
    }

    void EvaluationAnime(Ghost_Type type)
    {
        if (Discrimination(type))
        {
            Instantiate(goodText);
        }
        else
        {
            Instantiate(badText);
        }
    }
}
