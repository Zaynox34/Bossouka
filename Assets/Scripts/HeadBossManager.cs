using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBossManager : MonoBehaviour
{
    [SerializeField] private GameObject projectileGroup;
    [SerializeField] private float cadance;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject prefabProjectile;
    public List<int> rythmContainer;
    public List<float> rythmTiming;
    [SerializeField] private int koeCounter;
    [SerializeField] private float koeTimer;
    [SerializeField] private float counterCadance;
    // Start is called before the first frame update
    void Start()
    {
        counterCadance = 0f;
        koeTimer = 0f;
        koeCounter = -1;
        float tmp = 0;
        foreach(float elem in rythmTiming)
        {
            tmp += elem;
        }
        if(cadance<tmp)
        {
            cadance = tmp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Hibike();
    }
    public void Fire()
    {
        GameObject tmp = Instantiate(prefabProjectile, transform.position, Quaternion.identity);
        tmp.transform.parent = projectileGroup.transform;
        tmp.GetComponent<ProjectileManager>().target = player.transform;
        tmp.GetComponent<ProjectileManager>().father = this.gameObject;

    }
    public void Hibike()
    {
        if (counterCadance >= cadance)
        {
            koeCounter = 0;
            counterCadance = 0;
            
        }
        if (koeCounter>=0 && koeCounter<rythmContainer.Count)
        {
            if(koeTimer>=rythmTiming[koeCounter])
            {
                player.GetComponent<PlayerController>().PlaySound(rythmContainer[koeCounter]);
                koeCounter++;
            }
            koeTimer += Time.deltaTime;
        }
        if(koeCounter >= rythmContainer.Count)
        {
            
            Fire();
            koeCounter = -1;
            koeTimer = 0;
        }
        counterCadance += Time.deltaTime;

    }

}
