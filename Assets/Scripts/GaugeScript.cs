using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeScript : MonoBehaviour
{
    Slider slider;
    // Start is called before the first frame update
    private void Awake()
    {
        slider = GetComponent<Slider>();

    }
    void Start()
    {
    }

    public void SetCurrentValue(float ratio) { slider.value = ratio; }
    public void SetMaxValue(float max) { slider.maxValue = max; }
    // Update is called once per frame
    void Update()
    {
        
    }
}
