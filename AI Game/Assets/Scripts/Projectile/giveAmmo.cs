using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giveAmmo : MonoBehaviour
{
    // Start is called before the first frame update
    private SpawnProjectile spawnProjectile;
    //void Start()
    //{
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<AudioManager>().Play("Recharge");
        spawnProjectile = other.gameObject.GetComponentInChildren<SpawnProjectile>();
        if (spawnProjectile != null)
            spawnProjectile.SetBullets(5);

        spawnProjectile = null;
    }
    public void OnCollisionEnter(Collision collision)
    {

        
       spawnProjectile = collision.gameObject.GetComponentInChildren<SpawnProjectile>();
        if(spawnProjectile!=null)
            spawnProjectile.SetBullets(5);

        spawnProjectile = null;
    }
}
