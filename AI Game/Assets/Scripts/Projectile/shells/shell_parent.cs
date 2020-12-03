using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shell_parent : MonoBehaviour
{



    [SerializeField]
    protected float gravity = -18f,shellDmg=10f, lifeTime=0,minH=3f,maxH=15;

    protected float height = 7f;

    [SerializeField]
    protected Vector3 destination = Vector3.zero;

    [SerializeField]
    protected GameObject ImpactPrefab;

    protected Rigidbody rb;
    protected GameManager manager;

    protected string[] names = new string[] { "RedTank", "BlueTank" };
    protected string audioName;



    protected void Launch()
    {
        Physics.gravity = Vector3.up * gravity;
        rb.useGravity = true;
        Vector3 velocity = CalculateLaunchVelocity();

        if (!float.IsNaN(velocity.x) || !float.IsNaN(velocity.y) || !float.IsNaN(velocity.z))
            rb.velocity = velocity;
    }

    protected Vector3 CalculateLaunchVelocity()
    {
        float displacementY = destination.y - gameObject.transform.position.y;
        Vector3 displacementXZ = new Vector3(destination.x - gameObject.transform.position.x, 0, destination.z - gameObject.transform.position.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity));


        return velocityXZ + velocityY * -Mathf.Sign(gravity);
    }


    public void SetDestination(Vector3 dest)
    {
        destination = dest;
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        pos.y += 0.05f; //TODO TEST
        FindObjectOfType<AudioManager>().Play(audioName);
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
}
