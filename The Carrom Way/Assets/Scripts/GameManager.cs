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

    public Transform collectedPuckPos;
    public float offset = 0.5f;
    public int numberOfCoinsCollected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CoinCollected(GameObject collectedCoin)
    {
        collectedCoin.GetComponent<CircleCollider2D>().isTrigger = true;
        collectedCoin.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        offset = 0.5f * numberOfCoinsCollected;
        collectedCoin.transform.position = new Vector2(collectedPuckPos.position.x + offset, collectedPuckPos.position.y);
        numberOfCoinsCollected += 1;
    }

}
