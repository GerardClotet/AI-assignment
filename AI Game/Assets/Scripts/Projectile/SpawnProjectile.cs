using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnProjectile : MonoBehaviour
{
    enum States
    {
        PATROLLING,
        ENEMY,
    }
    States tank_states;

    // Start is called before the first frame update
    public GameObject shell;
    public GameObject megaShell;
    public GameObject dimensionalShell;
    public Transform startPos;
    public Transform endPos;

    //reload
    public float cadence = 4f;
    private float reload_time;
    //recoil
    public GameObject turret;
    public float recoilSpeed = 7f;
    private readonly float recoilTime = 1f; //maxtime to back & forth
    private float recoilingAmount = 0f;
    private bool recol_turret = false;
    private bool recolback = true;
    private Vector3 originRecoilPos;
    private Vector3 endRecoilPos;

    //Ammo Events
    private int bullets = 2; //TESTING BULLET
    public delegate void RechargeAction();
    public event RechargeAction NoBullets;

    public GameObject ammo_ui; //previous Ammo

    public bool noAmmo { get; private set; }

    //Raycasting
    public LayerMask layer;
    public bool isInSight = false;
    //PortalAffairs
    private bool portal_execution = false;

    
    public void SetAmmoIconFalse()
    {
        if (ammo_ui != null) //Change to Ammo Icon
            if (ammo_ui.activeSelf)
                ammo_ui.SetActive(false);
    }
    public void ResetPortalExecution()
    {
        portal_execution = false;
    }
    public bool GetPortalExtecution()
    {
        return portal_execution;
    }
    void Start()
    {
        reload_time = cadence;
        noAmmo = false;
        tank_states = States.PATROLLING;
        //ammo_ui.SetActive(false);
        //GameObject objShell = Instantiate(shell, startPos.position, Quaternion.identity) as GameObject;
        //RotateTo(objShell, endPos.position);
    }
    // Update is called once per frame
    
    private void Update()
    {
        reload_time -= Time.deltaTime;
        switch (tank_states)
        {
            case States.ENEMY:
                ShootUpdate();
                break;
            case States.PATROLLING:

                break;
        }
        //tank_bullets >
       
    }
    void ShootUpdate()
    {
        if (!recol_turret)
        {
            if (bullets != 0)
            {

                if (ammo_ui != null) //Change to Ammo Icon
                    if (ammo_ui.activeSelf)
                        ammo_ui.SetActive(false);


                if (reload_time <= 0)
                {

                    Vector3 dir = endPos.position - startPos.position;
                    if (!Physics.Raycast(startPos.position, dir.normalized, dir.magnitude, layer))
                    {
                        isInSight = true;
                        recol_turret = true;


                        int i = Random.Range(1, 5);
                        if (i == 1)
                            SpawnMegaShell();

                        else if (i == 2 && !portal_execution)
                        {
                            portal_execution = true;
                            SpawnDimensionalShell();
                        }
                        else SpawnShell();



                        bullets -= 1; //reduce ammo

                        reload_time = cadence;


                    }
                    else isInSight = false;

                }
            }
            else if (bullets == 0)
            {

                noAmmo = true;


                if (ammo_ui != null)
                {
                    if (!ammo_ui.activeSelf)
                        ammo_ui.SetActive(true);
                }

                if (NoBullets != null)
                {
                    //Debug.Log("Calling Event");
                    NoBullets();
                }

            }

        }
        else
        {
            if (recolback)
            {
                recoilingAmount += Time.deltaTime * recoilSpeed;
                if (recoilingAmount >= recoilTime)
                    recolback = false;
            }
            else if (!recolback)
            {
                recoilingAmount -= Time.deltaTime * recoilSpeed;

                if (recoilingAmount <= 0)
                {
                    recolback = true;
                    recoilingAmount = 0;
                    recol_turret = false;
                }
            }

            Recoil();
        }
    }

    void RotateTo(GameObject obj, Vector3 destination)
    {
        Vector3 direction = destination - obj.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
    }

    void SpawnShell()
    {
        GameObject objShell = Instantiate(shell, startPos.position, Quaternion.identity) as GameObject;
        RotateTo(objShell, endPos.position);
        // TODO SET HERE THE SHELL DAMAGE
        objShell.GetComponent<shell_movement>().SetDestination(endPos.position);
        GetPositions();
        FindObjectOfType<AudioManager>().Play("TankShoot");
        //Recoil();

    }

    void SpawnMegaShell()
    {
        GameObject objShell = Instantiate(megaShell, startPos.position, Quaternion.identity) as GameObject;
        

        RotateTo(objShell, endPos.position);

        // TODO SET HERE THE SHELL DAMAGE
        objShell.GetComponent<MegaShell_movement>().SetDestination(endPos.position);
        objShell.GetComponent<MegaShell_movement>().SetTarget(endPos.gameObject);

        GetPositions();
        //Recoil();
    }


    void SpawnDimensionalShell()
    {
        GameObject objShell = Instantiate(dimensionalShell, startPos.position, Quaternion.identity) as GameObject;
        

        RotateTo(objShell, endPos.position);
        // FindObjectOfType<AudioManager>().Play("TankShoot");

        // TODO SET HERE THE SHELL DAMAGE

        objShell.GetComponent<DimensionalShell_movement>().SetDestination(endPos.position);
        objShell.GetComponent<DimensionalShell_movement>().SetEnemyGameObjectAttached(endPos.GetComponent<Transform>().gameObject);
        objShell.GetComponent<DimensionalShell_movement>().SetFriendlyGameObjectAttached(gameObject.GetComponentInParent<Transform>().gameObject);
        // objShell.GetComponent<DimensionalShell_movement>().SetTarget(endPos.gameObject);

        GetPositions();
    }

    //This Just recoils Back  ---> TODO -> need to recoil forth
    private void Recoil()
    {
        transform.localPosition = Vector3.Lerp(originRecoilPos, endRecoilPos, recoilingAmount);
    }

    private void GetPositions()
    {
        //TODO CHECK A LOOK TO PROPER RECOIL 


        Vector3 dir = startPos.position - endPos.position;
        originRecoilPos = turret.transform.localPosition;
        endRecoilPos = originRecoilPos;
        endRecoilPos.x *= dir.normalized.x * -15;
        endRecoilPos.z *= dir.normalized.z * -15;
    }


    public void SetBullets(int ammo)
    {

        bullets = ammo;
        noAmmo = false;
    }
    public void SwitchState(int i)
    {
        tank_states = (States)i;
    }

    public int GetState()
    {
        return (int)tank_states;
    }
}
