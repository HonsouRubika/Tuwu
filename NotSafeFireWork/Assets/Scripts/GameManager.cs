using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public int playersJoinedCount = 0;
	[SerializeField] GameObject enemiesGlobalParent;
	public List<PlayerController> playerControllers = new List<PlayerController>();
	public PlayerLifeSystem pls;
	public int playerScore = 0;

	private void Awake()
	{
		CreateSingleton();
		pls = GetComponent<PlayerLifeSystem>();
	}

    public void OnPlayerJoined()
	{
		playersJoinedCount++;

		if(playersJoinedCount == 1)
		{
			playerControllers = new List<PlayerController>(FindObjectsOfType<PlayerController>());
		}

		if(playersJoinedCount == 2)
		{
			playerControllers = new List<PlayerController>(FindObjectsOfType<PlayerController>());

			PlayerUIManager uiManager = playerControllers[1].transform.GetChild(0).GetChild(0).GetComponent<PlayerUIManager>();

			uiManager.playerNumber = 2;
			uiManager.healthBarA.gameObject.SetActive(false);
			uiManager.healthBarB.gameObject.SetActive(true);

			playerControllers[0].transform.position = LevelHandler.Instance.listspawnPointsP1[0].transform.position;
			playerControllers[1].transform.position = LevelHandler.Instance.listspawnPointsP2[0].transform.position;

			EnableFirstRoomEnemies();
		}

		pls.InitLifeSystem((uint)playersJoinedCount);
	}

	void EnableFirstRoomEnemies()
	{
		LevelHandler.Instance.InitRoom();
	}
}