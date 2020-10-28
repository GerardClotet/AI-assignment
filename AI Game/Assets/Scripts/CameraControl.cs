using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update

    private Camera cam;
    public GameObject tank1;
    public GameObject tank2;
    void Start()
    {
        cam = Camera.main; 
    }

    // Update is called once per frame
    void Update()
    {
        GetMidPoint();
    }
    private void GetMidPoint()
    {
        Vector3 temp = (tank1.transform.position - tank2.transform.position)/2;

        temp.y = cam.transform.position.y;
        cam.transform.position = temp;
    }
}
