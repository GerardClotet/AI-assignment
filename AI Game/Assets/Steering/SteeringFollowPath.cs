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
    void Start () {
		move = GetComponent<Move>();
		seek = GetComponent<SteeringSeek>();
        curve = Path.GetComponent<BGCurve>();
        bgMath = Path.GetComponent<BGCcMath>();
        // TODO 1: Calculate the closest point from the tank to the curve
        section = Path.GetComponent<BGCurveBaseMath.SectionInfo>();
        //bgMath.sec
        //var n_dst = bgMath.CalcByDistance()
	}
	
	// Update is called once per frame
	void Update () 
	{
        var points = curve.Points;
        var pointsCount = points.Length;
        Debug.Log(section);
        for(int i =0; i < pointsCount; i++)
        {
            Debug.Log(points[i]);
            //points[i];
            
        }

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
