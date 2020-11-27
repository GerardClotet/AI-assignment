using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using BBUnity.Actions;        // GOAction


using UnityEngine;

[Action("Tank/Patrol")]
[Help("Tanks Patrol throuhg control points")]
public class patrol : GOAction
{

    [InParam("Tank")]
    public GameObject tank;


    private AgentPatrol a_patrol;
    public override void OnStart()
    {
        //Agent patrol must be controled thhrough here
        if (tank != null)
            a_patrol = tank.GetComponent<AgentPatrol>();

        //a_patrol.Start();

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
        if (a_patrol == null)
            return TaskStatus.FAILED;

        a_patrol.DoAction();
        base.OnUpdate();
        return TaskStatus.RUNNING;


    }
    public override void OnAbort()
    {
        Debug.Log("Patroling Aborted");
        base.OnAbort();
    }


    //OnStart() : used when the action is first started.Subclasses can do whatever initialization they need here.
    //OnEnd(): called when the action ends with COMPLETED.
    //OnFailedEnd(): called when the action ends with FAILED.
    //OnAbort(): called when the execution engine aborts the execution of the action.This usually occurs if a higher priority action 
    //must be started because some condition has changed (in a priority selector). Actions have here the opportunity to
    //graciously stop its execution and release any resources.
}
