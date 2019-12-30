using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DarkSight : MonoBehaviour
{
    public GameObject Player;
    private SpriteRenderer spr;

    void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {      
        transform.position = Player.transform.position;
        spr.color = new Color(0, 0, 0, 0);
        spr.DOFade(1, 1.5f);
    }

    void Update()
    {
        transform.position = Vector3.Lerp(Player.transform.position, transform.position, Time.deltaTime * 50f);
    }
}
