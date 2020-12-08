using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giveAmmo : MonoBehaviour
{
    // Start is called before the first frame update
    private SpawnProjectile spawnProjectile;

    private void OnTriggerEnter(Collider other)
    {
        spawnProjectile = other.gameObject.GetComponentInChildren<SpawnProjectile>();
        
        if (spawnProjectile != null)
            spawnProjectile.SetBullets(5);

        spawnProjectile.SetAmmoIconFalse();
        spawnProjectile = null;


        
    }
    public void OnCollisionEnter(Collision collision)
    {

        
       spawnProjectile = collision.gameObject.GetComponentInChildren<SpawnProjectile>();
        if(spawnProjectile!=null)
            spawnProjectile.SetBullets(5);

        spawnProjectile.SetAmmoIconFalse();

        spawnProjectile = null;
    }
}
