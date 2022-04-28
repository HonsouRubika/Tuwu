using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XTrap : MonoBehaviour
{

    private CircleCollider2D cc;
    private BulletPro.BulletEmitter be;

    private void Start()
    {
        cc = GetComponent<CircleCollider2D>();
        be = GetComponent<BulletPro.BulletEmitter>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("oui");
        if (collision.gameObject.tag == "Player")
        {
            be.Play();
        }
    }


}
