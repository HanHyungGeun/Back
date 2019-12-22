using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{

    private GameObject Player;

    private bool isFire;
    private bool isFollow;
    private float DetectRange = 2.5f;
    private int MovePoint = 0;
    private float FireDelay = .3f;


    [Header("Cross Hair Parts")]
    [SerializeField] private GameObject Aim;
    [SerializeField] private GameObject Cicle;
    [SerializeField] private GameObject Shadow;
    public GameObject Lock;
    public GameObject UnLock;


    public GameObject BloodEffect;
    public bool isLock;
    public Vector3[] WayPoint;


    void Start()
    {
        Player = GameObject.Find("Player");
        InvokeRepeating("AttackPlayer", FireDelay, FireDelay);///kjkjkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkjjjj
    }

    
    public void FollowCtrl()
    {
        isFollow = !isFollow;
    }
    void OnEnable()
    {
        UnLock.SetActive(false);
        Lock.SetActive(true);
        isFire = false;
        isFollow = false;
        isLock = true;
    }
    //화면에 금가는거
    //노랑배경
    //사운드
    //안쫓아올때 isFollow Flase일때 효과
    //쫓아올때(공격) isFollow true 일때효과 
    void Update()
    {
        if (isLock) return;
        FindNextPoint();
        DetectPlayer();
        transform.Translate((WayPoint[MovePoint] - transform.position).normalized * 5 * Time.deltaTime);
    }
    void FindNextPoint()
    {
        if (Vector3.Distance(transform.position, WayPoint[MovePoint]) <= .5f)
        {
            MovePoint = MovePoint >= WayPoint.Length - 1 ? 0 : MovePoint + 1;
        }
    }
    void DetectPlayer()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) <= 4.5f)
        {
            if(isFollow)
            {
                transform.position = Vector3.Lerp(transform.position, Player.transform.position, Time.deltaTime * 20f);
                isFire = true;
            }
            StartCoroutine(co_FollowControl());
        }
        else
            isFire = false;
    }
    IEnumerator co_FollowControl()
    {
        isFollow = true;
        Lock.SetActive(false);
        UnLock.SetActive(true);
        yield return new WaitForSeconds(2f);
        Lock.SetActive(true);
        UnLock.SetActive(false);
        isFollow = false;
    }

    void AttackPlayer()
    {
        if (!isFire) return;
        if (Vector3.Distance(transform.position, Player.transform.position) <= 0.5f)
        {
            if (!DataManager.Instance.isBlood)
            {
                GameObject Blood = Instantiate(BloodEffect) as GameObject;
                Blood.transform.position = StageManager.Instance.GetPlayer().transform.position;
                Blood.transform.eulerAngles = new Vector3(0, 0, 0);
                Blood.transform.localScale = new Vector3(8f, 8f, 1);
            }
            StageManager.Instance.PlayerHit(1, 0.025f, 10, 50);
            //사운드
        }
    }

    void OnDrawGizmosSelected()
    {
        for (int i = 0; i < WayPoint.Length; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(WayPoint[i], 1);
        }
    }
}