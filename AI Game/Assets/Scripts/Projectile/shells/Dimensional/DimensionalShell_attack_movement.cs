using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionalShell_attack_movement : shell_parent
{
    public float speed = 250f;
    // Start is called before the first frame update
    void Start()
    {
        audioName = "TankImpact";
        rb = GetComponent<Rigidbody>();

        Invoke("DestroyItself", 10f); //In case it doens collide with anything
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(destination);
        Vector3 dir = destination - transform.position;
        transform.position += dir.normalized * (speed * Time.deltaTime);
    }

    private void DestroyItself()
    {
        Destroy(gameObject);
    }
}
