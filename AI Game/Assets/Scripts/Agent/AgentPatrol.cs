using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class AgentPatrol : MonoBehaviour
{


    //Patrol + navmesh
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;

    //Seek
    public SteeringSeek seek;
    private Vector3 seekdest;

    //Frewquency
    private float freq = 0f;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.autoBraking = false;
        agent.updatePosition = false;
        //agent.updateRotation = false;
        //seek.GetComponent<SteeringSeek>();
    }

    void GetRandomPoint()
    {
       float destination = Random.Range(0, points.Length);
        if(destination == destPoint)
        {
            Vector3 v = Vector3.positiveInfinity;
            int sebi = 0;
            for (int i =0; i<points.Length;i++)
            {
                Vector3 vec = points[i].position - gameObject.transform.position;

                if (vec.magnitude < v.magnitude && i != destPoint )
                {
                    v = vec;
                    sebi = i;
                }
            }

            destPoint = sebi;
        }
    }
    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        GetRandomPoint();
        agent.destination = points[destPoint].position;
       
        seekdest = agent.destination;
        

        //destPoint = (destPoint + 1) % points.Length;

        //if (points.Length == destPoint)
        //    destPoint = 0;

    }


    public void DoAction()
    {
        if (agent != null)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GotoNextPoint();
            }

            if (!agent.pathPending)
            {
                freq += Time.deltaTime;

                if (freq >= 0.5)
                {
                    freq -= .5f;
                    agent.CalculatePath(seekdest, agent.path);
                }

                seek.Steer(agent.path.corners[0]);
            }

        }
    }

}
