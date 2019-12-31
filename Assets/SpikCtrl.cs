using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikCtrl : MonoBehaviour
{
    public float Damage;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            StageManager.Instance.PlayerHit(Damage);
        }
    }

}
