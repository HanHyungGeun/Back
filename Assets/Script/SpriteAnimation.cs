using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{

    public Sprite[] Sprites;
    private SpriteRenderer spr;
    private int SpriteCount = 0;
    public float Speed;

    public enum PlayType { Loop , Once};
    public PlayType playType;
    void Awake()
    {
        spr = GetComponent<SpriteRenderer>();   
    }
    private void OnEnable()
    {
        switch (playType)
        {
            case PlayType.Loop:
                StartCoroutine(SpriteAnimationPlayLoop());
                break;
            case PlayType.Once:
                StartCoroutine(SpriteAnimationPlayOnce());
                break;
        }
    }
    IEnumerator SpriteAnimationPlayLoop()
    {
        while(true)
        {
            spr.sprite = Sprites[SpriteCount++];
            yield return new WaitForSeconds(Speed);
            SpriteCount = SpriteCount >= Sprites.Length ? 0 : SpriteCount;
        }
    }
    IEnumerator SpriteAnimationPlayOnce()
    {
        while (true)
        {
            spr.sprite = Sprites[SpriteCount++];
            yield return new WaitForSeconds(Speed);
            if (SpriteCount >= Sprites.Length - 1) break;
        }
        Destroy(gameObject);
    }
}
