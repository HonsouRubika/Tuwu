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

    public Image healthBarA;
    public Image healthBarB;
    public List<GameObject> ammoIcons;

    private void Start()
    {
        lifeSystem = GameManager.Instance.GetComponent<PlayerLifeSystem>();
    }

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
            lifeDrawn = lifeSystem.playerBLife / lifeSystem.playerBMaxLife;
        }
        else
        {
            lifeDrawn = lifeSystem.playerALife / lifeSystem.playerAMaxLife;
        }
    }

    private void GetAmmo()
    {
        ammoDrawn = (int)controller.fireworkStackActu;
    }

    private void DrawHealth()
    {
        healthBarA.fillAmount = lifeDrawn;
        healthBarB.fillAmount = lifeDrawn;
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
