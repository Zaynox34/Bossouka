using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeckManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject head;
    [SerializeField] Transform origin;
    [SerializeField] List<Vector3> bezierPoint;
    [SerializeField] GameObject point1;
    [SerializeField] GameObject point2;
    [SerializeField] GameObject point3;
    [SerializeField] float offset;
    [SerializeField] int subdivision;
    public float thickness;
    void Start()
    {
        GetComponent<LineRenderer>().startWidth = thickness;
        GetComponent<LineRenderer>().endWidth = thickness;
        GetComponent<LineRenderer>().positionCount = subdivision;
        bezierPoint.Add(origin.position);

        for (int i = 1; i < 5; i++)
        {
            bezierPoint.Add(new Vector3(0, 0, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlacerLesPoint();
        TracerBezierCourbe();
    }
    public void PlacerLesPoint()
    {   
        bezierPoint[1]=origin.position + new Vector3(0, offset,0);
        bezierPoint[4] = head.transform.position;
        bezierPoint[2] =new Vector3(origin.position.x,head.transform.position.y, origin.position.z);
        Vector3 tmp = (head.transform.position - origin.position).normalized;
        tmp.y = 0;
        tmp*=offset;
        bezierPoint[3] = head.transform.position - tmp;
        point1.transform.position = bezierPoint[1];
        point2.transform.position = bezierPoint[2];
        point3.transform.position = bezierPoint[3];
    }
    public void TracerBezierCourbe()
    {
        for(int i=0;i<subdivision;i++)
        {
            
            GetComponent<LineRenderer>().SetPosition(i, PolynBernstein((float)1 / (float)subdivision) * i);
        }
    }
    public int Fact(int n)
    {
        if(n==0)
        {
            return 1;
        }
        int pi = 1;
        for(int i=1;i<=n;i++)
        {
            n *= i;
        }
        return pi;
    }
    public float CoefBinomial(int n,int k)
    {
        return Fact(n) / (Fact(k) * Fact(n - k));
    }
    public Vector3 PolynBernstein(float t)
    {
        int n= 4;
        t = Mathf.Clamp(t, 0, 1);
        Vector3 sigma=Vector3.zero;
        for (int i = 0; i <= n; i++)
        {
            Debug.Log(i);
            sigma += CoefBinomial(n, i) * Mathf.Pow(1 - t, n - i) * Mathf.Pow(t, i) * bezierPoint[i];
        }
        return sigma;

    }
}
