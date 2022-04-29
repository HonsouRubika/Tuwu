using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIManager : Singleton<UIManager>
{
    private GameManager gameManager;
    public PlayerLifeSystem lifeSystem;

    [Space]

    [Header("HUD Variables")]
    public int scoreDrawn = 0;
    public int reviveDrawn = 3;
    public bool isHudActive = true;

    [Header("HUD Elements")]
    public GameObject hudCanvas;
    public TextMeshProUGUI scoreText;
    public List<GameObject> reviveIcons;

    [Space]

    [Header("End Screen Variables")]
    public GameObject endCanvas;
    public int endScoreDrawn = 0;
    public bool isEndScreenActive = false;

    [Header("End Screen Elements")]
    public GameObject victoryImage;
    public GameObject defeatImage;
    public GameObject returnButton;
    public TextMeshProUGUI endScoreText;

    private void Awake()
    {
        CreateSingleton();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        hudCanvas.SetActive(true);
        endCanvas.SetActive(false);
    }

    private void FixedUpdate()
    {
        if(isHudActive)
        {
            GetRevive();
            GetScore();

            DrawScore();
            DrawRevive();        
        }

        if(isEndScreenActive)
        {
            if (endScoreDrawn < gameManager.playerScore)
            {
                endScoreDrawn += 4;

                if (endScoreDrawn > gameManager.playerScore)
                {
                    endScoreDrawn = gameManager.playerScore;
                }

                scoreText.text = "" + endScoreDrawn;
            }
        }
    }

    private void GetRevive()
    {
        reviveDrawn = lifeSystem.playersGlobalLife;
    }

    private void GetScore()
    {
        if(scoreDrawn < gameManager.playerScore) {
            scoreDrawn += 1;
        }

        if (scoreDrawn > gameManager.playerScore)
        {
            scoreDrawn -= 1;
        }
    }

    private void DrawScore()
    {
        scoreText.text = "" + scoreDrawn;
    }

    private void DrawRevive()
    {
        for(int i = 0; i<reviveIcons.Count; i++)
        {
            if(i<reviveDrawn)
            {
                reviveIcons[i].SetActive(true);
            }
            else
            {
                reviveIcons[i].SetActive(false);
            }
        }
    }

    public void EnableEndScreen(bool isVictory)
    {
        isHudActive = false;
        isEndScreenActive = true;

        hudCanvas.SetActive(false);
        endCanvas.SetActive(true);

        if(isVictory)
        {
            victoryImage.SetActive(true);
            defeatImage.SetActive(false);
        }
        else
        {
            victoryImage.SetActive(false);
            defeatImage.SetActive(true);
        }

        EventSystem.current.SetSelectedGameObject(returnButton);
    }

    public void ActionBackToMainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
