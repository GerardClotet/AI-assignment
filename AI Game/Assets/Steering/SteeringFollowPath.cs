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



    private Vector3 distance;
    private float tdist =0;
    public GameObject test;

    void Start () {
		move = GetComponent<Move>();
		seek = GetComponent<SteeringSeek>();
        curve = Path.GetComponent<BGCurve>();
        bgMath = Path.GetComponent<BGCcMath>();
        // TODO 1: Calculate the closest point from the tank to the curve
      //  section = Path.GetComponent<BGCurveBaseMath.SectionInfo>();


        int nearPoint = -1;
        var points = curve.Points;
        var pointsCount = points.Length;
      
       // Debug.Log(section);
        float shortestpoint =-1;

        // Get Shortest point of the curve & distance
        for (int i = 0; i < pointsCount; i++)
        {
            Debug.Log(points[i]);
            Vector3 pp =test.transform.position - points[i].PositionWorld;


            if(pp.magnitude < shortestpoint && shortestpoint != -1)
            {
                shortestpoint = pp.magnitude;
                nearPoint = i;
                Debug.Log(shortestpoint);
                Debug.Log(nearPoint);
            }
            //points[i];

        }
        //cada seccio té 15 parts
        int parts = bgMath.SectionParts;
        Debug.Log(parts);
        Vector3 cdis = points[0].PointTransform.position;
        distance = test.transform.position - cdis;
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
