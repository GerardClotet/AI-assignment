using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update

    private Camera cam;
    public GameObject tank1;
    public GameObject tank2;
    //private RaycastHit rayhit;
    public LayerMask layer;
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        GetMidPoint();
    }
    private void GetMidPoint()
    {
        //Vector3 sum = Vector3.zero;
        //sum += tank1.transform.position;
        //sum += tank2.transform.position;

        Vector3 temp = Vector3.zero;
        temp += tank1.transform.position;
        temp += tank2.transform.position;
        temp /= 2;

        temp.y = cam.transform.position.y;
        cam.transform.position = temp;

        //get x rotation  //to move on z axis to see the tanks properly
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(tank1.transform.position, tank2.transform.position);
        Vector3 sum = Vector3.zero;
        sum += tank1.transform.position;
        sum += tank2.transform.position;
        if (cam != null)
        {
            Gizmos.DrawLine(sum / 2, cam.transform.position);

            Vector3 temp_test = Vector3.zero;
            temp_test += sum / 2;
            temp_test += cam.transform.position;
            temp_test /= 2;

            // Diagonal
            Vector3 tst = cam.transform.rotation * temp_test;

            // Mathf.Cos(cam.transform.rotation.x) * cam.transform.position.y;
            Vector3 dir = tst - cam.transform.position;
            float h_dist = Mathf.Cos(cam.transform.rotation.eulerAngles.x * Mathf.Deg2Rad);

            //  Physics.Raycast(cam.transform.position, tst, out rayhit,layer,1000);



            Vector3 norm_dir = tst.normalized;
            Vector3 nouvec = Vector3.Scale(norm_dir, tst);

            Gizmos.DrawLine(cam.transform.position, tst); //GOOD DIR
                                                          //Gizmos.DrawLine(cam.transform.position,nouvec);
                                                          //float what = Mathf.Cos(cam.transform.rotation.eulerAngles.x) * cam.transform.position.y;
        }
    }
}
