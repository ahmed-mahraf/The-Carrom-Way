using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holes : MonoBehaviour
{
    //public Striker striker;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Pucks") // If object is tagged pucks, then collect pucks
        {
            GameManager.Instance.CoinCollected(collider.gameObject);
        }
        if (collider.gameObject.tag == "Striker") // If object is tagged striker, then reset striker
        {
            collider.transform.position = new Vector3(0, 3f);
            //striker.resetStriker();
        }
    }
}