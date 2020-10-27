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


    private Vector3[] startRot_Pos;
    enum CALCTYPE { CLOSESTPOINT,FOLLOWPATH,NONE};
    CALCTYPE calctype;
    void Start () {

        calctype = CALCTYPE.CLOSESTPOINT;
		move = GetComponent<Move>();
		seek = GetComponent<SteeringSeek>();
        curve = Path.GetComponent<BGCurve>();
        bgMath = Path.GetComponent<BGCcMath>();
        // TODO 1: Calculate the closest point from the tank to the curve
      //  section = Path.GetComponent<BGCurveBaseMath.SectionInfo>();
       startRot_Pos = GetClosestPoint_GetOrientation();



    }
	
	// Update is called once per frame
	void Update () 
	{

        switch (calctype)
        {
            case CALCTYPE.CLOSESTPOINT:
                ApproachClosestPoint();
                break;
            case CALCTYPE.FOLLOWPATH:
                FollowPath();
                break;
        }

        

        // TODO 2: Check if the tank is close enough to the desired point
        // If so, create a new point further ahead in the path
    }

 
    private Vector3[] GetClosestPoint_GetOrientation()
    {
        Vector3 tangent;
        Vector3 destination = bgMath.CalcPositionByClosestPoint(test.transform.position, out distance, out tangent);
        Vector3[] dir_rot = new Vector3[2];
        dir_rot[0] = destination;
        dir_rot[1] = tangent;
        return dir_rot;
    }

    private void ApproachClosestPoint()
    {
        Vector3 direction = startRot_Pos[0] - test.transform.position;
        test.transform.rotation = Quaternion.LookRotation(startRot_Pos[1]);

        test.transform.position += direction.normalized * 5 * Time.deltaTime;

        if (Mathf.Abs((startRot_Pos[0] - test.transform.position).magnitude) <= 0.1f)
            calctype = CALCTYPE.FOLLOWPATH;
    }

    private void FollowPath()
    {
        float totaDistance = bgMath.GetDistance();
        if (totaDistance <= distance)
            distance = 0;
        Vector3 tangent;
        test.transform.position = bgMath.CalcPositionAndTangentByDistance(distance, out tangent);
        test.transform.rotation = Quaternion.LookRotation(tangent);
        distance += 5f * Time.deltaTime;
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
