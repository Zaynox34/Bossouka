using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBossManager : MonoBehaviour
{
    [SerializeField] private GameObject projectileGroup;
    public float cadance;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject prefabProjectile;
    public List<int> rythmContainer;
    public List<float> rythmTiming;
    [SerializeField] private int koeCounter;
    [SerializeField] private float koeTimer;
    public float counterCadance;
    [SerializeField] GameObject neck;
    [SerializeField] GameObject headController;
    [SerializeField] GameObject boss;
    public bool hatsudou;
    // Start is called before the first frame update
    void Start()
    {
        cadance = 0;
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
        hatsudou = false;
    }

    // Update is called once per frame
    void Update()
    {
        cadance = boss.GetComponent<BossManger>().bpm;
        if (hatsudou)
        {
            Hibike();
        }
        transform.position=neck.GetComponent<LineRenderer>().GetPosition(neck.GetComponent<LineRenderer>().positionCount-1);
        transform.rotation = headController.transform.rotation;
    }
    public void Fire()
    {
        Debug.Log("aa");
        neck.GetComponent<NeckManager>().freeze = false;
        GameObject tmp = Instantiate(prefabProjectile, transform.position, Quaternion.identity);
        tmp.transform.parent = projectileGroup.transform;
        tmp.GetComponent<ProjectileManager>().target = player.transform;
        tmp.GetComponent<ProjectileManager>().father = this.gameObject;
        hatsudou = false;

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
            transform.localScale += new Vector3(2, 2, 2) * Time.deltaTime;
            neck.GetComponent<NeckManager>().freeze = true;
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
