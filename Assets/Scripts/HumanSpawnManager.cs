using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpawnManager : MonoBehaviour
{
    [SerializeField] GetSquareSize spawnArea;
    [SerializeField] float spawnNum;
    [SerializeField] GameObject hummanPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnNum; i++)
        {
            Vector2 pos;
            pos.x=Random.Range(spawnArea.GetRange().leftDown.x, spawnArea.GetRange().rightUp.x);
            pos.y = Random.Range(spawnArea.GetRange().leftDown.y, spawnArea.GetRange().rightUp.y);
            Instantiate(hummanPrefab, pos, Quaternion.identity, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
