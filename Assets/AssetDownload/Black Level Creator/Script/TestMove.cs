using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour {

    public float moveSpeed;
    public float forceJump;
    public string collisionMask;

    public bool jumpFlag;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A)) {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x - Time.deltaTime * moveSpeed, gameObject.transform.position.y);
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x + Time.deltaTime * moveSpeed, gameObject.transform.position.y);
        }
        if (Input.GetKey(KeyCode.Space) && jumpFlag)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forceJump * 10), ForceMode2D.Force);
            jumpFlag = false;
        }
    }



}
