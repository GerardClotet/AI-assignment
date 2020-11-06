using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class WanderTest : MonoBehaviour
{
    // Start is called before the first frame update

    private NavMeshAgent agent;

    public GameObject enemyTank;
    private float freq = 0f;
    // SteeringWander wander;
    SteeringSeek seek;
    SteeringAlign aling;
    NavMeshPath nav_path;
    void Start()
    {
        //TODO ALIGN THROUGH STEERING
        agent = GetComponent<NavMeshAgent>();
        seek = GetComponent<SteeringSeek>();
        aling = GetComponent<SteeringAlign>();
        agent.updatePosition = false;
        //agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(agent != null)
        {
            if(!agent.pathPending)
            {
                agent.destination = enemyTank.transform.position;
            }

            if (!agent.pathPending)
            {
                freq += Time.deltaTime;
                if (freq > 0.5)
                {
                    freq -= 0.5f;
                    // agent.SetDestination(enemyTank.transform.position);
                    agent.CalculatePath(enemyTank.transform.position, agent.path);
                }
                aling.SetTarget(agent.path.corners[0]);

               // if(aling.GetDiffAbs()==0f)
                    seek.Steer(agent.path.corners[0]);
            }

        }

    }
}
