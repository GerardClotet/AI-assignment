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
    void Start()
    {
        
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

        }

    }
}
