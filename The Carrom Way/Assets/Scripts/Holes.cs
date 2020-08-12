using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Holes : MonoBehaviour
{
    public static event Action<string, int> PuckHole;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Puck"))
        {
            PiecePotted(collision);
        }
        else if (collision.tag == "Striker")
        {
            GameTracker.instance.AddItem(collision.gameObject, GameManager.instance.getActivePlayerID);
            GameManager.instance.Foul(collision.gameObject);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            StartCoroutine(PotEffect(collision.gameObject));
        }
    }
    void PiecePotted(Collider2D collision)
    {
        //disabling Collider
        Transform piece = collision.gameObject.transform;
        piece.GetComponent<Pucks>().Reset();
        piece.GetComponent<Collider2D>().enabled = false;

        //jamming position
        Vector3 newPos = new Vector3();
        newPos = this.transform.position;
        newPos.z = piece.transform.position.z;
        piece.position = newPos;

        //broadcasting event
        if (PuckHole != null)
            PuckHole(piece.GetComponent<Pucks>().GetColor, GameManager.instance.getActivePlayerID);

        GameTracker.instance.AddItem(piece.gameObject, GameManager.instance.getActivePlayerID);

        if (IsCorrectPot(collision))
        {
            GameManager.instance.getActivePlayer.GetComponent<Striker>().pottedPiece = true;
        }
        else
        {
            GameManager.instance.getActivePlayer.GetComponent<Striker>().pottedPiece = false;
        }
        //hit effect fade
        StartCoroutine(PotEffect(collision.gameObject));
    }

    bool IsCorrectPot(Collider2D collision)
    {
        if (collision.GetComponent<Pucks>().GetColor == GameManager.instance.getActivePlayerPieceColor)
            return true;
        return false;
    }
    IEnumerator PotEffect(GameObject piece)
    {
        SpriteRenderer sr = piece.GetComponent<SpriteRenderer>();
        Color newColor = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a);
        float alpha = 1;
        while (sr.color.a > 0)
        {
            alpha -= Time.deltaTime * 3;
            newColor.a = alpha;
            sr.color = newColor;
            yield return null;
        }

        yield return 0;
    }
}
