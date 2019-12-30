using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThreeCtrl : MonoBehaviour
{

    public GameObject XMark;
    public Vector3[] XPosition;
    private int CurrentPosition = 0;

    private void Awake()
    {
        
    }
    private void OnEnable()
    {
        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        Instantiate(XMark , XPosition[CurrentPosition] , transform.rotation);
        CurrentPosition = CurrentPosition >= XPosition.Length - 1 ? 0 : CurrentPosition + 1;
        yield return new WaitForSeconds(7f);
        StartCoroutine(Attack());
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0;i<XPosition.Length;i++)
        {
            Gizmos.DrawSphere(XPosition[i], 1);
        }

    }

}
