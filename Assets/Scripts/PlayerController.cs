using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerControl playerControl;
    [SerializeField] private Transform centerPivotTransform;
    [SerializeField] private GameObject projectileTargetGroup;
    [SerializeField] private float speed;  
    [Header("Rythm")]
    [SerializeField] GameObject target;
    [SerializeField] private float rythmOffsetError;
    [SerializeField] private List<int> rythmContainer;
    [SerializeField] private int maxrythm;
    [SerializeField] private int numberofrythm;
    [SerializeField] private List<float> rythmTimming;
    [SerializeField] private float timeInput;
    [SerializeField] private bool startTimeInput;
    [SerializeField] private List<float> rythmTimmingAverage;   
    
    private void Awake()
    {
        playerControl = new PlayerControl();
        Application.targetFrameRate = 60;
    }

    private void OnEnable()
    {
        playerControl.Enable();
    }
    private void OnDisable()
    {
        playerControl.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        numberofrythm = 0;
        timeInput = 0;
        speed = 40;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Patapon();
        //ChronoMetre(maxrythm);
        Targeter();
    }
    
    public void Move()
    {
        transform.forward = new Vector3(transform.position.x, 0, transform.position.z).normalized; 
        Vector2 move = playerControl.BaseControl.Move.ReadValue<Vector2>();
        if (move != Vector2.zero)
        {
            centerPivotTransform.Rotate(new Vector3(0, -move.x, 0) * Time.deltaTime * speed);
            transform.position += transform.forward*-move.y * Time.deltaTime * speed / Mathf.PI;
        }
    }
    public void Patapon()
    {
        if (playerControl.BaseControl.North.triggered)
        {
            GetComponents<AudioSource>()[0].Play();
        }
        else
        {
            if (playerControl.BaseControl.East.triggered)
            {
                GetComponents<AudioSource>()[1].Play();
            }
            else
            {
                if (playerControl.BaseControl.West.triggered)
                {
                    GetComponents<AudioSource>()[2].Play();
                }
                else
                {
                    if (playerControl.BaseControl.South.triggered)
                    {
                        GetComponents<AudioSource>()[3].Play();
                    }
                    else
                    {
                        if (playerControl.BaseControl.Gatotsu.triggered)
                        {
                            GetComponents<AudioSource>()[4].Play();
                        }
                    }
                }
            }
        }
    }
    public void PlaySound(int n)
    {
        if (n==8)
        {
            GetComponents<AudioSource>()[0].Play();
        }
        if (n == 6)
        {
            GetComponents<AudioSource>()[1].Play();
        }
        if (n == 4)
        {
            GetComponents<AudioSource>()[2].Play();
        }
        if (n == 2)
        {
            GetComponents<AudioSource>()[3].Play();
        }
        if (n == 5)
        {
            GetComponents<AudioSource>()[4].Play();
        }
    }
    
    public void Targeter()
    {
        if (projectileTargetGroup.transform.childCount == 0)
        {
            target = null;
        }
        else
        {
            int targeterIndex = 0;
            for (int i = 0; i < projectileTargetGroup.transform.childCount; i++)
            {
                if ((projectileTargetGroup.transform.GetChild(i).position - transform.position).magnitude > (projectileTargetGroup.transform.GetChild(targeterIndex).position - transform.position).magnitude)
                {
                    targeterIndex = i;
                }
            }
            target = projectileTargetGroup.transform.GetChild(targeterIndex).GetComponent<ProjectileManager>().father;
            Checker();
        }
    }
    public void Checker()
    {
       
        if (numberofrythm < target.GetComponent<HeadBossManager>().rythmContainer.Count)
        {
            if (timeInput > target.GetComponent<HeadBossManager>().rythmTiming[numberofrythm] + rythmOffsetError)
            {
                Debug.Log("trop lent");
                timeInput = 0;
                startTimeInput = false;
                rythmContainer = new List<int>() { };
                rythmTimming = new List<float>() { };
                numberofrythm = 0;
            }
            else
            {
                if (
                    (playerControl.BaseControl.North.triggered) ||
                    (playerControl.BaseControl.East.triggered) ||
                    (playerControl.BaseControl.West.triggered) ||
                    (playerControl.BaseControl.South.triggered) ||
                    (playerControl.BaseControl.Gatotsu.triggered)
                    )
                {
                    if (numberofrythm == 0)
                    {
                        startTimeInput = true;
                    }
                    if (
                        (
                         (playerControl.BaseControl.North.triggered && target.GetComponent<HeadBossManager>().rythmContainer[numberofrythm] == 8) ||
                         (playerControl.BaseControl.East.triggered && target.GetComponent<HeadBossManager>().rythmContainer[numberofrythm] == 6) ||
                         (playerControl.BaseControl.West.triggered && target.GetComponent<HeadBossManager>().rythmContainer[numberofrythm] == 4) ||
                         (playerControl.BaseControl.South.triggered && target.GetComponent<HeadBossManager>().rythmContainer[numberofrythm] == 2) ||
                         (playerControl.BaseControl.Gatotsu.triggered && target.GetComponent<HeadBossManager>().rythmContainer[numberofrythm] == 5)
                        )
                        &&
                        (
                         (timeInput >= target.GetComponent<HeadBossManager>().rythmTiming[numberofrythm] - rythmOffsetError) &&
                         (timeInput <= target.GetComponent<HeadBossManager>().rythmTiming[numberofrythm] + rythmOffsetError)
                        )
                       )
                    {
                        rythmTimming.Add(timeInput);
                        rythmContainer.Add(target.GetComponent<HeadBossManager>().rythmContainer[numberofrythm]);
                        if (numberofrythm == maxrythm - 1)
                        {
                            numberofrythm = 0;
                            startTimeInput = false;
                            timeInput = 0;
                            rythmContainer = new List<int>() { };
                            rythmTimming = new List<float>() { };
                            Repel();
                            Debug.Log("yes");

                        }
                        else
                        {
                            numberofrythm++;
                        }
                    }
                    else
                    {
                        timeInput = 0;
                        startTimeInput = false;
                        rythmContainer = new List<int>() { };
                        rythmTimming = new List<float>() { };
                        numberofrythm = 0;
                    }
                }
                if (startTimeInput)
                {
                    timeInput += Time.deltaTime;
                }
            }
        }
    }
    public void Repel()
    {
        for (int i = 0; i < projectileTargetGroup.transform.childCount; i++)
        {
            if (projectileTargetGroup.transform.GetChild(i).GetComponent<ProjectileManager>().father=target)
            {
                projectileTargetGroup.transform.GetChild(i).GetComponent<ProjectileManager>().revers=true;
                projectileTargetGroup.transform.GetChild(i).GetComponent<ProjectileManager>().speed *= 2;
            }
        }
    }
    public void ChronoMetre()
    {
        if (numberofrythm < maxrythm)
        {
            if ((playerControl.BaseControl.North.triggered) ||
                (playerControl.BaseControl.East.triggered) ||
                (playerControl.BaseControl.West.triggered) ||
                (playerControl.BaseControl.South.triggered) ||
                (playerControl.BaseControl.Gatotsu.triggered))
            {
                if (numberofrythm == 0)
                {
                    startTimeInput = true;
                }
                if (playerControl.BaseControl.North.triggered)
                {
                    rythmContainer.Add(8);
                }
                if (playerControl.BaseControl.East.triggered)
                {
                    rythmContainer.Add(6);
                }
                if (playerControl.BaseControl.West.triggered)
                {
                    rythmContainer.Add(4);
                }
                if (playerControl.BaseControl.South.triggered)
                {
                    rythmContainer.Add(2);
                }
                if (playerControl.BaseControl.Gatotsu.triggered)
                {
                    rythmContainer.Add(5);

                }

                rythmTimming.Add(timeInput);
                if (numberofrythm == maxrythm - 1)
                {
                    numberofrythm = 0;
                    startTimeInput = false;
                    timeInput = 0;
                    rythmTimmingAverage = new List<float>() { 0, 0, 0 };
                    for (int z = 0; z < rythmTimming.Count; z++)
                    {
                        rythmTimmingAverage[z % maxrythm] += rythmTimming[z];
                    }
                    for (int z = 0; z < maxrythm; z++)
                    {
                        rythmTimmingAverage[z] = rythmTimmingAverage[z] / (rythmTimming.Count / maxrythm);
                    }

                }
                else
                {
                    numberofrythm++;
                }
            }
            if (startTimeInput)
            {
                timeInput += Time.deltaTime;
            }
        }
    }
}
