using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversManager : MonoBehaviour
{
    [SerializeField] private Transform CameraTransform;
    [SerializeField] private GameObject prefabTarget;
    [SerializeField] private GameObject projectileGroup;
    [SerializeField] private GameObject projectileTargetGroup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
            //other.GetComponent<ProjectileManager>().isTargetByPlayer = true;
            other.transform.parent = projectileTargetGroup.transform;
            GameObject tmp = Instantiate(prefabTarget, other.transform);
            tmp.GetComponent<TargetManager>().CameraTransform = this.CameraTransform;
    }
    private void OnTriggerExit(Collider other)
    {
            //other.GetComponent<ProjectileManager>().isTargetByPlayer = false;
            other.transform.parent = projectileGroup.transform;
            other.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;      
    }

}
