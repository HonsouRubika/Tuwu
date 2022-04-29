using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : Singleton<LevelHandler>
{
    private SoundManager soundManager;

    [SerializeField] private Animator animator;

    [SerializeField] private GameObject blockerParent;
    [SerializeField] private GameObject[] blockers;

    [SerializeField] private List<GameObject> roomList = new List<GameObject>();

    [SerializeField] private List<GameObject> currentList = new List<GameObject>();

    [SerializeField] private GameObject enemyParent0;
    [SerializeField] private List<GameObject> enemyInRoom0 = new List<GameObject>();
    [SerializeField] private GameObject enemyParent1;
    [SerializeField] private List<GameObject> enemyInRoom1 = new List<GameObject>();
    [SerializeField] private GameObject enemyParent2;
    [SerializeField] private List<GameObject> enemyInRoom2 = new List<GameObject>();
    [SerializeField] private GameObject enemyParent3;
    [SerializeField] private List<GameObject> enemyInRoom3 = new List<GameObject>();
    [SerializeField] private GameObject enemyParent4;
    [SerializeField] private List<GameObject> enemyInRoom4 = new List<GameObject>();
    [SerializeField] private GameObject enemyParent5;
    [SerializeField] private List<GameObject> enemyInRoom5 = new List<GameObject>();

    [SerializeField] private GameObject spawnPointP1;
    public List<GameObject> listspawnPointsP1 = new List<GameObject>();
    [SerializeField] private GameObject SspawnPointP2;
    public List<GameObject> listspawnPointsP2 = new List<GameObject>();


    [SerializeField] private GameObject arrowParent;
    [SerializeField] private List<GameObject> arrows = new List<GameObject>();
    public bool roomfinished = false;
    public GameObject collisionCollider;

    public static int currentState = 0;

    private int initInit;

    private void Awake()
    {

        foreach (Transform child in spawnPointP1.transform)
        {
            listspawnPointsP1.Add(child.gameObject);
        }


        foreach (Transform child in SspawnPointP2.transform)
        {
            listspawnPointsP2.Add(child.gameObject);
        }


        foreach (Transform child in transform)
        {
            roomList.Add(child.gameObject);
        }

        foreach (Transform child in arrowParent.transform)
        {
            arrows.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }

        EnemyInit();
        currentList = enemyInRoom0;
    }

	private void Start()
	{
        soundManager = SoundManager.Instance;
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

    void EnemyInit()
    {
        enemyInRoom0.Clear();
        foreach (Transform item in enemyParent0.gameObject.transform)
        {
            enemyInRoom0.Add(item.gameObject);
            item.gameObject.SetActive(false);
        }

            enemyInRoom1.Clear();
        foreach (Transform item in enemyParent1.gameObject.transform)
        {
            enemyInRoom1.Add(item.gameObject);
            item.gameObject.SetActive(false);
        }

            enemyInRoom2.Clear();
        foreach (Transform item in enemyParent2.gameObject.transform)
        {
            enemyInRoom2.Add(item.gameObject);
            item.gameObject.SetActive(false);
        }

            enemyInRoom3.Clear();
        foreach (Transform item in enemyParent3.transform)
        {
            enemyInRoom3.Add(item.gameObject);
            item.gameObject.SetActive(false);
        }

            enemyInRoom4.Clear();
        foreach (Transform item in enemyParent4.transform)
        {
            enemyInRoom4.Add(item.gameObject);
            item.gameObject.SetActive(false);
        }

            enemyInRoom5.Clear();
        foreach (Transform item in enemyParent5.transform)
        {
            enemyInRoom5.Add(item.gameObject);
            item.gameObject.SetActive(false);
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
                currentList = enemyInRoom1;
                for (int i = 0; i <= currentList.Count - 1; i++)
                {
                    enemyInRoom1[i].gameObject.SetActive(true);
                }
                break;

            case 2:
                currentList = enemyInRoom2;
                for (int i = 0; i <= currentList.Count - 1; i++)
                {
                    enemyInRoom2[i].gameObject.SetActive(true);
                }
                break;

            case 3:
                currentList = enemyInRoom3;
                for (int i = 0; i <= currentList.Count - 1; i++)
                {
                    enemyInRoom3[i].gameObject.SetActive(true);
                }
                break;

            case 4:
                currentList = enemyInRoom4;
                for (int i = 0; i <= currentList.Count - 1; i++)
                {
                    enemyInRoom4[i].gameObject.SetActive(true);
                }
                break;
            case 5:
                currentList = enemyInRoom5;
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
    public void RoomClearFeedback(GameObject monsterkilled)
    {
        currentList.Remove(monsterkilled);

        if (currentList.Count ==0)
        {
            collisionCollider.GetComponent<BoxCollider>().enabled = true; 

            blockers[currentState].SetActive(false);
            
            arrows[currentState].SetActive(true);

            

            //Trigger le clignotement de la flèche.
            lerpCoroutine = StartCoroutine(LerpValue());
        }
    }

    //Clignotements de la flèche.
    Coroutine lerpCoroutine;

    public IEnumerator LerpValue()
    {
       
        Debug.Log("Started");
        float _elapsedTime = 0f;
        float _completion = 0f;
        float wantedTime = 0.5f;

        while (_elapsedTime < wantedTime)
        {
            _elapsedTime += Time.deltaTime;
            _completion = _elapsedTime / (wantedTime);
            arrows[currentState].gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_emissiveIntensity", Mathf.Lerp(1.5f, 1, _completion));
            yield return new WaitForEndOfFrame();
        }
        
       lerpCoroutine = StartCoroutine(LerpValueReturn()); 
    }

    public IEnumerator LerpValueReturn()
    {

        float _elapsedTime = 0f;
        float _completion = 0f;
        float wantedTime = 1f;

        while (_elapsedTime < wantedTime)
        {
            _elapsedTime += Time.deltaTime;
            _completion = _elapsedTime / (wantedTime);
            arrows[currentState].gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_emissiveIntensity", Mathf.Lerp(1f, 1.5f, _completion));
            yield return new WaitForEndOfFrame();
        }

        lerpCoroutine = StartCoroutine(LerpValue());
    }

    //A appeler lorsque les joueurs veulent changer de salle.
    public void ChangeRoomTrigger()
    {
        collisionCollider.GetComponent<BoxCollider>().enabled = false;

        //désactiver le clignotement de la flèche.
        if (lerpCoroutine != null)
        {
            StopCoroutine(lerpCoroutine);
        }    
        
        //Couper les inputs des players.
        foreach (var item in GameManager.Instance.playerControllers)
        {
            item.playerStateActu = (int)PlayerController.PlayerState.stun;
        }

        CurrentRoom();

        //Spawner les fumigènes à l'emplacement des players.
        foreach(var item in GameManager.Instance.playerControllers)
        {
            item.gameObject.SetActive(false);
            //Instancier les fummées.
            GameObject spawnedItem = Instantiate(item.gameObject.GetComponent<PlayerController>().goodReanimationFeedback, item.gameObject.transform.position,Quaternion.identity);
        }

        //Activer le changement de caméra
        StartCoroutine(ChangeRoom());
    }

    private IEnumerator ChangeRoom()
    {
        CameraSwitch();
        Debug.Log("ChangeRoom");
        
        yield return new WaitForSeconds(0.5f);
        

        //A la fin de la transition :
        arrows[currentState - 1].SetActive(false);


        //Teléporter les personnages

        GameManager.Instance.playerControllers[0].gameObject.transform.position = new Vector3( listspawnPointsP1[currentState].transform.position.x,listspawnPointsP1[currentState].transform.position.y, GameManager.Instance.playerControllers[0].gameObject.transform.position.z);
        GameManager.Instance.playerControllers[1].gameObject.transform.position = new Vector3(listspawnPointsP2[currentState].transform.position.x, listspawnPointsP2[currentState].transform.position.y, GameManager.Instance.playerControllers[1].gameObject.transform.position.z);
        foreach (var item in GameManager.Instance.playerControllers)
        {
            item.LinkCorners();
            Instantiate(item.gameObject.GetComponent<PlayerController>().goodReanimationFeedback, item.gameObject.transform);
            item.gameObject.SetActive(true);
            //Instancier les fummées.
        }

        //Bruit de Gong;
        soundManager.PlaySFX("gongStartLevel", soundManager.fxSource);

        //Faire apparaitre le message FIGHT.

        //Réactiver les inputs du player;
        foreach (var item in GameManager.Instance.playerControllers)
        {
            item.playerStateActu = (int)PlayerController.PlayerState.active;
        }
        InitRoom();
        yield return null;
    } 

    void CameraSwitch()
    {
        currentState++;
        Debug.Log(currentState);
        
        switch (currentState)
        {
            case 1:
                animator.SetInteger("CurrentState", currentState);
                blockers[currentState].SetActive(true);
                blockers[currentState-1].SetActive(true);
                break;
            case 2:
                animator.SetInteger("CurrentState", currentState);
                blockers[currentState].SetActive(true);
                blockers[currentState-1].SetActive(true);
                break;
            case 3:
                animator.SetInteger("CurrentState", currentState);
                blockers[currentState].SetActive(true);
                blockers[currentState - 1].SetActive(true);
                break;
            case 4:
                animator.SetInteger("CurrentState", currentState);
                blockers[currentState].SetActive(true);
                blockers[currentState - 1].SetActive(true);
                break;
            case 5:
                animator.SetInteger("CurrentState", currentState);
                blockers[currentState].SetActive(true);
                blockers[currentState - 1].SetActive(true);
                break;
            default:
                break;
        }
    }
  
}
