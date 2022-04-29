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
        }
    }
}
