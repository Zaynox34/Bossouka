using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeckManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Transform")]
    [SerializeField] Transform origin;
    [Header("GameObject")]
    [SerializeField] GameObject headController;
    [SerializeField] GameObject point1;
    [SerializeField] GameObject point2;
    [Header("List")]
    [SerializeField] List<Vector3> bezierPoint;
    [SerializeField] List<Vector3> bezierCurvePoint;
    [Header("Parameter")]
    [SerializeField] float offset;
    [SerializeField] int subdivision;
    [SerializeField] float thickness;
    [SerializeField] float numberOscilation;
    [SerializeField] float speedOscilationX;
    [SerializeField] float speedOscilationY;
    [SerializeField] float amplitudeOscilation;
    [SerializeField] float offsetX;
    [SerializeField] float offsetY;
    
    bool test;
    void Start()
    {
        GetComponent<LineRenderer>().startWidth = thickness;
        GetComponent<LineRenderer>().endWidth = thickness;
        GetComponent<LineRenderer>().positionCount = subdivision+1;
        bezierPoint.Add(origin.position);
        test = false;
        for (int i = 1; i < 4; i++)
        {
            bezierPoint.Add(new Vector3(0, 0, 0));
        }
        offsetX = 0;
        offsetY = 0;
        for (int i = 0; i <=subdivision; i++)
        {
            bezierCurvePoint.Add(new Vector3(0, 0, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlacerLesPoint();
        TracerBezierCourbe();
        Ondule();
    }
    public void PlacerLesPoint()
    {
        point1.transform.position = origin.position + new Vector3(0, 3*offset,0);
        bezierPoint[3] = headController.transform.position;
        Vector3 tmp = (headController.transform.position - origin.position).normalized;
        tmp.y = 0;
        tmp*=offset;
        point2.transform.position = headController.transform.position + new Vector3(0, 4 * offset, 0);

        bezierPoint[1] = point1.transform.position;
        bezierPoint[2] = point2.transform.position;
    }
    public void TracerBezierCourbe()
    {
        for (int i = 0; i <= subdivision; i++)
        {
            if (!test)
            {
                //Debug.Log(((float)i / (float)subdivision));
            }
            bezierCurvePoint[i] = PolynBernstein((float)i / (float)subdivision);
            //GetComponent<LineRenderer>().SetPosition(i, PolynBernstein((float)i/(float)subdivision));
        }
        test = true;
    }
    public void Ondule()
    {
        for (int i = 0; i <=subdivision; i++)
        {
            Vector3 tmpVector = new Vector3(
                0,
                Mathf.Sin(((i + offsetY) % (subdivision / numberOscilation)) * (Mathf.PI * 2) / (subdivision / numberOscilation)) * amplitudeOscilation,
               0
                );

            GetComponent<LineRenderer>().SetPosition(i, bezierCurvePoint[i] + tmpVector);

           
        }
        offsetX += speedOscilationX;
        offsetY += speedOscilationY;
        offsetX = offsetX%subdivision;
        offsetY = offsetY % subdivision;
    }
    public int Fact(int n)
    {
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
        int n= 3;
       // t = Mathf.Clamp(t, 0, 1);
        Vector3 sigma=Vector3.zero;
        for (int i = 0; i <= n; i++)
        {
            sigma += CoefBinomial(n, i) * Mathf.Pow(1 - t, n - i) * Mathf.Pow(t, i) * bezierPoint[i];
        }
        return sigma;

    }
}
