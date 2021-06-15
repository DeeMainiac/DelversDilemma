using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// functions for the main menu, such as loading scenes and managing menus.
/// </summary>
public class MainMenuFuncts : MonoBehaviour
{
    [SerializeField] GameObject titleImg;
    [SerializeField] GameObject mainButtons;

    [SerializeField] GameObject backButton;

    [SerializeField] GameObject creditsWindow;

    public void PlayGame() {
        SceneManager.LoadScene(1);
    }

    public void Options() {
        titleImg.SetActive(false);
        mainButtons.SetActive(false);

        backButton.SetActive(true);
    }


    public void Credits() {
        titleImg.SetActive(false);
        mainButtons.SetActive(false);

        creditsWindow.SetActive(true);
        backButton.SetActive(true);
    }

    public void Back() {

        creditsWindow.SetActive(false);
        backButton.SetActive(false);

        titleImg.SetActive(true);
        mainButtons.SetActive(true);

    }

    public void Exit() {
        Application.Quit();
    }


}
