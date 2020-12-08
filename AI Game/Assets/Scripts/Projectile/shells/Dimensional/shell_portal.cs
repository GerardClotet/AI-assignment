using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shell_portal : MonoBehaviour
{

    public Material red_mat;
    public Material blue_mat;
    private ParticleSystemRenderer psr;

    public ParticleSystem mantain;
    public ParticleSystem decrease;

    private float timer = 3f;
    private float secondtimer = 3f;
    public bool collided { get; private set; }
    private GameObject go_attached;
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("portal");
        collided = false;
        //decrease.Pause();
        gameObject.GetComponent<ParticleSystem>().Play(false);
       
    }

    void Update()
    {

        timer -= Time.deltaTime;
        secondtimer -= Time.deltaTime;
        if (secondtimer <= 0.4 && secondtimer > -1)
        {
            mantain.Clear();
            decrease.gameObject.SetActive(true);
            decrease.Clear();
            decrease.Play();

            secondtimer = -1f;
        }
            
        if (timer <= 0f)
        {
            if (!collided)
                go_attached.GetComponent<SpawnProjectile>().ResetPortalExecution();
            Destroy(gameObject);

        }


    }
    public void GameObjectAttached(GameObject go)
    {
        go_attached = go;
    }
    public void Init()
    {
        if (go_attached.name == "RedTank")
            ChangeMaterial(blue_mat);
        else
            ChangeMaterial(red_mat);

        gameObject.GetComponent<ParticleSystem>().Play();
        mantain.Play();
        

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile" && collision.gameObject.GetComponent<DimensionalShell_movement>().GetFriendlyGameObjectAttached() == go_attached)
        {
            //mantain.Stop();
            //decrease.Play();

            //TODO launch vfx when projectile collides, vfx de entrar al portal
            secondtimer = 0.6f;
            timer = 0.3f;
            collided = true;
        }
    }



    void ChangeMaterial(Material mat)
    {
        psr = gameObject.GetComponent<ParticleSystemRenderer>();
        psr.material = mat;


        //sons

        ParticleSystemRenderer[] sons_psr = GetComponentsInChildren<ParticleSystemRenderer>();
        foreach (ParticleSystemRenderer it in sons_psr)
        {
            it.material = mat;
        }
       
    }


}
