using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{

    public GameObject creditPanel;
    public GameObject playButton;
    public GameObject backButton;

    private void Awake()
    {
        creditPanel.SetActive(false);
    }

    public void ActionPlay()
    {
        SceneManager.LoadScene("XP_LevelGestion");
    }

    public void ActionCredits()
    {
        creditPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(backButton);
    }

    public void ActionQuit()
    {
        Application.Quit();
    }

    public void ActionBack()
    {
        creditPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(playButton);
    }

}
