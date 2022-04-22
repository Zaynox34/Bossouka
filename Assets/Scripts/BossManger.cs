using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManger : MonoBehaviour
{
    [SerializeField] List<GameObject> headsControllers;
    [SerializeField] List<GameObject> monsters;
    [SerializeField] List<GameObject> heads;
    [SerializeField] List<GameObject> necks;
    [SerializeField] GameObject projectileGroup;
    [SerializeField] GameObject projectileTargetGroup;
    [SerializeField] GameObject player;

    public int whichtHead;
    [Header("Variable")]
    public float bpm;
    public float bpmCounter;
    [SerializeField] bool hadsudoChecker;
    // Start is called before the first frame update
    void Start()
    {
        bpm = bpm / 60;
        bpm = 1 / bpm;
        bpm *= 2;
        bpmCounter = 0f;
        //whichtHead = Random.Range(0, 8);
        //heads[whichtHead].GetComponent<HeadBossManager>().hatsudou = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (bpm >= bpmCounter)
        { 
            bpmCounter += Time.deltaTime;
        }
        else
        {
            whichtHead = Random.Range(0, 8);
            heads[whichtHead].GetComponent<HeadBossManager>().hatsudou = true;
            bpmCounter = 0;
        }
    }*/
        foreach (GameObject headController in headsControllers)
        {
            headController.transform.right = (player.transform.position - headController.transform.position).normalized;
        }
        //Vector3 angleDirection = (player.transform.parent.eulerAngles-monsters[whichtHead].transform.eulerAngles).normalized;       
        //monsters[whichtHead].transform.Rotate(angleDirection * Time.deltaTime * monsters[whichtHead].GetComponent<MonsterManager>().speed);
        hadsudoChecker = false;
        foreach(GameObject head in heads)
        {
            hadsudoChecker = hadsudoChecker || head.GetComponent<HeadBossManager>().hatsudou;
        }
        if (projectileGroup.transform.childCount == 0 && projectileTargetGroup.transform.childCount == 0 && !hadsudoChecker)
        {
            whichtHead = Random.Range(0, 8);
            heads[whichtHead].GetComponent<HeadBossManager>().hatsudou = true;
        }
    }
}
