using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : Singleton<LevelHandler>
{
    [SerializeField] private Animator animator;

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

                enemyInRoom0.Clear();
                currentList = enemyInRoom0;
                break;

            case 1:
                for (int i = currentList.Count - 1; i >= 0; i--)
                {
                    Destroy(enemyInRoom1[i].gameObject);
                }

                enemyInRoom1.Clear();
                currentList = enemyInRoom1;
                break;

            case 2:
                for (int i = currentList.Count - 1; i >= 0; i--)
                {
                    Destroy(enemyInRoom2[i].gameObject);
                }

                enemyInRoom2.Clear();
                currentList = enemyInRoom2;
                break;

            case 3:
                for (int i = currentList.Count - 1; i >= 0; i--)
                {
                    Destroy(enemyInRoom3[i].gameObject);
                }

                enemyInRoom3.Clear();
                currentList = enemyInRoom3;
                break;

            case 4:
                for (int i = currentList.Count - 1; i >= 0; i--)
                {
                    Destroy(enemyInRoom4[i].gameObject);
                }

                enemyInRoom4.Clear();
                currentList = enemyInRoom4;
                break;

            case 5:
                for (int i = currentList.Count - 1; i >= 0; i--)
                {
                    Destroy(enemyInRoom5[i].gameObject);
                }

                enemyInRoom0.Clear();
                currentList = enemyInRoom5;
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
    }

    //A appeler d�s qu'un enemy meurt.
    public void RoomClearFeedback()
    {
        if (currentList.Count ==0)
        {
            //D�sactiver les inputs du players
            //blockers[currentState].SetActive(false);
            //Trigger le clignotement de la fl�che.
            //Jouer une m�lodie
            //L'input des joueurs se r�activent apr�s 2 clignotements.
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
        //D�sactiver les inputs du players
        //d�sactiver le clignotement de la fl�che.
        //Activer le changement de cam�ra
        //Faire d�placer les personnages
        currentState++;
        CurrentRoom();
        CameraSwitch();

        //A la fin de la transition :
        //Bruit de Gong;
        //Spawn des Ennemis;
        //R�activer les inputs du player;
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
