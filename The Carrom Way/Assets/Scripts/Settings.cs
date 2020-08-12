using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.transform);
    }

    public bool isPracticeMode = false;
    public bool rePositionHUD;


    public void Set_isPracticeMode(bool state)
    {
        isPracticeMode = state;
    }
    public void Set_rePositionHUD(bool state)
    {
        isPracticeMode = state;
    }
}