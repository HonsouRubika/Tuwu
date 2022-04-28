using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public PlayerLifeSystem lifeSystem;

    [Space(20)]

    public int scoreDrawn = 0;
    public int reviveDrawn = 3;

    public TextMeshProUGUI scoreText;
    public List<GameObject> reviveIcons;

    private void FixedUpdate()
    {
        GetRevive();

        DrawScore();
        DrawRevive();        
    }

    private void GetRevive()
    {
        reviveDrawn = lifeSystem.playersGlobalLife;
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
}
