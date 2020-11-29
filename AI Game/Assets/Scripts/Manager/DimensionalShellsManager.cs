using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionalShellsManager : MonoBehaviour
{

    public GameObject portalVFX;

    private List<GameObject> DimensionalShells;

    private GameObject blueTank;
    private GameObject redTank;
    // Start is called before the first frame update
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
                StartCoroutine();//Also destroy gameobject
                DimensionalShells.RemoveAt(i);
                break;
            }
        }

    }

    public void CreateFirstPortal(GameObject shell_attached)
    {
        //pos
        Vector3 curr_velocity = shell_attached.GetComponent<Rigidbody>().velocity;
        Vector3 cur_position = shell_attached.transform.position;
        Vector3 future_position = cur_position + curr_velocity * Time.deltaTime;

        GameObject objShell = Instantiate(portalVFX, future_position, Quaternion.identity) as GameObject;

        //rot
        Vector3 direction = cur_position - objShell.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        objShell.transform.localRotation = Quaternion.Lerp(objShell.transform.rotation, rotation, 1);
    }

    public void StartCoroutine()
    {

    }
    public void AddtoList(GameObject shell)
    {
        DimensionalShells.Add(shell);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
