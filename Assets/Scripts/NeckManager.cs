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
    [SerializeField] GameObject head;
    [SerializeField] GameObject point1;
    [SerializeField] GameObject point2;
    [SerializeField] GameObject breath;
    [SerializeField] Transform pivot1;
    [SerializeField] Transform pivot2;
    //[SerializeField] Transform pivot3;
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

    public bool freeze;

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
        freeze = false;
        pivot2.localPosition = new Vector3(0, 4 * offset, 0);
        //point1.transform.position = origin.position + new Vector3(0, 3 * offset, 0);
    }

    // Update is called once per frame
    void Update()
    {
        PlacerLesPoint();
        TracerBezierCourbe();
        Ondule();
        DeepThroat();
    }
    public void PlacerLesPoint()
    {

        Vector3 DirectionBezier1 = new Vector3(0, 3 * offset, 0);
        Vector3 DirectionBezier2 = new Vector3(0, 4 * offset, 0);
        pivot1.localRotation=Quaternion.Euler(new Vector3(0,0,headController.transform.eulerAngles.z));
        pivot2.localPosition = new Vector3(0, 4 * offset, 0);
        if((headController.transform.eulerAngles.z<360) && (headController.transform.eulerAngles.z > 180))
        {
           
            point1.transform.position = origin.position + DirectionBezier1;
            //point1.transform.position += new Vector3(headController.transform.eulerAngles.x, headController.transform.eulerAngles.z, headController.transform.eulerAngles.y);
        }
        else
        {
            point1.transform.position = origin.position + DirectionBezier1;
        }
        bezierPoint[3] = headController.transform.position;
        //point2.transform.position = headController.transform.position + DirectionBezier2;
        /*if (point1.transform.position.y<offset)
        {
            point1.transform.position= new Vector3(point1.transform.position.x,2*offset, point1.transform.position.z);
        }
        if (point2.transform.position.y < 0)
        {
            //point2.transform.position = new Vector3(point2.transform.position.x, 0, point2.transform.position.z);
        }       */
        bezierPoint[1] = point1.transform.position;
        bezierPoint[2] = pivot2.position;
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
    public void DeepThroat()
    {
        if (!freeze)
        {
            head.transform.localScale =Vector3.one*2;
            int tmp = (int)(head.GetComponent<HeadBossManager>().counterCadance / head.GetComponent<HeadBossManager>().cadance * subdivision);
            if(tmp>subdivision)
            {
                tmp = subdivision;
            }
            breath.transform.position = GetComponent<LineRenderer>().GetPosition(tmp);
        }
        else
        {
            head.transform.localScale += new Vector3(2,2,2)*Time.deltaTime;
        }
                
    }
}
