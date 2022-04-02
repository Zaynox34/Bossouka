using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBossManager : MonoBehaviour
{
    [SerializeField] private GameObject projectileGroup;
    [SerializeField] private float cadance;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject prefabProjectile;
    private float counterCadance;
    // Start is called before the first frame update
    void Start()
    {
        counterCadance = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }
    public void Fire()
    {
        if (counterCadance >= cadance)
        {
            counterCadance = 0;
            GameObject tmp = Instantiate(prefabProjectile, transform.position, Quaternion.identity);
            tmp.transform.parent = projectileGroup.transform;
            tmp.GetComponent<ProjectileManager>().target = player.transform;
        }
        counterCadance += Time.deltaTime;
    }
}
