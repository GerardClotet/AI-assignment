using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shell_movement : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;
    public float lifeTime = 10;
    public GameObject ImpactPrefab;

    private string[] names = new string[] { "RedTank", "BlueTank" };
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lifeTime -= Time.deltaTime;
        if(speed !=0)
        {
            gameObject.transform.position += transform.forward * (speed * Time.deltaTime);
        }

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
       // speed = 0;
        Debug.Log("Impacted");
        if(ImpactPrefab != null)//Instantiate impact vfx
        {
            
        }

        for( int i =0; i < names.Length; i++)
        {
            if(names[i] == collision.gameObject.name)
            {
                Debug.Log("Tank Impact");
            }
        }
        
        if(collision.gameObject.tag != "Projectile")
            Destroy(gameObject);

    }
}
