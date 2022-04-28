using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public PlayerController controller;
    public PlayerLifeSystem lifeSystem;
    [Range(1,2)] public int playerNumber;

    [Space(20)]

    public float lifeDrawn = 1;
    public int ammoDrawn = 3;

    public Image healthBar;
    public List<GameObject> ammoIcons;

    private void FixedUpdate()
    {
        GetHealth();
        GetAmmo();

        DrawHealth();
        DrawAmmo();
    }

    private void GetHealth()
    {
        if(playerNumber == 1)
        {
            lifeDrawn = lifeSystem.playerALife / lifeSystem.playerAMaxLife;
        }
        else
        {
            lifeDrawn = lifeSystem.playerBLife / lifeSystem.playerBMaxLife;
        }
    }

    private void GetAmmo()
    {
        ammoDrawn = (int)controller.fireworkStackActu;
    }

    private void DrawHealth()
    {
        healthBar.fillAmount = lifeDrawn;
    }

    private void DrawAmmo()
    {
        for (int i = 0; i < ammoIcons.Count; i++)
        {
            if (i < ammoDrawn)
            {
                ammoIcons[i].SetActive(true);
            }
            else
            {
                ammoIcons[i].SetActive(false);
            }
        }
    }
}
