using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Deleter : MonoBehaviour
{
    // Start is called before the first frame update

    public float lifeTime = 10f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
            Destroy(gameObject);
    }
}
