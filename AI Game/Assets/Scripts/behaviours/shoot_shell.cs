using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using BBUnity.Actions;        // GOAction
using UnityEngine;

[Action("Tank/Shoot")]
[Help("Shoots between 3 differents types of shells")]
public class shoot_shell : GOAction
{
    [InParam("Tank")]
    public GameObject tank;

    [InParam("ShotsDelayTime")]
    public float reload_time= 1f;

    private SpawnProjectile spawner;
    private float aux_time;
    // Start is called before the first frame update
    public override void OnStart()
    {
        if (tank != null)
            spawner = tank.GetComponentInChildren<SpawnProjectile>();

        aux_time = reload_time;
        base.OnStart();
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        if (spawner == null)
            return TaskStatus.FAILED;


        if(spawner.GetState()==0)
        {
            spawner.SwitchState(1);
        }
        //if(spawner.GetReloadTime() <=0)
        //{
        //    spawner.ShootUpdate();
        //    spawner.SetReloadTime(spawner.cadence);
        //}

        base.OnUpdate();
        return TaskStatus.RUNNING;
        //Add wander movement while is shooting, ui symbols
        
    }

    public override void OnAbort()
    {
        spawner.SwitchState(0);
        Debug.Log("Aborted");
        base.OnAbort();
    }
}
