using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class WanderTest : MonoBehaviour
{
    // Start is called before the first frame update

    public NavMeshAgent agent;

    public Camera cam;
    public GameObject enemyTank;
    private float freq = 0f;
    // SteeringWander wander;
    SteeringSeek seek;
    NavMeshPath nav_path;
    void Start()
    {
        seek = GetComponent<SteeringSeek>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if(Physics.Raycast(ray,out hit))
        //    {
        //        agent.SetDestination(enemyTank.transform.position);
        //    }
        // }
        freq += Time.deltaTime;
        if (freq > 0.5)
        {
            freq -= 0.5f;
            agent.SetDestination(enemyTank.transform.position);
            if (agent.CalculatePath(enemyTank.transform.position, agent.path))
            {

              if(agent.path.status == NavMeshPathStatus.PathComplete)
              {
                    Debug.Log("Path Complete");
              }

              else if (agent.path.status == NavMeshPathStatus.PathInvalid)
              {
                    Debug.Log("Path INVALID");
              }

              else if (agent.path.status == NavMeshPathStatus.PathPartial)
              {
                    Debug.Log("Path PARTIAL");
              }

                int i = agent.path.corners.Length;

               seek.Steer(agent.path.corners[i-1]);
            }

           
        }

    }
}
