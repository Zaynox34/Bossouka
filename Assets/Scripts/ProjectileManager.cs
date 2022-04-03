using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public Transform target;
    public GameObject father;
    public bool isTargetByPlayer;
    public bool revers;
    public float speed;
    [SerializeField] private float flair;
    [SerializeField] private Vector3 Direction;
    [SerializeField]  private float flairCadance;
    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
        flairCadance = 0;
        revers = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!revers)
        {
            Pif();
        }
        else
        {
            Revers();
        }
        
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
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
    public void Revers()
    {
        Vector3 Direction = father.transform.position - transform.position;
        flairCadance += Time.deltaTime;
        Direction.Normalize();
        this.gameObject.layer = 9;
        transform.parent = null;
        transform.position += Direction * Time.deltaTime * speed;
    }
}
