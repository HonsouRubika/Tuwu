using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLifeSystem : MonoBehaviour
{
    public float playerALife;
    public float playerAMaxLife;
    public float playerBLife;
    public float playerBMaxLife;
    public int playersGlobalLife;

    public Image playersGlobalLifeSymbolA;
    public Image playersGlobalLifeSymbolB;
    public Image playersGlobalLifeSymbolC;

    public ParticleSystem reviveFX;

    private GameObject[] players;

    [HideInInspector] Animator playerAAnimator;
    [HideInInspector] Animator playerBAnimator;

    private void Awake()
    {
        players[0] = GameManager.Instance.playerControllers[0].gameObject;
        playerAAnimator = players[0].GetComponent<Animator>();
        players[1] = GameManager.Instance.playerControllers[1].gameObject;
        playerBAnimator = players[1].GetComponent<Animator>();
    }

    private void Start()
    {
        playerALife = playerAMaxLife;
        playerBLife = playerBMaxLife;
        playersGlobalLife = 3;
        playersGlobalLifeSymbolA.enabled = true;
        playersGlobalLifeSymbolB.enabled = true;
        playersGlobalLifeSymbolC.enabled = true;
    }

    public void PlayerATakeDamage()
    {
        //playerALife - dégâts du feu d'artifice l'ayant touché
        if(playerALife <= 0)
        {
            playerHasBeenDefeated();
        }
    }

    public void PlayerBTakeDamage()
    {
        //playerBLife - dégâts du feu d'artifice l'ayant touché
        if (playerBLife <= 0)
        {
            playerHasBeenDefeated();
        }
    }

    private void playerHasBeenDefeated()
    {
        playersGlobalLife--;

        switch (playersGlobalLife)
        {
            case 2:
                playersGlobalLifeSymbolA.enabled = false;
                playersGlobalLifeSymbolB.enabled = true;
                playersGlobalLifeSymbolC.enabled = true;
                Revive();
                break;
            case 1:
                playersGlobalLifeSymbolA.enabled = false;
                playersGlobalLifeSymbolB.enabled = false;
                playersGlobalLifeSymbolC.enabled = true;
                Revive();
                break;
            case 0:
                GameOver();
                break;
            default:
                throw new System.ArgumentException("case not valid");
        }
    }

    private void GameOver()
    {
        //Envoyer à l'écran de death. Rajouter une coroutine avant pour jouer les anims ?
        StartCoroutine(ToScreenDeath());
    }

    private void Revive()
    {
        StartCoroutine(ReviveAnimation());
    }

    private IEnumerator ToScreenDeath()
    {
        if(playerALife <= 0)
        {
            if (playerAAnimator != null) playerAAnimator.SetBool("isDead", true);
        }
        else if(playerBLife <= 0)
        {
            if (playerBAnimator != null) playerBAnimator.SetBool("isDead", true);
        }
        else
        {
            yield return null;
        }
        new WaitForSeconds(0.5f);
        yield return null;
        SceneManager.LoadScene("Death Screen");
    }
    private IEnumerator ReviveAnimation()
    {
        if (playerALife <= 0)
        {
            if (playerAAnimator != null) playerAAnimator.SetBool("isDead", true);
            new WaitForSeconds(0.5f);
            reviveFX.Play();
            playerALife = playerAMaxLife;
        }
        else if (playerBLife <= 0)
        {
            if (playerBAnimator != null) playerBAnimator.SetBool("isDead", true);
            new WaitForSeconds(0.5f);
            reviveFX.Play();
            playerBLife = playerBMaxLife;
        }
        else
        {
            yield return null;
        }
        new WaitForSeconds(0.5f);
        yield return null;
    }
}
