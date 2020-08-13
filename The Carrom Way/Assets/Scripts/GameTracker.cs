using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameTracker : MonoBehaviour
{
    public static event Action<GameObject> pieceBackOnBoard;
    public static GameTracker instance;

    public List<Tracker> trackerList;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    private void Start()
    {
        trackerList = new List<Tracker>();
    }


    public void AddItem(GameObject piece, int playerID)
    {
        Tracker obj;
        obj.playerID = playerID;
        obj.piece = piece;
        trackerList.Add(obj);
    }

    public void ClearList()
    {
        trackerList.Clear();
    }

    public void ReplaceFouledPiece()
    {
        bool isFoul = false;
        int playerWhoFouledID = 0;
        foreach (Tracker t in trackerList)
        {
            if (t.piece.name.Contains("Striker"))
            {
                playerWhoFouledID = t.piece.GetComponent<Striker>().playerID;
                isFoul = true;
                break;
            }
        }

        if (!isFoul)
            return;

        int counter = 0;

        foreach (Tracker t in trackerList)
        {

            if (t.piece.name.Contains("Piece"))
            {
                counter++;
                PutPieceBackOnBoard(t.piece);
            }
        }

        print("counter " + counter);
        if (counter == 0)
        {
            ScoreManager.instance.FoulNoPot(playerWhoFouledID);
        }
    }

    public void PutPieceBackOnBoard(GameObject piece)
    {
        //x -4.82  -2.85 //y 1.5 -0.55
        float xPos = UnityEngine.Random.Range(-4.82f, -2.82f);
        float yPos = UnityEngine.Random.Range(-0.55f, 1.5f);

        Vector3 position = new Vector3(xPos, yPos, piece.transform.position.z);
        piece.transform.localPosition = position;
        piece.GetComponent<Collider2D>().enabled = true;
        SpriteRenderer sr = piece.GetComponent<SpriteRenderer>();
        Color newColor = new Color(sr.color.r, sr.color.g, sr.color.b, 1.0f);
        sr.color = newColor;

        pieceBackOnBoard?.Invoke(piece);
    }
}

public struct Tracker
{
    public GameObject piece;
    public int playerID;
}