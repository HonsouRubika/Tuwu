using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerLifeSystem : MonoBehaviour
{
    public float playerALife;
    public float playerAMaxLife;
    public float playerBLife;
    public float playerBMaxLife;
    public int playersGlobalLife;

    public ParticleSystem reviveFX;

    private GameObject[] players;

    [HideInInspector] Animator playerAAnimator;
    [HideInInspector] Animator playerBAnimator;

    Clock invincibilityTimerA;
    Clock invincibilityTimerB;

    bool canTakeDamageA = true;
    bool canTakeDamageB = true;

    private void Start()
    {
        //init tab
        players = new GameObject[2];

        playerALife = playerAMaxLife;
        playerBLife = playerBMaxLife;
        playersGlobalLife = 3;

        invincibilityTimerA = new Clock();
        invincibilityTimerB = new Clock();

        invincibilityTimerA.ClockEnded += OnInvincibilityTimerEndedA;
        invincibilityTimerB.ClockEnded += OnInvincibilityTimerEndedB;
    }

	private void OnInvincibilityTimerEndedA()
	{
        canTakeDamageA = true;
	}

    private void OnInvincibilityTimerEndedB()
    {
        canTakeDamageB = true;
    }

    public void InitLifeSystem(uint playerID)
    {
        if (playerID == 1)
        {
            players[0] = GameManager.Instance.playerControllers[0].gameObject;
            playerAAnimator = players[0].GetComponent<Animator>();
            reviveFX = GameManager.Instance.playerControllers[0].goodReanimationFeedback.gameObject.GetComponent<ParticleSystem>();
        }
        else
        {
            players[1] = GameManager.Instance.playerControllers[1].gameObject;
            playerBAnimator = players[1].GetComponent<Animator>();
            reviveFX = GameManager.Instance.playerControllers[1].goodReanimationFeedback.gameObject.GetComponent<ParticleSystem>();
        }
    }

    public void PlayerATakeDamage(float _damages)
    {
        if (!canTakeDamageA)
            return;

        SoundManager.Instance.PlaySFX("charaHurt", SoundManager.Instance.fxSource);

        canTakeDamageA = false;
        invincibilityTimerA.SetTime(.6f);

        if(playerALife - _damages < 0)
        {
            playerALife = 0;
        }
        else
        {
            playerALife -= _damages;
            if (playerAAnimator != null) playerAAnimator.SetBool("isHit", true);
        }

        if (playerALife <= 0)
        {
            playerHasBeenDefeated();
        }
    }

    public void PlayerBTakeDamage(float _damages)
    {
        if (!canTakeDamageB)
            return;

        SoundManager.Instance.PlaySFX("charaHurt", SoundManager.Instance.fxSource);

        canTakeDamageB = false;
        invincibilityTimerB.SetTime(.6f);

        if (playerBLife - _damages < 0)
        {
            playerBLife = 0;
        }
        else
        {
            playerBLife -= _damages;
            if (playerBAnimator != null) playerBAnimator.SetBool("isHit", true);
        }

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
                Revive();
                break;
            case 1:
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
        UIManager.Instance.EnableEndScreen(false);
    }
    private IEnumerator ReviveAnimation()
    {
        if (playerALife <= 0)
        {
            GameManager.Instance.playerControllers[0].playerStateActu = 1;
            if (playerAAnimator != null) playerAAnimator.SetBool("isDead", true);
            new WaitForSeconds(0.5f);
            GameManager.Instance.playerControllers[0].playerStateActu = 0;
            reviveFX.Play();
            GameManager.Instance.playerControllers[1].gameObject.transform.position = new Vector3(LevelHandler.Instance.listspawnPointsP2[LevelHandler.currentState].transform.position.x, LevelHandler.Instance.listspawnPointsP2[LevelHandler.currentState].transform.position.y, GameManager.Instance.playerControllers[1].gameObject.transform.position.z);
            playerALife = playerAMaxLife;
        }
        else if (playerBLife <= 0)
        {
            GameManager.Instance.playerControllers[1].playerStateActu = 1;
            if (playerBAnimator != null) playerBAnimator.SetBool("isDead", true);
            new WaitForSeconds(0.5f);
            GameManager.Instance.playerControllers[1].playerStateActu = 0;
            reviveFX.Play();
            GameManager.Instance.playerControllers[0].gameObject.transform.position = new Vector3(LevelHandler.Instance.listspawnPointsP1[LevelHandler.currentState].transform.position.x, LevelHandler.Instance.listspawnPointsP1[LevelHandler.currentState].transform.position.y, GameManager.Instance.playerControllers[0].gameObject.transform.position.z);
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

