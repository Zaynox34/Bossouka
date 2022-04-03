using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouManger : MonoBehaviour
{
    public float size;
    public float thickness;
    public int subdivision;
    public float numberOscilation;
    public float speedOscilationX;
    public float speedOscilationY;
    public float amplitudeOscilation;
    public float offsetX;
    public float offsetY;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<LineRenderer>().startWidth = thickness;
        GetComponent<LineRenderer>().endWidth = thickness;
        GetComponent<LineRenderer>().positionCount=subdivision;
        offsetX = 0;
        offsetY = 0;
        /*for (int i=0; i<subdivision;i++)
        {
            GetComponent<LineRenderer>().SetPosition(i, new Vector3(0,Mathf.Sin((i%(subdivision/numberOscilation))*(Mathf.PI*2)/ (subdivision / numberOscilation))*amplitudeOscilation, size / subdivision * i));
        }
        */
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < subdivision; i++)
        {
            GetComponent<LineRenderer>().SetPosition(i, new Vector3(
                Mathf.Cos(((i + offsetX) % (subdivision / numberOscilation)) * (Mathf.PI * 2) / (subdivision / numberOscilation)) * amplitudeOscilation- Mathf.Cos(((0 + offsetX) % (subdivision / numberOscilation)) * (Mathf.PI * 2) / (subdivision / numberOscilation)) * amplitudeOscilation,
                Mathf.Sin(((i + offsetY) % (subdivision / numberOscilation)) * (Mathf.PI * 2) / (subdivision / numberOscilation)) * amplitudeOscilation- Mathf.Sin(((0 + offsetY) % (subdivision / numberOscilation)) * (Mathf.PI * 2) / (subdivision / numberOscilation)) * amplitudeOscilation,
                size / subdivision * i)
                );
        }
        offsetX+=speedOscilationX;
        offsetY += speedOscilationY;
    }
}

