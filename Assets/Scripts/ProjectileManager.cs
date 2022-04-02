using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public Transform target;
    [SerializeField] private bool isTargetByPlayer;
    [SerializeField] private float speed;
    [SerializeField] private float flair;
    [SerializeField] private Vector3 Direction;
    [SerializeField]  private float flairCadance;
    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        flairCadance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Pif();
    }
    public void Pif()
    {
        if (flairCadance >= flair)
        {
            flairCadance = -1;
            Vector3 Direction = target.position;

        }
        else
        {
            if (flairCadance >= 0)
            {
                Direction = target.position - transform.position;
                flairCadance += Time.deltaTime;
            }
        }
        Direction.Normalize();
        transform.position += Direction * Time.deltaTime * speed;
    }
}
