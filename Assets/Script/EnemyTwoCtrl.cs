using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwoCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject CrossHair;
    void Start()
    {
      
    }
    private void OnEnable()
    {
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
}
