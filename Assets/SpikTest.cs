using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class SpikTest : MonoBehaviour
{
    public Sprite WallSprite;
    public SpriteRenderer WallRender;

    public Sprite SpikSprite;
    public SpriteRenderer SpikRender;

    public float SpikTime = 0.3f;
    public float SpikPower = 0.3f;


    private float WallWidth;
    private float WallHeight;

    private float WallWidthRadius;
    private float WallHeightRadius;

    private float SpikWidth;
    private float SpikHeight;

    private float SpikWidthRadius;
    private float SpikHeightRadius;

    private float SpikSliceOffset;

    private float SpikOriginalPosY;
    private float SpikOriginalSizeY;

    private BoxCollider2D SpikCollider;

    // Start is called before the first frame update
    void Start()
    {
        WallWidth = (float)WallSprite.texture.width / 100;
        WallHeight = (float)WallSprite.texture.height / 100;

        WallWidthRadius = WallWidth / 2;
        WallHeightRadius = WallHeight / 2;

        SpikWidth = (float)SpikSprite.texture.width / 100;
        SpikHeight = (float)SpikSprite.texture.height / 100;

        SpikWidthRadius = SpikWidth / 2;
        SpikHeightRadius = SpikHeight / 2;

        SpikSliceOffset = SpikRender.size.y / SpikHeight;

        SpikOriginalPosY = SpikRender.transform.localPosition.y;
        SpikOriginalSizeY = SpikRender.size.y;

        SpikCollider = SpikRender.GetComponent<BoxCollider2D>();
        SpikCollider.size = new Vector2(SpikRender.size.x, SpikRender.size.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            StartCoroutine(Spik());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Spik());
        }
    }
    private IEnumerator Spik()
    {
        float CurrentTime = 0.0f;
        float EndTime = SpikTime * 0.5f;

        while (true)
        {
            CurrentTime += Time.deltaTime;
            if (CurrentTime >= EndTime)
            {
                CurrentTime = 0.0f;
                break;
            }
            SpikRender.size = new Vector2(SpikRender.size.x, SpikRender.size.y + SpikPower);
            SpikSliceOffset = SpikRender.size.y / SpikHeight;
            SpikRender.transform.localPosition =
                new Vector3(SpikRender.transform.localPosition.x, -(WallHeightRadius + (SpikHeightRadius * SpikSliceOffset)) - SpikOriginalPosY, 0);
            SpikCollider.size = new Vector2(SpikRender.size.x, SpikRender.size.y);
            yield return null;
        }

        while (true)
        {
            CurrentTime += Time.deltaTime;
            if (CurrentTime >= EndTime)
            {
                CurrentTime = 0.0f;
                break;
            }
            SpikRender.size = new Vector2(SpikRender.size.x, SpikRender.size.y - SpikPower);
            SpikSliceOffset = SpikRender.size.y / SpikHeight;
            SpikRender.transform.localPosition =
                new Vector3(SpikRender.transform.localPosition.x, -(WallHeightRadius + (SpikHeightRadius * SpikSliceOffset)) - SpikOriginalPosY, 0);
            SpikCollider.size = new Vector2(SpikRender.size.x, SpikRender.size.y);
            yield return null;
        }


        SpikRender.size = new Vector2(SpikRender.size.x, SpikOriginalSizeY);
        SpikSliceOffset = SpikRender.size.y / SpikHeight;
        SpikRender.transform.localPosition =
            new Vector3(SpikRender.transform.localPosition.x, SpikOriginalPosY, 0);
        SpikCollider.size = new Vector2(SpikRender.size.x, SpikRender.size.y);
    }

}
