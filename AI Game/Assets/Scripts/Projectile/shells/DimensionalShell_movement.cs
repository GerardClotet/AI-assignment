using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionalShell_movement : shell_parent
{
    // Start is called before the first frame update

    private GameObject go_attached;
    private DimensionalShellsManager portal_manager;

    public float speed = 20f;
    private Collider ignore_launchcoll;
    void Start()
    {

        audioName = "TankImpact";
        if (lifeTime == 0)
            lifeTime = 10;

        rb = GetComponent<Rigidbody>();


        height = Random.Range(minH, maxH);
        Launch();
        manager = Camera.main.gameObject.GetComponent<GameManager>();
        if (Camera.main.gameObject.GetComponent<DimensionalShellsManager>() == null)
            Debug.Log("portal manager null");

        portal_manager = Camera.main.gameObject.GetComponent<DimensionalShellsManager>();
        portal_manager.AddtoList(gameObject);
        portal_manager.CreateFirstPortal(gameObject);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(rb.velocity);

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
            Destroy(gameObject);
    }


    
    //private void OnDestroy()
    //{
        
    //}
    public void SetGameObjectAttached(GameObject go)
    {
        go_attached = go;
    }
    public GameObject GetGameObjectAttached()
    {
        return go_attached;
    }
    public override void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        pos.y += 0.05f; //TODO TEST
        FindObjectOfType<AudioManager>().Play(audioName);


        if (collision.gameObject.tag == "Portal")
        {

            portal_manager.PortalCollider(gameObject);

        }

        //Only when not touching portals


        else
        {
            //if (ImpactPrefab != null)//Instantiate impact vfx
            //{
            //    GameObject impactVFX = Instantiate(ImpactPrefab, pos, rot) as GameObject;


            //}
            for (int i = 0; i < names.Length; i++)
            {

                if (names[i] == collision.gameObject.name)
                {
                    if (manager != null)
                    {
                        if (i == 0)
                            manager.DecreaseBlueTankLife(shellDmg);
                        else
                            manager.DecreaseRedTankLife(shellDmg);

                    }


                }
            }

            if (collision.gameObject.tag != "Projectile")
                Destroy(gameObject);
        }
       
    }
}
