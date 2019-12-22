using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyTwoCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject CrossHair;
    public GameObject Remnants;

    private LineRenderer lr;
    private Vector3 firePos;
    private void Start()
    {
        StageManager.Instance.SyncEnemy();
    }
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.startWidth = .19f;
        lr.endWidth = .19f;
        lr.startColor = Color.red;
        lr.endColor = Color.black;
    }
    private void OnEnable()
    {
        firePos = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        CrossHair.transform.position = transform.position;
        StageManager.Instance.GameStartEvent += (() => 
        {
            CrossHair.SetActive(true);
            CrossHair.GetComponent<CrossHair>().isLock = false;
        });
    }

    private void OnDisable()
    {
        StageManager.Instance.GameStartEvent -= (() => { CrossHair.SetActive(true); });
    }
    private void Update()
    {
        lr.SetPosition(0, firePos);
        lr.SetPosition(1,CrossHair.transform.position);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        gameObject.SetActive(false);
        for(int i=0;i<6;i++)
        {
            var go = Instantiate(Remnants, transform.position, transform.rotation);
            Vector2 randomVector = new Vector2((float)Random.Range(-3, 3), (float)Random.Range(-3, 3));
            go.GetComponent<Rigidbody2D>().AddForce(randomVector * 90);
        }
        StageManager.Instance.EnemyDie();
        Camera.main.DOShakePosition(0.25f, 3, 30);
    }
}
