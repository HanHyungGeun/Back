using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{

    public Sprite[] Images;
    private SpriteRenderer spr;
    int Point = 0;
    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        StartCoroutine(ShowEffect());
    }

    IEnumerator ShowEffect()
    {
        while(true)
        {
            spr.sprite = Images[Point];
            yield return new WaitForSeconds(0.05f);
            Point++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
