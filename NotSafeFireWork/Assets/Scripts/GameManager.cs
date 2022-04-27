using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	[HideInInspector] public List<PlayerController> playerControllers = new List<PlayerController>();

	private void Awake()
	{
		CreateSingleton();
	}
}