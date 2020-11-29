using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class face_cam : MonoBehaviour
{
    // Start is called before the first frame update

    private Camera cam;
    void Start()
    {
        cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = cam.transform.position - transform.position;

        v.x = v.z = 0;

        transform.LookAt(cam.transform.position - v);
        transform.rotation = (cam.transform.rotation);
    }


}
