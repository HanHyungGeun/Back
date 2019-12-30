using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float time;

    private void OnEnable()
    {
        StartCoroutine(co_Destroy());
    }
    IEnumerator co_Destroy()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
