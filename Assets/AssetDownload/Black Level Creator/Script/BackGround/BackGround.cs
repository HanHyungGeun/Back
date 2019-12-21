using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackGround : MonoBehaviour {

    public GameObject player; // GameObjact player from which will move backgrounds
    public List<BackGroundLayer> allBackGoundLayer; // All background layers

    [HideInInspector]
    public List<GameObject> myBackGroundLayer;
    [HideInInspector]
    public List<GameObject> myBackGroundCount;
    [HideInInspector]
    public List<float> boundsX;
    [HideInInspector]
    public List<Vector2> starPositionBackGroundLayer;
    [HideInInspector]
    public bool haveBackGround;

    private void Start()
    {
        if (haveBackGround) {
            for (int i = 0; i < myBackGroundLayer.Count; i ++) {
                starPositionBackGroundLayer.Add(myBackGroundLayer[i].transform.position);
            }
        }
    }

    // Use this for initialization. Create and fill all layers of backgrounds
    public void CreateBackGround() {
        
        if (!haveBackGround) {
            for (int i = 0; i < allBackGoundLayer.Count; i++)
            {
                GameObject newObject = new GameObject();
                myBackGroundLayer.Add(newObject);
                newObject.transform.parent = gameObject.transform;
                newObject.name = "BackGroundLayer_" + i;
                for (int j = 0; j < 3; j++) {
                    GameObject newObjectLayer = new GameObject();
                    myBackGroundCount.Add(newObjectLayer);
                    newObjectLayer.transform.parent = newObject.transform;
                    newObjectLayer.AddComponent<SpriteRenderer>().sprite = allBackGoundLayer[i].mySprite;
                    newObjectLayer.GetComponent<SpriteRenderer>().sortingLayerName = allBackGoundLayer[i].layerName;
                    newObjectLayer.GetComponent<SpriteRenderer>().sortingOrder = allBackGoundLayer[i].orderInLayer;
                    boundsX.Add(newObjectLayer.GetComponent<SpriteRenderer>().sprite.bounds.size.x);
                    newObjectLayer.transform.position = new Vector2(-boundsX[j] + boundsX[j] * j, 0);
                    newObjectLayer.name = "BackGroundCount_" + j;
                    }
                }
            haveBackGround = true;
            }
        }

    // Update is called once per frame. We move the background depending on the player's position
    void Update() {
        if (haveBackGround) {
            for (int i = 0; i < allBackGoundLayer.Count; i++) {
                myBackGroundLayer[i].transform.position = new Vector2(starPositionBackGroundLayer[i].x + player.transform.position.x * allBackGoundLayer[i].moveSpeedX,
                                                                      starPositionBackGroundLayer[i].y + player.transform.position.y * allBackGoundLayer[i].moveSpeedY);
            }
            for (int j = 0; j < myBackGroundCount.Count; j++) {
                CheckPosition(myBackGroundCount[j], j);
            }
        }
    }

    //We check the background position if it is too far from the player, we move it to the next position
    void CheckPosition(GameObject myObject, int j) {
        if (myObject.transform.position.x < player.transform.position.x - 1.5f * boundsX[j])
        {
            myObject.transform.position = new Vector2(myObject.transform.position.x + boundsX[j] * 3, myObject.transform.position.y);
        } else if (myObject.transform.position.x > player.transform.position.x + boundsX[j] * 1.5f)
        {
            myObject.transform.position = new Vector2(myObject.transform.position.x - boundsX[j] * 3, myObject.transform.position.y);
        }
    }
    public void DeleteBackGround() {
        for (int i = 0; i < myBackGroundLayer.Count; i ++) {
            DestroyImmediate(myBackGroundLayer[i]);
        }
        myBackGroundLayer.Clear();
        myBackGroundCount.Clear();
        haveBackGround = false;
    }
}
