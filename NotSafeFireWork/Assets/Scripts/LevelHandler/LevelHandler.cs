using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private GameObject[] blockers;

    [SerializeField] private List<GameObject> roomList = new List<GameObject>();

    [SerializeField] private List<Enemy> enemyInRoom0 = new List<Enemy>();
    [SerializeField] private List<Enemy> enemyInRoom1 = new List<Enemy>();
    [SerializeField] private List<Enemy> enemyInRoom2 = new List<Enemy>();
    [SerializeField] private List<Enemy> enemyInRoom3 = new List<Enemy>();
    [SerializeField] private List<Enemy> enemyInRoom4 = new List<Enemy>();
    [SerializeField] private List<Enemy> enemyInRoom5 = new List<Enemy>();

    public static int currentState = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        foreach (Transform child in transform)
        {
            roomList.Add(child.gameObject);
        }
    }

    //A appeler dès qu'un enemy meurt.
    public void RoomClearFeedback()
    {
        if (roomList[currentState] ==null)
        {
            //Désactiver les inputs du players
            blockers[currentState].SetActive(false);
            //Trigger le clignotement de la flèche.
            //Jouer une mélodie
            //L'input des joueurs se réactivent après 2 clignotements.
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
