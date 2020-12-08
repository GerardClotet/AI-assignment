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

    [InParam("UI_EXCLAMATION")]
    public GameObject exclamation_UI;
 
    private SpawnProjectile spawner;
    // Start is called before the first frame update
    private SteeringWander wander;

   // private GameObject exclamation_UI;
    public override void OnStart()
    {
        if (exclamation_UI != null)
            exclamation_UI.SetActive(true);
       
        if (tank != null)
        {
            spawner = tank.GetComponentInChildren<SpawnProjectile>();
            wander = tank.GetComponent<SteeringWander>();
            wander.enabled = true;
            if(spawner.isInSight)
                wander.DeleteAgenPath();
            wander.ChangeTarget();
        }

        base.OnStart();
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        if (spawner == null)
            return TaskStatus.FAILED;

        wander.DoAction();

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
        //Debug.Log("Aborted");
        if(exclamation_UI != null)
            exclamation_UI.SetActive(false);

        tank.GetComponent<SteeringWander>().enabled = false;
        base.OnAbort();

    }
}
