using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shell_portal : MonoBehaviour
{
    // Start is called before the first frame update

    public ParticleSystem mantain;
    public ParticleSystem decrease;

    private float timer = 3f;
    private float secondtimer = 3f;
    public bool collided { get; private set; }

    void Start()
    {
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
            Destroy(gameObject);


    }
    public void Init()
    {
        gameObject.GetComponent<ParticleSystem>().Play();
        mantain.Play();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            //mantain.Stop();
            //decrease.Play();

            //TODO launch vfx when projectile collides, vfx de entrar al portal
            secondtimer = 0.6f;
            timer = 0.3f;
            collided = true;
        }
    }

}
