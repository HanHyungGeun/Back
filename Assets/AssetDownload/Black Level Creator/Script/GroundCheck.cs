using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    public TestMove player;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == player.collisionMask)
        {
            player.jumpFlag = true;
        }
    }
}
