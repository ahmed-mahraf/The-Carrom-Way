using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text playerTurnText;
    [Space]
    [Header("Rect Transforms Refrences - Slider")]
    public RectTransform slider;
    public RectTransform sliderP1, sliderP2;
    [Space]
    [Header("Rect Transforms Refrences - TopBar")]
    public RectTransform topBar;
    public RectTransform topBarP1, topBarP2;
    [Space]
    [Header("players Score Texts")]
    public Text player1Score;
    public Text player2Score;
    [Space]
    [Header("players Pieces count Texts")]
    public Text p1_PieceCount;
    public Text p2_PieceCount;
    [Space]
    [Header("players Name Texts")]
    public Text player1Name;
    public Text player2Name;
    [Header("Match Over UI")]
    public GameObject winLoseScreen;
    [Space]
    [Header("Main Screen")]
    public GameObject MainScreen;
    public InputField player1TF;
    public string Getplayer1Name { get; set; }
    public InputField player2TF;
    public string Getplayer2Name { get; set; }
    public Button doneBtn;
    [Space]
    [Header("Win Screen")]
    public Text winnerName;
    public Text looserName;
    public Text winnerScore;
    public Text looserScore;

    void Start()
    {

        ScoreManager.instance.ScoreUpdated += UpdateScoreUI;
        ScoreManager.instance.MatchOver += MatchOver;
        GameManager.instance.ChangeTurnEvent += UpdateTurn;
        GameManager.instance.OnFoul += Foul;
        GameTracker.pieceBackOnBoard += PieceBackonBoard;
        UpdateTurn();

        if (Settings.instance.isPracticeMode)
            PracticeMode_HideUI();
    }
    #region UI_UPDATION
    private void PracticeMode_HideUI()
    {
        player2Score.GetComponent<CanvasGroup>().alpha = 0;
        p2_PieceCount.GetComponent<CanvasGroup>().alpha = 0;
        player2Name.GetComponent<CanvasGroup>().alpha = 0;
    }

    private void PieceBackonBoard(GameObject piece)
    {
        print("Piece " + piece.name + " is back on board due to FOUL");
    }

    private void MatchOver(int playerID)
    {
        //player 1 wins
        if (ScoreManager.instance.PlayerOneScore > ScoreManager.instance.PlayerTwoScore)
        {
            winnerName.text = player1Name.text;
            looserName.text = player2Name.text;
            winnerScore.text = ScoreManager.instance.PlayerOneScore.ToString();
            looserScore.text = ScoreManager.instance.PlayerTwoScore.ToString();
        } //player 2 wins
        else if (ScoreManager.instance.PlayerOneScore < ScoreManager.instance.PlayerTwoScore)
        {
            winnerName.text = player2Name.text;
            looserName.text = player1Name.text;
            winnerScore.text = ScoreManager.instance.PlayerTwoScore.ToString();
            looserScore.text = ScoreManager.instance.PlayerOneScore.ToString();
        }// DRAW same score
        else
        {
            winnerName.text = "D R A W";
            looserName.text = "D R A W";
        }

        winLoseScreen.SetActive(true);
    }

    private void Foul(GameObject player)
    {
        Popup.instance.showPopup();
        if (player.GetComponent<Striker>().playerID == (int)Players.player1)
        {

        }
        else
        {

        }
    }

    void UpdateTurn()
    {

        playerTurnText.text = "Player " + GameManager.instance.getActiveTurn + " Turns.";
        if (GameManager.instance.getActivePlayerID == (int)Players.player1)
        {

            slider.anchoredPosition = sliderP1.anchoredPosition;
            slider.sizeDelta = sliderP1.sizeDelta;
            slider.eulerAngles = sliderP1.eulerAngles;
            slider.anchorMax = sliderP1.anchorMax;
            slider.anchorMin = sliderP1.anchorMin;

            if (Settings.instance.rePositionHUD)
            {
                topBar.anchoredPosition = topBarP1.anchoredPosition;
                topBar.sizeDelta = topBarP1.sizeDelta;
                topBar.eulerAngles = topBarP1.eulerAngles;
                topBar.anchorMax = topBarP1.anchorMax;
                topBar.anchorMin = topBarP1.anchorMin;
            }
        }
        else
        {
            slider.anchoredPosition = sliderP2.anchoredPosition;
            slider.sizeDelta = sliderP2.sizeDelta;
            slider.eulerAngles = sliderP2.eulerAngles;
            slider.anchorMax = sliderP2.anchorMax;
            slider.anchorMin = sliderP2.anchorMin;

            if (Settings.instance.rePositionHUD)
            {
                topBar.anchoredPosition = topBarP2.anchoredPosition;
                topBar.sizeDelta = topBarP2.sizeDelta;
                topBar.eulerAngles = topBarP2.eulerAngles;
                topBar.anchorMax = topBarP2.anchorMax;
                topBar.anchorMin = topBarP2.anchorMin;

                Vector3 adJustPosition = new Vector3();
                adJustPosition = topBar.position;
                adJustPosition.y -= adJustPosition.y;
                topBar.position = adJustPosition;
            }

        }

    }

    // event
    void UpdateScoreUI()
    {
        player1Score.text = ScoreManager.instance.PlayerOneScore.ToString();
        player2Score.text = ScoreManager.instance.PlayerTwoScore.ToString();

        p1_PieceCount.text = string.Format("0{0} / 08", ScoreManager.instance.whitePotted.ToString());
        p2_PieceCount.text = string.Format("0{0} / 08", ScoreManager.instance.blackPotted.ToString());

    }

    #endregion

    #region BUTTON_CLICKS_FUNCTIONS

    public void OnClick_DoneBtn()
    {
        player1Name.text = player1TF.text;
        player2Name.text = player2TF.text;

    }

    #endregion
}
