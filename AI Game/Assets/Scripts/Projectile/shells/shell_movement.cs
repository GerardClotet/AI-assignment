using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shell_movement : shell_parent
{
    public float speed = 20f;

    void Start()
    {

        audioName = "TankImpact";
        if (lifeTime == 0)
            lifeTime = 10;
        rb = GetComponent<Rigidbody>();

        height = Random.Range(minH, maxH);
        Launch();

        manager = Camera.main.gameObject.GetComponent<GameManager>();
    }

  

    // Update is called once per frame
    void FixedUpdate()
    {


        transform.rotation = Quaternion.LookRotation(rb.velocity);

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
            Destroy(gameObject);
    }

}
