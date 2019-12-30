using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyThreeCtrl : MonoBehaviour
{

    public GameObject XMark;

    public GameObject[] Paint;

    public GameObject Ball;

    public GameObject Effect;
    public GameObject Remnants;
    public Vector3[] XPosition;
    private int count = 0;
    public Transform FIrePos;

    private GameObject TargetX;
    public bool isLock;
    private void Start()
    {
        StageManager.Instance.SyncEnemy();
    }
    private void OnEnable()
    {
        isLock = true;
        StageManager.Instance.GameStartEvent += (() =>
        {
            StartCoroutine(co_Attack());
        });
    }
    private void OnDisable()
    {
        StageManager.Instance.GameStartEvent -= (() => { StartCoroutine(co_Attack()); });
    }

    IEnumerator co_Attack()
    {
        TargetX = Instantiate(XMark, XPosition[count], transform.rotation);
        TargetX.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        TargetX.GetComponent<SpriteRenderer>().DOFade(1, 1);
        yield return new WaitForSeconds(2f);
        StartCoroutine(co_ShootBall());
        yield return new WaitForSeconds(6f);
        count = count >= XPosition.Length - 1 ? 0 : count + 1;
        StartCoroutine(co_Attack());        
    }
    IEnumerator co_ShootBall()
    {
        var go = Instantiate(Ball, FIrePos.position, transform.rotation);
        int rnd = Random.Range(0, 3);
        Vector3 dir = XPosition[count] - FIrePos.position;
        while(true)
        {
            yield return null;
            //Debug.Log(Vector3.Distance(go.transform.position, XPosition[count]));
            go.transform.Translate(dir * 2 * Time.deltaTime);
            if (Vector3.Distance(go.transform.position,XPosition[count]) <= .5f)
            {
                Instantiate(Paint[rnd], XPosition[count], transform.rotation);
                Instantiate(Effect, XPosition[count], transform.rotation);
                Destroy(go.gameObject);
                Destroy(TargetX);
                SoundManager.Instance.Play_Gun(Constant.GunSoundType.PaintBallShoot);
                break;
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0;i<XPosition.Length;i++)
        {
            Gizmos.DrawSphere(XPosition[i], 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        gameObject.SetActive(false);
        for (int i = 0; i < 6; i++)
        {
            var go = Instantiate(Remnants, transform.position, transform.rotation);
            Vector2 randomVector = new Vector2((float)Random.Range(-3, 3), (float)Random.Range(-3, 3));
            go.GetComponent<Rigidbody2D>().AddForce(randomVector * 90);
        }
        StageManager.Instance.EnemyDie();
        Camera.main.DOShakePosition(0.25f, 3, 30);
    }

}
