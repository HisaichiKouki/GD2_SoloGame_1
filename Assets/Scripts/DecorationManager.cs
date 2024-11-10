using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationManager : MonoBehaviour
{
    [SerializeField] DecorationScript[] decorations;
    ghostManager ghostManagerSc;
    // Start is called before the first frame update
    void Start()
    {
        ghostManagerSc = FindAnyObjectByType<ghostManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (ghostManagerSc.GetCurCombo() >= 500 - 1)
        {
            SetActive(5, true);
        }
        else if(ghostManagerSc.GetCurCombo() >= 200 - 1)
        {
            SetActive(4, true);
        }
        else if (ghostManagerSc.GetCurCombo() >= 100 - 1)
        {
            SetActive(3, true);
        }
        else if (ghostManagerSc.GetCurCombo() >= 50 - 1)
        {
            SetActive(2, true);
        }
        else if (ghostManagerSc.GetCurCombo() >= 25 - 1)
        {
            SetActive(1, true);
        }
        else if (ghostManagerSc.GetCurCombo() >= 10-1)
        {

            SetActive(0,true);
        }

        if (ghostManagerSc.GetCurCombo() == 0)
        {
            for (int i = 0; i < decorations.Length; i++)
            {
                SetActive(i,false);
            }
        }
    }



    void SetActive(int index, bool value)
    {
        if (decorations[index].GetIsFlag() != value)
        {
            decorations[index].SetAnimator(value);
        }
    }


}
