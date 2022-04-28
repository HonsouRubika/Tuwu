using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public int playersJoinedCount = 0;
	[SerializeField] GameObject enemiesGlobalParent;
	public List<PlayerController> playerControllers = new List<PlayerController>();
	public PlayerLifeSystem pls;

	private void Awake()
	{
		CreateSingleton();
		pls = GetComponent<PlayerLifeSystem>();
	}

	public void OnPlayerJoined()
	{
		playersJoinedCount++;

		if(playersJoinedCount >= 1)
		{
			playerControllers = new List<PlayerController>(FindObjectsOfType<PlayerController>());
			EnableFirstRoomEnemies();
		}

		pls.InitLifeSystem((uint)playersJoinedCount);
	}

	void EnableFirstRoomEnemies()
	{

	}
}