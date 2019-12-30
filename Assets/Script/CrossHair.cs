using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    private enum State {Follow,NoFollow,Wait };
    private State state;
    private GameObject Player;

    private bool isFire;
    private float DetectRange = 2.5f;
    private int MovePoint = 0;
    private float FireDelay = .3f;


    [SerializeField] private SpriteRenderer Aim;

    public GameObject Lock;
    public GameObject UnLock;


    public GameObject BloodEffect;
    public bool isLock;
    private bool isControl = false;
    public Vector3[] WayPoint;



    void Start()
    {
        Player = GameObject.Find("Player");
        InvokeRepeating("AttackPlayer", FireDelay, FireDelay);
    }

    

    void OnEnable()
    {
        UnLock.SetActive(false);
        Lock.SetActive(true);
        isFire = false;
        state = State.Wait;
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
            if (state != State.Wait || state == State.NoFollow) return;

            StartCoroutine(co_Follow());
            transform.position = Vector3.Lerp(transform.position, Player.transform.position, Time.deltaTime * 20f);
            isFire = true;
        }
        else
            isFire = false;
    }
    IEnumerator co_Follow()
    {
        Lock.SetActive(false);
        UnLock.SetActive(true);
        Aim.color = Color.red;
        yield return new WaitForSeconds(2);
        state = State.NoFollow;
        Lock.SetActive(true);
        UnLock.SetActive(false);
        Aim.color = Color.black;
        yield return new WaitForSeconds(1.5f);
        state = State.Wait;

    }
    void AttackPlayer()
    {
        if (!isFire) return;
        if (Vector3.Distance(transform.position, Player.transform.position) <= 1f)
        {
            if (!DataManager.Instance.isBlood)
            {
                GameObject Blood = Instantiate(BloodEffect) as GameObject;
                Blood.transform.position = StageManager.Instance.GetPlayer().transform.position;
                Blood.transform.eulerAngles = new Vector3(0, 0, 0);
                Blood.transform.localScale = new Vector3(8f, 8f, 1);
            }
            StageManager.Instance.PlayerHit(1, 0.025f, 10, 50);
            SoundManager.Instance.Play_Gun(Constant.GunSoundType.EnemyRifle);
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