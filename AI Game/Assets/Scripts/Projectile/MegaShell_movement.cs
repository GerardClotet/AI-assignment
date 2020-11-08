using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaShell_movement : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 15;
    public GameObject ImpactPrefab;
    private Vector3 destination = Vector3.zero;
    private readonly string[] names = new string[] { "RedTank", "BlueTank" };
    public float shellDmg = 25f;
    private Rigidbody rb;
    private GameManager manager;



    public float minH = 3f;
    public float maxH = 15f;
    private float height = 7.97f;
    public float gravity = -18f;

    private float channelingPowerTime = 2.5f;

    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        height = Random.Range(minH, maxH);
        Launch();

        manager = Camera.main.gameObject.GetComponent<GameManager>();
    }

   
    // Update is called once per frame
    void FixedUpdate()
    {
        lifeTime -= Time.deltaTime;


        if (rb.velocity != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(rb.velocity);

        else
        {
            if(target !=null)
                transform.rotation = Quaternion.LookRotation(target.transform.position);
        }


        if(rb.velocity.y < 6f && rb.velocity != Vector3.zero)
        {
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            //Instantiate fire
        }

        else if(rb.velocity == Vector3.zero)
        {
            channelingPowerTime -= Time.deltaTime;

        }
        if (channelingPowerTime <= 0)
        {
            gameObject.transform.position += transform.forward * (speed*10 * Time.deltaTime);
        }

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
            Destroy(gameObject);
    }

    private void Launch()
    {
        Physics.gravity = Vector3.up * gravity;
        rb.useGravity = true;
        Vector3 velocity = CalculateLaunchVelocity();

        if (!float.IsNaN(velocity.x) || !float.IsNaN(velocity.y) || !float.IsNaN(velocity.z))
            rb.velocity = velocity;

        else Destroy(gameObject);

    }

    Vector3 CalculateLaunchVelocity()
    {
        float displacementY = destination.y - gameObject.transform.position.y;
        Vector3 displacementXZ = new Vector3(destination.x - gameObject.transform.position.x, 0, destination.z - gameObject.transform.position.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity));


        return velocityXZ + velocityY * -Mathf.Sign(gravity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        speed = 0;
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        pos.y += 0.05f; //TODO TEST
        Debug.Log("Impacted");
        if (ImpactPrefab != null)//Instantiate impact vfx
        {
            GameObject impactVFX = Instantiate(ImpactPrefab, pos, rot) as GameObject;

        }

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

    public void SetTarget(GameObject tank)
    {
        target = tank;
    }
    public void SetDestination(Vector3 dest)
    {
        destination = dest;
    }
}
