using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainScreenUI;
    public GameObject winLoseScreen;

    public void HideMainScreen()
    {
        MainScreenUI.SetActive(false);
    }

    public void ShowMainScreen()
    {
        MainScreenUI.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
