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

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        height = Random.Range(minH, maxH);
        Launch();

    }

    void Launch()
    {
        Physics.gravity = Vector3.up * gravity;
        rb.useGravity = true;
        print(CalculateLaunchVelocity());
        rb.velocity = CalculateLaunchVelocity();
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
        //if(speed !=0)
        //{
        //    gameObject.transform.position += transform.forward * (speed * Time.deltaTime);
        //}

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
        Debug.Log("Impacted");
        if(ImpactPrefab != null)//Instantiate impact vfx
        {
            GameObject impactVFX = Instantiate(ImpactPrefab, pos, rot) as GameObject;

        }

        for ( int i =0; i < names.Length; i++)
        {
            if(names[i] == collision.gameObject.name)
            {
                Debug.Log("Tank Impact");
            }
        }
        
        if(collision.gameObject.tag != "Projectile")
            Destroy(gameObject);

    }

    public void SetDestination(Vector3 dest)
    {
        destination = dest;
    }
}
