using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shell_movement : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;
    public float lifeTime = 10;
    public GameObject ImpactPrefab;

    public float minH = 3f;
    public float maxH = 15f;
    private float height = 7.97f;
    public float gravity =-18f;
    private string[] names = new string[] { "RedTank", "BlueTank" };
    private Vector3 destination = Vector3.zero;

    public float shellDmg = 10f;
    private Rigidbody rb;
    private GameManager manager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        height = Random.Range(minH, maxH);
        Launch();

        manager = Camera.main.gameObject.GetComponent<GameManager>();
    }

    void Launch()
    {
        Physics.gravity = Vector3.up * gravity;
        rb.useGravity = true;
        Vector3 velocity = CalculateLaunchVelocity();

        if (!float.IsNaN(velocity.x) || !float.IsNaN(velocity.y) || !float.IsNaN(velocity.z))
            rb.velocity = velocity;
    }
    Vector3 CalculateLaunchVelocity()
    {
        float displacementY = destination.y - gameObject.transform.position.y;
        Vector3 displacementXZ = new Vector3(destination.x - gameObject.transform.position.x, 0, destination.z - gameObject.transform.position.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXZ = displacementXZ /( Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity));


        return velocityXZ + velocityY * -Mathf.Sign(gravity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lifeTime -= Time.deltaTime;


        transform.rotation = Quaternion.LookRotation(rb.velocity);

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        speed = 0;
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        pos.y += 0.05f; //TODO TEST
        Debug.Log("Impacted");
        if(ImpactPrefab != null)//Instantiate impact vfx
        {
            GameObject impactVFX = Instantiate(ImpactPrefab, pos, rot) as GameObject;

        }

        for ( int i =0; i < names.Length; i++)
        {
            
            if(names[i] == collision.gameObject.name)
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
        

        

        if(collision.gameObject.tag != "Projectile")
            Destroy(gameObject);

    }

    public void SetDestination(Vector3 dest)
    {
        destination = dest;
    }


    public void SetShellDamage(float dmg)
    {
        shellDmg = dmg;
    }



}
