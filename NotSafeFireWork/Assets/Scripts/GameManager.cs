using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public int playersJoinedCount = 0;
	[SerializeField] GameObject enemiesGlobalParent;
	public List<PlayerController> playerControllers = new List<PlayerController>();

	private void Awake()
	{
		CreateSingleton();
	}

	public void OnPlayerJoined()
	{
		playersJoinedCount++;
		
		if(playersJoinedCount >= 2)
		{
			playerControllers = new List<PlayerController>(FindObjectsOfType<PlayerController>());
			EnableRoomEnemies();
		}
	}

	void EnableRoomEnemies()
	{

	}
}