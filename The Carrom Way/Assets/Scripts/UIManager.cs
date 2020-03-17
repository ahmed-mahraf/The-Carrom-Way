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
    public Text player1;
    public Text player2;
    public Text gameText;
    public Text winnerText;

    public float factor = 5;

    void Start()
    {
        winnerText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.striker.GetComponent<Rigidbody2D>().velocity.magnitude == 0 
            && GameManager.Instance.striker.posIsSet)
            powerIndicator.fillAmount = 
                Vector2.Distance(GameManager.Instance.striker.gameObject.transform.position, 
                Camera.main.ScreenToWorldPoint(Input.mousePosition)) / factor;

        player1.text = "Player 1 : " + GameManager.Instance.noOfBlackCoins.ToString();
        player2.text = "Player 2 : " + GameManager.Instance.noOfWhiteCoins.ToString();

        if(GameManager.Instance.striker.HostPlayer == true)
        {
            gameText.text = "Player 1's Turn (Black)";
        }
        else
        {
            gameText.text = "Player 2's Turn (White)";
        }
    }
}
