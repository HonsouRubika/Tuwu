using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : Singleton<LevelHandler>
{
    [SerializeField] private Animator animator;

    [SerializeField] private GameObject blockerParent;
    [SerializeField] private GameObject[] blockers;

    [SerializeField] private List<GameObject> roomList = new List<GameObject>();

    [SerializeField] private List<Enemy> currentList = new List<Enemy>();
    [SerializeField] private List<Enemy> enemyInRoom0 = new List<Enemy>();
    [SerializeField] private List<Enemy> enemyInRoom1 = new List<Enemy>();
    [SerializeField] private List<Enemy> enemyInRoom2 = new List<Enemy>();
    [SerializeField] private List<Enemy> enemyInRoom3 = new List<Enemy>();
    [SerializeField] private List<Enemy> enemyInRoom4 = new List<Enemy>();
    [SerializeField] private List<Enemy> enemyInRoom5 = new List<Enemy>();

    public static int currentState = 0;

    private int initInit;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            roomList.Add(child.gameObject);
        }

        currentList = enemyInRoom0;
        InitRoom();
    }

    public void ClearRoom()
    {
        switch (currentState)
        {
            case 0:
                for (int i = currentList.Count-1; i >= 0; i--)
                {
                    Destroy(enemyInRoom0[i].gameObject);
                }
                blockers[currentState].SetActive(false);
                enemyInRoom0.Clear();
                currentList = enemyInRoom0;
                break;

            case 1:
                for (int i = currentList.Count - 1; i >= 0; i--)
                {
                    Destroy(enemyInRoom1[i].gameObject);
                }
                blockers[currentState].SetActive(false);
                enemyInRoom1.Clear();
                currentList = enemyInRoom1;
                break;

            case 2:
                for (int i = currentList.Count - 1; i >= 0; i--)
                {
                    Destroy(enemyInRoom2[i].gameObject);
                }
                blockers[currentState].SetActive(false);
                enemyInRoom2.Clear();
                currentList = enemyInRoom2;
                break;

            case 3:
                for (int i = currentList.Count - 1; i >= 0; i--)
                {
                    Destroy(enemyInRoom3[i].gameObject);
                }
                blockers[currentState].SetActive(false);
                enemyInRoom3.Clear();
                currentList = enemyInRoom3;
                break;

            case 4:
                for (int i = currentList.Count - 1; i >= 0; i--)
                {
                    Destroy(enemyInRoom4[i].gameObject);
                }
                blockers[currentState].SetActive(false);
                enemyInRoom4.Clear();
                currentList = enemyInRoom4;
                break;

            case 5:
                for (int i = currentList.Count - 1; i >= 0; i--)
                {
                    Destroy(enemyInRoom5[i].gameObject);
                }
                blockers[currentState].SetActive(false);
                enemyInRoom0.Clear();
                currentList = enemyInRoom5;
                break;
            default:
                break;
        }
    }

    public void InitRoom()
    {
        switch (currentState)       
        {
            case 0:
                for (int i = 0; i <= currentList.Count - 1; i++)
                {
                    enemyInRoom0[i].gameObject.SetActive(true);
                }
                break;

            case 1:
                for (int i = 0; i <= currentList.Count - 1; i++)
                {
                    enemyInRoom1[i].gameObject.SetActive(true);
                }
                break;

            case 2:
                for (int i = 0; i <= currentList.Count - 1; i++)
                {
                    enemyInRoom2[i].gameObject.SetActive(true);
                }
                break;

            case 3:
                for (int i = 0; i <= currentList.Count - 1; i++)
                {
                    enemyInRoom3[i].gameObject.SetActive(true);
                }
                break;

            case 4:
                for (int i = 0; i <= currentList.Count - 1; i++)
                {
                    enemyInRoom4[i].gameObject.SetActive(true);
                }
                break;
            case 5:
                for (int i = 0; i <= currentList.Count - 1; i++)
                {
                    enemyInRoom5[i].gameObject.SetActive(true);
                }
                break;
            default:
                break;
        }

        
    }

    void CurrentRoom()
    {
        switch (currentState)
        {
            case 0:
                currentList = enemyInRoom0;
                break;
            case 1:
                currentList = enemyInRoom1;
                break;
            case 2:
                currentList = enemyInRoom2;
                break;
            case 3:
                currentList = enemyInRoom3;
                break;
            case 4:
                currentList = enemyInRoom4;
                break;
            case 5:
                currentList = enemyInRoom5;
                break;
            default:
                break;
        }
        InitRoom();
    }

    //A appeler dès qu'un enemy meurt.
    public void RoomClearFeedback()
    {
        if (currentList.Count ==0)
        {
            //Désactiver les inputs du players
            blockers[currentState].SetActive(false);
            //Trigger le clignotement de la flèche.
            //Jouer une mélodie
            //L'input des joueurs se réactivent après 2 clignotements.
            ChangeRoomTrigger();
        }
        else
        {
            Debug.Log("Still Enemy");
            //Still Enemy;
        }
    }

   //A appeler lorsque les joueurs veulent changer de salle.
    void ChangeRoomTrigger()
    {
        //Désactiver les inputs du players
        //désactiver le clignotement de la flèche.
        //Activer le changement de caméra
        //Faire déplacer les personnages
        currentState++;
        CurrentRoom();
        CameraSwitch();

        //A la fin de la transition :
        //Bruit de Gong;
        //Spawn des Ennemis;
        //Réactiver les inputs du player;
    }

    void CameraSwitch()
    {
        switch (currentState)
        {
            case 1:
                animator.SetInteger("CurrentState", currentState);
                break;
            case 2:
                animator.SetInteger("CurrentState", currentState);
                break;
            case 3:
                animator.SetInteger("CurrentState", currentState);
                break;
            case 4:
                animator.SetInteger("CurrentState", currentState);
                break;
            case 5:
                animator.SetInteger("CurrentState", currentState);
                break;
            default:
                break;
        }
    }
  
}
