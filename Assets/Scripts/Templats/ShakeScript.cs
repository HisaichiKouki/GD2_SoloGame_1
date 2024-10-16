using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScript : MonoBehaviour
{
    public float shakeTime;
    float shakeTimeCount;
    public float shakeNum;
    float shakeNumCount;
    public float shakeSize;
    float currentShakeSize;

    Vector3 newPosition;
    Vector3 initPos;
    bool isShake;
    RectTransform rectTransform;
    Transform transformPos;
    // Start is called before the first frame update
    void Start()
    {
        shakeNumCount = shakeNum;
        shakeTimeCount = 0;
        transformPos = GetComponent<Transform>();
    }
    public bool GetisShake() {  return isShake; }
    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.G))
        {
            ShakeStart();
        }
#endif
        IsShake();

    }

    public void SetShakeParamater()
    {
        shakeNumCount = shakeNum;
        currentShakeSize=shakeSize;
    }
    public void ShakeStart()
    {
        isShake = true;
        initPos = transformPos.position;
    }

    void IsShake()
    {
        if (!isShake) { return; }
        if (shakeTimeCount > 0)
        {
            shakeTimeCount -= Time.deltaTime;
            return;
        }
        shakeTimeCount = shakeTime;
        shakeNumCount--;

        // Debug.Log("ShakeNum=" + shakeNumCount);
        //shakeèIÇÌÇË
        if (shakeNumCount == 0)
        {
            isShake = false;
            shakeNumCount = shakeNum;
            // rectTransform.anchoredPosition = Vector3.zero;
            transformPos.position = initPos;
            shakeTimeCount = 0;
            return;
        }
        currentShakeSize = shakeSize * (shakeNumCount / shakeNum);
        newPosition = initPos;
        if (currentShakeSize != 0)
        {
            newPosition.x += Random.Range(-currentShakeSize, currentShakeSize);
            newPosition.y += Random.Range(-currentShakeSize, currentShakeSize);

        }

        transformPos.position = newPosition;

    }
}
