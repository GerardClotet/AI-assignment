using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class SteeringWander : MonoBehaviour {

	public Vector3 offset = Vector3.zero;
	public float radius = 1.0f;
	public float min_update = 0.5f;
	public float max_update = 3.0f;

	SteeringSeek seek;
	Vector3 random_point;

    private NavMeshAgent agent;

	void Start () {
		seek = GetComponent<SteeringSeek>();
        agent = GetComponent<NavMeshAgent>();
		//ChangeTarget();
	}


    // Update is called once per frame
    public void ChangeTarget () 
	{
		random_point = Random.insideUnitSphere;
		random_point *= radius;
		random_point += transform.position + offset;
		random_point.y = transform.position.y;

        agent.CalculatePath(random_point, agent.path);
        //seek.Steer()
		Invoke("ChangeTarget", Random.Range(min_update, max_update));
	}

    public void DoAction()
    {
        if (!agent.pathPending)
            seek.Steer(agent.path.corners[0]);


    }

	void OnDrawGizmosSelected() 
	{
		if(this.isActiveAndEnabled)
		{
			// Display the explosion radius when selected
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(transform.TransformPoint(offset), radius);
		
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(random_point, 0.2f);
		}
	}
}
