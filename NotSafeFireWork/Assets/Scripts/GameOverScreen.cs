using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public GameObject returnButton;
    public TextMeshProUGUI scoreText;
    private int playerScore = 0;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(returnButton);
        scoreText.text = "" + playerScore;
    }

    private void OnEnable()
    {
        playerScore = PlayerPrefs.GetInt("score");
    }

    public void ActionBackToMainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
