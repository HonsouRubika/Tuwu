using System.Collections;
using UnityEngine;

public class EnemyDeathFX : MonoBehaviour
{
	private void Start()
	{
		StartCoroutine(Death());
	}

	IEnumerator Death()
	{
		yield return new WaitForSeconds(1.5f);
		Destroy(gameObject);
	}
}
