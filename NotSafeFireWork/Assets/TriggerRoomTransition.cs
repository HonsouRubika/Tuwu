using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRoomTransition : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag =="Player")
        {
            Debug.Log("Playercollision");
            LevelHandler.Instance.ChangeRoomTrigger();

            if (LevelHandler.currentState == 5)
            {
                UIManager.Instance.EnableEndScreen(true);
                StartCoroutine(QuitGame());
            }
        }
    }

    IEnumerator QuitGame()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("byebye");
        Application.Quit();
    }
}
