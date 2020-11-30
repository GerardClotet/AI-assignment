using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionalShellsManager : MonoBehaviour
{

    public GameObject portalVFX;

    [HideInInspector]
    public List<GameObject> DimensionalShells;

    private GameObject blueTank;
    private GameObject redTank;

    private int portals_per_attack = 5;
    private int red_portals=0;
    private int blue_portals=0;

    //testing
    bool drawing = false;
    GameObject tanktype;
    Vector3 direccio;
    //-----------

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
                GameObject[] tmp_tank = CheckTank(original_shell.GetComponent<DimensionalShell_movement>().GetGameObjectAttached());
                if (tmp_tank == null)
                    Debug.Log("no tank attached to DimensionalShell");
                else
                {
                    if (tmp_tank[1].name == redTank.name)
                        StartCoroutine(CreateAttackPortalsCorroutine(tmp_tank, red_portals));

                    else StartCoroutine(CreateAttackPortalsCorroutine(tmp_tank,blue_portals));//Also destroy gameobject
                }
                //CreateAttackPortals(tmp_tank[1]);
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
            GameObject portal = Instantiate(portalVFX, CreateAttackPortals(tank[1]), Quaternion.identity) as GameObject;

            RotateTo(portal, tank[1].transform.position);
            //Instantiate(portalVFX)
            portal_counter++;

            yield return new WaitForSeconds(2f);

        }

        Debug.Log("end coroutine");
        portal_counter = 0;

  
    }

    public void AddtoList(GameObject shell)
    {
        DimensionalShells.Add(shell);
    }

    GameObject[] CheckTank(GameObject tank)
    {
        if (tank.name == "RedTank")
        {
           
            return new GameObject[] { redTank, blueTank};
        }
        else if (tank.name =="BlueTank")
        {
            return new GameObject[] { blueTank, redTank };
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


