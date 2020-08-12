using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class SliderPos : MonoBehaviour
{
    public static SliderPos instance;
    float sliderValue = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.transform);
    }

    public float getValue { get { return sliderValue; } }

    public void OnValueChange(float value)
    {

        sliderValue = value;

    }


}
