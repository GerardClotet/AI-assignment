using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimEnemy : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject enemy;
    public GameObject ownturret;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 aim = enemy.transform.position - ownturret.transform.position;
        aim.y = 0;
        transform.rotation = Quaternion.LookRotation(aim);
    }
}
