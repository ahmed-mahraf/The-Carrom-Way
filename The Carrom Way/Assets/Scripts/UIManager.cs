using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIManager>();
            }

            return _instance;
        }
    }

     ///////////////////////////////
    //---------------------------//
   ///////////////////////////////

    public Image powerIndicator;
    public float factor = 5;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.striker.GetComponent<Rigidbody2D>().velocity.magnitude == 0 
            && GameManager.Instance.striker.posIsSet)
            powerIndicator.fillAmount = 
                Vector2.Distance(GameManager.Instance.striker.gameObject.transform.position, 
                Camera.main.ScreenToWorldPoint(Input.mousePosition)) / factor;



        //if (Input.GetKey(KeyCode.E)) //Increase striker force
        //{
        //    powerIndicator.fillAmount = Mathf.Clamp(powerIndicator.fillAmount, 0.1f, 1f) + Time.deltaTime;
        //}

        //if (Input.GetKey(KeyCode.Q)) //Decrease striker force
        //{
        //    powerIndicator.fillAmount = Mathf.Clamp(powerIndicator.fillAmount, 0.1f, 1f) - Time.deltaTime;
        //}
    }
}
