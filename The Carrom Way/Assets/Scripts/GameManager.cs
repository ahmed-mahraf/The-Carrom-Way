using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Players
{
    player1 = 1,
    player2 = 2
}

public class GameManager : MonoBehaviour
{

    public event Action ChangeTurnEvent;
    public event Action<GameObject> OnFoul;
    public static GameManager instance;

    public Transform spawn01, spawn02;
    public GameObject strikerPrefab;
    public GameObject player1, player2;

    private int whosTurn = (int)Players.player1;
    public string getActiveTurn { get { return whosTurn.ToString(); } }
    public int getActivePlayerID { get { return whosTurn; } }

    public int GettotalPieces { get { return 8; } }

    public GameObject getActivePlayer
    {
        get
        {
            if (whosTurn == (int)Players.player1)
                return player1;
            return player2;
        }
    }

    public string getActivePlayerPieceColor
    {
        get
        {
            if (whosTurn == (int)Players.player1)
                return player1.GetComponent<Striker>().puckColor;
            return player2.GetComponent<Striker>().puckColor;
        }
    }



    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.transform);
    }

    private void OnEnable()
    {
        Holes.PuckHole += OnPieceHole;
    }
    private void OnDisable()
    {
        Holes.PuckHole -= OnPieceHole;
    }


    private void Start()
    {
        // instantiating striker and setting the playerID [player 1]
        player1 = Instantiate(strikerPrefab, spawn01.transform.position, Quaternion.identity);
        player1.GetComponent<Striker>().playerID = (int)Players.player1;
        // instantiating striker and setting the playerID [player 2]
        player2 = Instantiate(strikerPrefab, spawn02.transform.position, Quaternion.identity);
        player2.GetComponent<Striker>().playerID = (int)Players.player2;
        // getting player with active turn and disabling other player
        if (whosTurn == (int)Players.player1)
        {
            player2.SetActive(false);
        }
        else
        {
            player1.SetActive(false);
        }

    }

    void OnPieceHole (string piece, int playerID)
    {

        ScoreManager.instance.UpdateScore(piece, playerID);
    }

    // will be called from Striker.cs script
    public void ChangeTurn()
    {
        if (whosTurn == (int)Players.player1)
        {
            player1.SetActive(false);
            player2.SetActive(true);
            whosTurn = (int)Players.player2;
        }
        else
        {
            player2.SetActive(false);
            player1.SetActive(true);
            whosTurn = (int)Players.player1;
        }
        if (ChangeTurnEvent != null)
            ChangeTurnEvent();
    }

    public void Foul(GameObject player)
    {

        if (OnFoul != null)
            OnFoul(player);
    }
}
