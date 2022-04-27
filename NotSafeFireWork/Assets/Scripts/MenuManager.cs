using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{

    public GameObject creditPanel;
    private bool isCreditsOpened;

    private void Awake()
    {
        creditPanel.SetActive(false);
        isCreditsOpened = false;
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
        isCreditsOpened = true;
    }

    public void ActionQuit()
    {
        Application.Quit();
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        Debug.Log("Cancel");
    }

}
