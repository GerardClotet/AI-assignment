using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using BBUnity.Actions;        // GOAction
using UnityEngine.AI;
using UnityEngine;

[Action("Tank/GoForAmmo")]
[Help("Goes for the selected place to get ammo")]

public class goForAmmo : GOAction
{
    [InParam("Reload Place")]
    public Transform reload_place;

    private NavMeshAgent agent;
    private SteeringSeek seek;
   // private 
    // Start is called before the first frame update
    public override void OnStart()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        seek = gameObject.GetComponent<SteeringSeek>();
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        //pseudoo code
        // if(tank.bullets ==0)
        //TaskStatus.Failed o Pending ?
        //


        //Se if it needs to be completed if tank reaches the patrol point or always & its always repeated

        // return TaskStatus.RUNNING;
        if (agent == null)
            return TaskStatus.FAILED;

        else GoToPoint();

        base.OnUpdate();
        return TaskStatus.RUNNING;


    }

    public void GoToPoint()
    {
        if (agent != null)
        {
            if (!agent.pathPending)
            {
                agent.destination = reload_place.position;
            }

            if (!agent.pathPending)
            {
                
                    // agent.SetDestination(enemyTank.transform.position);
                    agent.CalculatePath(reload_place.position, agent.path);
                
                //aling.SetTarget(agent.path.corners[0]);

                // if(aling.GetDiffAbs()==0f)
                seek.Steer(agent.path.corners[0]);
            }

        }
    }
}
