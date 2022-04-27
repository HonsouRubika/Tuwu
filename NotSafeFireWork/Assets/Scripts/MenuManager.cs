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
        // TODO : Replace TestScene here by GameScene when ready
        // SceneManager.LoadScene("TestScene");
        Debug.Log("Link la scene de jeu dans le code (script MenuManager)");
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
