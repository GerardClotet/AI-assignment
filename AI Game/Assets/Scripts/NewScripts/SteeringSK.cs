using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringSK : MonoBehaviour
{
    // Start is called before the first frame update

     //Seek
    GameObject target;
    Vector3 movement;
    float acceleration;
    Quaternion rotation;


    //frequency
    float freq = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        freq += Time.deltaTime;
        if (freq > 0.5)
        {
            freq -= 0.5f;
            Seek();
        }
    }

    void Seek()
    {
        Vector3 direction = target.transform.position - transform.position;
        direction.y = 0f;
        movement = direction.normalized * acceleration;
        float angle = Mathf.Rad2Deg * Mathf.Atan2(movement.x, movement.z);
        rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }

    void Wander()
    {
        float radius = 2f;
        float offset = 3f;
        Vector3 localTarget = new Vector3(
            Random.Range(-1.0f, 1.0f), 0,
            Random.Range(-1.0f, 1.0f));
        localTarget.Normalize();
        localTarget *= radius;
        localTarget += new Vector3(0, 0, offset);
        Vector3 worldTarget =
            transform.TransformPoint(localTarget);
        worldTarget.y = 0f;
       // Seek(worldTarget);
    }
}
