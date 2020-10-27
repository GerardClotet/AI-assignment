using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shell_movement : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;
    public float lifeTime = 10;
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
}
