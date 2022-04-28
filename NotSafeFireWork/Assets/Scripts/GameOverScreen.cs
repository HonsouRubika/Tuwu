using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameOverScreen : MonoBehaviour
{
    public GameObject returnButton;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(returnButton);
    }

    public void ActionBackToMainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
