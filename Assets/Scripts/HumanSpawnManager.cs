using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HumanSpawnManager : MonoBehaviour
{
    [SerializeField] GetSquareSize spawnArea;
    [SerializeField] int spawnNum;
    [SerializeField] GameObject hummanPrefab;
    [SerializeField] float addSize;
    public GameObject spownObj;
    TrainManager trainManager;
    // Start is called before the first frame update
    void Start()
    {
        trainManager = FindAnyObjectByType<TrainManager>();
        trainManager.SetTotalCount(spawnNum);

        HumanSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    public void NextStation()
    {
        Vector3 newSize = spawnArea.transform.localScale;
        newSize.y += addSize;
        spawnArea.transform.localScale = newSize;

        HumanReset();
        spawnNum += 10;
        //Debug.Log("SpawnNum=" + spawnNum);
        trainManager.SetTotalCount(spawnNum);
        HumanSpawn();
    }

    void HumanSpawn()
    {
        for (int i = 0; i < spawnNum; i++)
        {
            Vector2 pos;
            pos.x = Random.Range(spawnArea.GetRange().leftDown.x, spawnArea.GetRange().rightUp.x);
            pos.y = Random.Range(spawnArea.GetRange().leftDown.y, spawnArea.GetRange().rightUp.y);
            Instantiate(hummanPrefab, pos, Quaternion.identity, spownObj.transform);
        }
    }
    public void HumanReset()
    {
        foreach (Transform n in spownObj.transform)
        {
            GameObject.Destroy(n.gameObject);
        }
    }
}
