using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkSight : MonoBehaviour
{
    public GameObject Player;

    void Start()
    {
        
    }
    private void OnEnable()
    {
        // x = 4 y = 15;
        transform.position = Player.transform.position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(Player.transform.position, transform.position, Time.deltaTime * 50f);
    }
}
