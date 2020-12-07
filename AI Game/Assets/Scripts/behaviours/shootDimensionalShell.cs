using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using BBUnity.Actions;        // GOAction

using UnityEngine;


[Action("Tank/ShootDimensionalShell")]
[Help("Tanks Patrol throuhg control points")]
public class shootDimensionalShell : GOAction
{
    private GameObject shell;

    public override void OnStart()
    {
        if(shell ==null)
            //Resources.Load()
        base.OnStart();
    }


}
