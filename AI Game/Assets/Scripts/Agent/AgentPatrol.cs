using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class AgentPatrol : MonoBehaviour
{
    // Start is called before the first frame update

    //Patrol + navmesh
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;

    //Seek
    public SteeringSeek seek;
    private bool seekpoint = false;
    private Vector3 seekdest;

    //Frewquency
    private float freq = 0f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.autoBraking = false;
        agent.updatePosition = false;
        //agent.updateRotation = false;
        //seek.GetComponent<SteeringSeek>();
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;
        seekdest = agent.destination;
        

        destPoint = (destPoint + 1) % points.Length;

        if (points.Length == destPoint)
            destPoint = 0;

    }
    // Update is called once per frame
    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.

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
