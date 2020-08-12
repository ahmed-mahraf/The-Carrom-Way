using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public static Popup instance;
    [Range(1, 5)]
    public float aliveTime;
    [Space]
    public GameObject popup;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(transform);
    }

    public void showPopup(string message = "FOUL")
    {
        popup.SetActive(true);
        Invoke("hidePopup", aliveTime);
    }

    void hidePopup()
    {
        popup.SetActive(false);
    }
}
