using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public List<PlayerController> playerControllers = new List<PlayerController>();

	private void Awake()
	{
		CreateSingleton();
	}
}