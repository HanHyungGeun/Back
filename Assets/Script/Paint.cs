using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Paint : MonoBehaviour
{
    bool isDark = false;
    private GameObject Target;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isDark) return;
        isDark = true;
        StageManager.Instance.PlayerHit(20);
        Target = col.gameObject;
        StartCoroutine(co_OffDarkSight(col.gameObject));
    }
    IEnumerator co_OffDarkSight(GameObject go)
    {
        go.transform.GetChild(0).gameObject.SetActive(true);
        SoundManager.Instance.Play_Gun(Constant.GunSoundType.PaintBallHit);
        yield return new WaitForSeconds(3);
        Off(go);
    }
    void Off(GameObject go)
    {
        SpriteRenderer spr = Target.transform.GetChild(0).GetComponent<SpriteRenderer>();
        spr.color = new Color(0, 0, 0, 1);
        spr.DOFade(0, 1);
        if(spr.color.a == 0)
        {
            go.transform.GetChild(0).gameObject.SetActive(false);
            isDark = false;
        }
    }
}
