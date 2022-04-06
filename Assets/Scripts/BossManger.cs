using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManger : MonoBehaviour
{
    [SerializeField] List<GameObject> headsControllers;
    [SerializeField] List<GameObject> heads;
    [SerializeField] List<GameObject> necks;
    public int whichtHead;
    [Header("Variable")]
    public float bpm;
    public float bpmCounter;
    // Start is called before the first frame update
    void Start()
    {
        bpm = bpm/60;
        bpm =1/bpm;
        bpm *= 2;
        bpmCounter = 0f;
        whichtHead = Random.Range(0, 8);
        heads[whichtHead].GetComponent<HeadBossManager>().hatsudou = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (bpm >= bpmCounter)
        { 
            bpmCounter += Time.deltaTime;
        }
        else
        {
            whichtHead = Random.Range(0, 8);
            heads[whichtHead].GetComponent<HeadBossManager>().hatsudou = true;
            bpmCounter = 0;
        }
    }
}
