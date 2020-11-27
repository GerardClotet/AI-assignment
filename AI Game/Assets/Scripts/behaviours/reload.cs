using UnityEngine;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // ConditionBase

[Condition("Tank/Reload")]
[Help("Checks if the tanks has any bullet, if it hasnt return false & makes the tank do de action to go for it")]

public class reloadCondition : ConditionBase
{
   [InParam("TANK_turret")] 
    public GameObject tank_turret;

    private SpawnProjectile spawnProjectile;
    // private TankShells
    public override bool Check() // This Functions determinetas if teh sequencer passes or continues with the taks is conditioning
    {
        if (SearchAmmo())
            return spawnProjectile.noAmmo;
        else
            return false;
       
        //If returns true it means there is no ammo so it goes for ammmo in his task below in the BBnode
    }

    //public void AmmoAdquired() //
    //{
    //    spawnProjectile.OutofAmmo -= Recharge; //Unsubscribe event
    //    EndMonitorWithSuccess(); //End condition
    //}
    //public override TaskStatus MonitorCompleteWhenTrue()
    //{
    //    if (Check())
    //        return TaskStatus.COMPLETED; //There is ammo right now
    //    else
    //    {
    //        if(spawnProjectile != null)
    //        {
    //            spawnProjectile.OutofAmmo += Recharge; //subcrive to the event
    //        }
    //        return TaskStatus.SUSPENDED;

    //    }
    //    // return base.MonitorCompleteWhenTrue();
    //}
    void Recharge()
    {
        Debug.Log("ei");
        //called when there is no ammo
    }

    private bool SearchAmmo()
    {
        if (spawnProjectile != null)
            return true;
        if(tank_turret != null)
            spawnProjectile = tank_turret.GetComponent<SpawnProjectile>();

        if (spawnProjectile == null)
            return false;

        //spawnProjectile = gam
        return spawnProjectile != null;
    }
}
