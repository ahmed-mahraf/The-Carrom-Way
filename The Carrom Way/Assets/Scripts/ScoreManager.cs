using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    //UIManager is subscribed to event
    public event Action ScoreUpdated;
    public event Action<int> MatchOver;
    public static ScoreManager instance;

    public int PlayerOneScore { get; set; }
    public int PlayerTwoScore { get; set; }
    //...
    public int whitePotted { get; private set; }
    public int blackPotted { get; private set; }
    public int GetTotalPotted { get { return whitePotted + blackPotted; } }

    readonly int scorePerPot = 5;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);


    }
    private void OnEnable()
    {
        GameTracker.pieceBackOnBoard += PieceBackonBoard;
        GameManager.instance.OnFoul += OnFoul;
    }

    private void OnFoul(GameObject player)
    {

    }

    private void PieceBackonBoard(GameObject piece)
    {

        if (piece.GetComponent<Pucks>().GetColor == "White")
        {
            whitePotted--;
            PlayerOneScore -= scorePerPot;
        }
        else
        {
            blackPotted--;
            PlayerTwoScore -= scorePerPot;
        }
        if (ScoreUpdated != null)
            ScoreUpdated();
    }
    public void FoulNoPot(int playerID)
    {
        DeductScore((Players)playerID);
    }

    private void DeductScore(Players playerID)
    {
        if (playerID == Players.player1)
        {
            PlayerOneScore -= scorePerPot;
        }
        else
        {
            PlayerTwoScore -= scorePerPot;
        }
        if (ScoreUpdated != null)
            ScoreUpdated();
    }
    public void UpdateScore(string piece, int playerID)
    {
        if (playerID == (int)Players.player1 && piece == "White")
        {
            //player 1 point
            AddScore(Players.player1);
        }
        else if (playerID == (int)Players.player1 && piece == "Black")
        {
            // player 2 point
            AddScore(Players.player2);
        }
        else if (playerID == (int)Players.player2 && piece == "Black")
        {
            // player 2 point
            AddScore(Players.player2);
        }
        else if (playerID == (int)Players.player2 && piece == "White")
        {
            // player 1 point
            AddScore(Players.player1);

        }

        if (ScoreUpdated != null)
            ScoreUpdated();
    }

    void AddScore(Players playerID)
    {
        if (playerID == Players.player1)
        {
            PlayerOneScore += scorePerPot;
            whitePotted++;
        }
        else
        {
            blackPotted++;
            PlayerTwoScore += scorePerPot;
        }

        if (whitePotted == GameManager.instance.GettotalPieces)
        {
            MatchOver?.Invoke((int)Players.player1);
        }
        else if (blackPotted == GameManager.instance.GettotalPieces)
        {
            MatchOver?.Invoke((int)Players.player2);
        }
    }
}
