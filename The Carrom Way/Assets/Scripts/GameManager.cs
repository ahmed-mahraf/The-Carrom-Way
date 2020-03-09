using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    public Transform collectedBlackPos;
    public Transform collectedWhitePos;

    public int noOfBlackCoins;
    public int noOfWhiteCoins;

    public float offset = 0.5f;
   
    int strikerForce;
    public int baseStrikerForce = 1000;
    public Striker striker;

    public void blackCoinCollected(GameObject collectedBlackCoin)
    {
        collectedBlackCoin.GetComponent<CircleCollider2D>().isTrigger = true;
        collectedBlackCoin.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        offset = 0.5f * noOfBlackCoins;
        collectedBlackCoin.transform.position = new Vector2(collectedBlackPos.position.x + offset, collectedBlackPos.position.y);
        noOfBlackCoins += 1;
    }

    public void whiteCoinCollected(GameObject collectedWhiteCoin)
    {
        collectedWhiteCoin.GetComponent<CircleCollider2D>().isTrigger = true;
        collectedWhiteCoin.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        offset = 0.5f * noOfWhiteCoins;
        collectedWhiteCoin.transform.position = new Vector2(collectedWhitePos.position.x + offset, collectedWhitePos.position.y);
        noOfWhiteCoins += 1;
    }

    public void queenCollected(GameObject collectedQueenCoin)
    {
        collectedQueenCoin.transform.position = new Vector3(0, 0f);
        collectedQueenCoin.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public int CalculateStrikerForce()
    {
        strikerForce = (int)(baseStrikerForce * UIManager.Instance.powerIndicator.fillAmount);
        return strikerForce;
    }

}
