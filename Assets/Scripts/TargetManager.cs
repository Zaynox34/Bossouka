using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public Transform CameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = (CameraTransform.position - transform.position).normalized;
        transform.localScale = Vector3.one *(Mathf.Pow((CameraTransform.position - transform.position).magnitude,4))/500000;
    }
}
