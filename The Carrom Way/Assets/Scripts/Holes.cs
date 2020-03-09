using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holes : MonoBehaviour
{
    //public Striker striker;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Black") // If object is tagged pucks, then collect pucks
        {
            GameManager.Instance.blackCoinCollected(collider.gameObject);
        }

        if (collider.gameObject.tag == "White")
        {
            GameManager.Instance.whiteCoinCollected(collider.gameObject);
        }

        if (collider.gameObject.tag == "Queen")
        {
            GameManager.Instance.queenCollected(collider.gameObject);
        }

        if (collider.gameObject.tag == "Striker") // If object is tagged striker, then reset striker
        {
            GameManager.Instance.striker.resetStriker();
        }
    }
}