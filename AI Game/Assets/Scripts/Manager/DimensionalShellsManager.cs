using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionalShellsManager : MonoBehaviour
{

    public GameObject portalVFX;
    public GameObject attackPortal;
    [HideInInspector]
    public List<GameObject> DimensionalShells;

    private GameObject blueTank;
    private GameObject redTank;

    private int portals_per_attack = 5;
    private int red_portals=0; //Todo create a single variable per object list, maybe with a map/Dictionary
    private int blue_portals=0;

    void Start()
    {
        blueTank = gameObject.GetComponent<GameManager>().blueTank;
        redTank = gameObject.GetComponent<GameManager>().redTank;
    }

    public void PortalCollider(GameObject original_shell)
    {

        for(int i =0; i < DimensionalShells.Count; i++)
        {
            if (DimensionalShells[i] == original_shell)
            {
                GameObject[] tmp_tank = CheckTank(original_shell.GetComponent<DimensionalShell_movement>().GetEnemyGameObjectAttached());
                if (tmp_tank == null)
                    Debug.Log("no tank attached to DimensionalShell");
                else
                {
                    if (tmp_tank[1].name == redTank.name)
                        StartCoroutine(CreateAttackPortalsCorroutine(tmp_tank, red_portals)); //It's called too many time check it out;

                    else StartCoroutine(CreateAttackPortalsCorroutine(tmp_tank,blue_portals));//Also destroy gameobject
                }
                DimensionalShells.RemoveAt(i);
                Destroy(original_shell);
                break;
            }
        }

    }

    public void CreateFirstPortal(GameObject shell_attached)
    {
        //pos
        Vector3 curr_velocity = shell_attached.GetComponent<Rigidbody>().velocity;
        Vector3 cur_position = shell_attached.transform.position;
        Vector3 future_position = cur_position + curr_velocity * Time.deltaTime*10;

        GameObject objShell = Instantiate(portalVFX, future_position, Quaternion.identity) as GameObject;

        //rot
        Vector3 direction = cur_position - objShell.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        objShell.transform.localRotation = Quaternion.Lerp(objShell.transform.rotation, rotation, 1);

        objShell.GetComponent<shell_portal>().Init();
        objShell.GetComponent<shell_portal>().GameObjectAttached(shell_attached.GetComponent<DimensionalShell_movement>().GetFriendlyGameObjectAttached());
    }

    public Vector3 CreateAttackPortals(GameObject enemytank)
    {

        Vector3 angle = Random.onUnitSphere;
        angle.y = Mathf.Abs(angle.y);
        if (angle.y < 0.4f)
            angle.y = 0.4f;
   

        Ray raig = new Ray(enemytank.transform.position,angle);

        return raig.GetPoint(10f);

    }

    IEnumerator CreateAttackPortalsCorroutine(GameObject[] tank, int portal_counter ) //need to pass enemy tank;
    {
        CreateAttackPortals(tank[1]);//enemytank


        while( portal_counter <portals_per_attack)
        {
            GameObject portal = Instantiate(attackPortal, CreateAttackPortals(tank[1]), Quaternion.identity) as GameObject;

            RotateTo(portal, tank[1].transform.position);

            portal.GetComponent<portal_attack>().SetDestination(tank[1].transform.position);
            //Instantiate(portalVFX)
            portal_counter++;

            yield return new WaitForSeconds(2f);

        }

        tank[0].GetComponentInChildren<SpawnProjectile>().ResetPortalExecution();

        Debug.Log("end coroutine");
        if (tank[1].name == redTank.name) //update! now may be correct bc tank only launched dimensional bullet when the proces has finished//Not correct, maybe the same tank launches a dimensional bullet before the previous one created the 5 portals --> see correction on the variables 
            red_portals = 0; 
        else blue_portals = 0;


  
    }

    public void AddtoList(GameObject shell)
    {
        if (shell.GetComponent<DimensionalShell_movement>().GetEnemyGameObjectAttached() == null)
            Destroy(shell);
        else
        DimensionalShells.Add(shell);
    }

    GameObject[] CheckTank(GameObject tank)
    {
        if (tank.name == redTank.name)
        {
            GameObject[] tst = { redTank, blueTank };
            return  tst;
        }
        else if (tank.name ==blueTank.name)
        {
            GameObject[] tst = { blueTank, redTank };

            return tst;
        }
         else   return null;
    }
    // Update is called once per frame


    void RotateTo(GameObject obj, Vector3 destination)
    {
        Vector3 direction = destination - obj.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
    }


}


