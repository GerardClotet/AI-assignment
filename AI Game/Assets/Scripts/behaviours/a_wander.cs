using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using BBUnity.Actions;        // GOAction


using UnityEngine;

[Action("Tank/WanderBehaviour")]
[Help("Tanks Wander in random positions")]
public class Wander_behaviour : GOAction
{
    [InParam("Tank")]
    public GameObject tank;

    // Start is called before the first frame update
    private WanderTest a_wander;
    public override void  OnStart()
    {
        if (tank != null)
            a_wander = tank.GetComponent<WanderTest>();


        base.OnStart();
    }


    public override TaskStatus OnUpdate()
    {
        if (a_wander == null)
            return TaskStatus.FAILED;

        a_wander.DoAtion();

       // base.OnUpdate();
        return TaskStatus.RUNNING;
    }
    // Update is called once per frame

    public override void OnAbort()
    {
        Debug.Log("Patroling Aborted");
        base.OnAbort();
    }
}
