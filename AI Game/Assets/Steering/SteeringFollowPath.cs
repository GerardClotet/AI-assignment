using UnityEngine;
using System.Collections;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;

public class SteeringFollowPath : MonoBehaviour {

	Move move;
	SteeringSeek seek;

    public float ratio_increment = 0.1f;
    public float min_distance = 1.0f;
    float current_ratio = 0.0f;

    public GameObject Path;
    // Use this for initialization
    //private
    private BGCurve curve;
    private BGCcMath bgMath;
    private BGCurveBaseMath.SectionInfo section;
    private float near_dest;



    private float distance;
    private float tdist =0;
    public GameObject test;
    enum CALCTYPE { CLOSESTPOINT,FOLLOWPATH};
    CALCTYPE calctype;
    void Start () {

        calctype = CALCTYPE.CLOSESTPOINT;
		move = GetComponent<Move>();
		seek = GetComponent<SteeringSeek>();
        curve = Path.GetComponent<BGCurve>();
        bgMath = Path.GetComponent<BGCcMath>();
        // TODO 1: Calculate the closest point from the tank to the curve
      //  section = Path.GetComponent<BGCurveBaseMath.SectionInfo>();


        int nearPoint = -1;
        var points = curve.Points;
        var pointsCount = points.Length;
      
        float[] distances = new float[pointsCount];
        float shortestdistance = 0;
        for (int i = 0; i < pointsCount; i++)
        {
            Vector3 pp =test.transform.position - points[i].PositionWorld;
            distances[i] = pp.magnitude;

            if(i == 0)
            {
                shortestdistance = pp.magnitude;
            }
            else if( shortestdistance > pp.magnitude)
            {
                shortestdistance = pp.magnitude;
            }
        }
        
        //cada seccio té 15 parts
        int parts = bgMath.SectionParts;
        Debug.Log(parts);
       // Vector3 cdis = points[0].PointTransform.position;
        //distance = test.transform.position - cdis;
	}
	
	// Update is called once per frame
	void Update () 
	{

        //var points = curve.Points;
        //var pointsCount = points.Length;
        //Debug.Log(section);
        //for(int i =0; i < pointsCount; i++)
        //{
        //    Debug.Log(points[i]);
        //    //points[i];

        //}
        Vector3 tangent;
        float ndistance;

        switch (calctype)
        {
            case CALCTYPE.CLOSESTPOINT:
                Vector3 destination = bgMath.CalcPositionByClosestPoint(test.transform.position, out ndistance, out tangent);
                Vector3 direction = destination - test.transform.position;
                float mg = direction.magnitude;
                test.transform.position += direction.normalized * 5 * Time.deltaTime;
                if (Mathf.Abs(test.transform.position.x - destination.x) < 1 && Mathf.Abs(test.transform.position.y - destination.y) < 1)
                    calctype = CALCTYPE.FOLLOWPATH;
                test.transform.rotation = Quaternion.LookRotation(tangent);
                break;

            case CALCTYPE.FOLLOWPATH:
                distance += 5f * Time.deltaTime;
                test.transform.position = bgMath.CalcPositionAndTangentByDistance(distance, out tangent);
                test.transform.rotation = Quaternion.LookRotation(tangent);
                break;
        }
        
        //Dest Position

        
    
           
        //distance += 5f * Time.deltaTime;
        //test.transform.position = bgMath.CalcPositionAndTangentByDistance(ndistance, out tangent);

        

        // TODO 2: Check if the tank is close enough to the desired point
        // If so, create a new point further ahead in the path
    }

 
	void OnDrawGizmosSelected() 
	{

		if(isActiveAndEnabled)
		{
			// Display the explosion radius when selected
			Gizmos.color = Color.green;
			// Useful if you draw a sphere were on the closest point to the path
		}

	}
}
