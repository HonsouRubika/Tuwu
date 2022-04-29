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
    public int playerScore = 0;
    private int playerScoreDrawn = 0;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(returnButton);
    }

    private void FixedUpdate()
    {
        if(playerScoreDrawn < playerScore)
        {
            playerScoreDrawn += 4;
            
            if (playerScoreDrawn > playerScore)
            {
                playerScoreDrawn = playerScore;
            }

            scoreText.text = "" + playerScoreDrawn;
        }
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
