using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderScript : MonoBehaviour {

    public BoxCollider2D boxCollider2D;
    SpriteRenderer spriteRenderer;


	// Use this for initialization
	public void CreateBoxCollider () {
        if (boxCollider2D == null) {
            boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
        }
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        boxCollider2D.size = new Vector2 (spriteRenderer.size.x, spriteRenderer.size.y);

	}

}
