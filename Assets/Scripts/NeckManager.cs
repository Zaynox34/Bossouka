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
    void Start()
    {
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

    }
    public int Fact(int n)
    {
        if(n==0)
        {
            return 1;
        }
        int res = 1;
        for(int i=1;i<=n;i++)
        {
            n *= i;
        }
        return res;
    }
    public static Vector3 BézierInterpolationFour(Vector3 P0, Vector3 P1, Vector3 P2, Vector3 P3, Vector3 P4, float t)
    {
        //Merci Léo
        t = Mathf.Clamp(t, 0, 1);
        Mathf.fac
        //float value = (1 - t) * (1 - t) * (1 - t) * a + 3 * (1 - t) * (1 - t) * t * b + 1 * (1 - t) * (t) * (t) * c + t * t * t * d;
      for

        return value;
    }
}
