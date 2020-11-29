using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaShell_movement : shell_parent
{
    public float speed = 20f;

    private bool SoundOnce = false;
    private bool SoundOnce2 = false;

    private float channelingPowerTime = 2.5f;

    private GameObject target;

    private Vector3 shake_originalPos = Vector3.zero;

    enum SHELL_PHASES { GO_UP, STOP_AIR,CHARGE_PROJECTILE,SHOOT_OUT};
    SHELL_PHASES phases = SHELL_PHASES.GO_UP;
    // Start is called before the first frame update
    void Start()
    {
        audioName = "MegaShellStart";
        rb = GetComponent<Rigidbody>();

        height = Random.Range(minH, maxH);
        Launch();

        manager = Camera.main.gameObject.GetComponent<GameManager>();
    }

   
    // Update is called once per frame
    void FixedUpdate()
    {

        switch (phases)
        {
            case SHELL_PHASES.GO_UP://///
                if (rb.velocity != Vector3.zero)
                    transform.rotation = Quaternion.LookRotation(rb.velocity);

                if (rb.velocity.y < 6f && rb.velocity != Vector3.zero)
                     phases = SHELL_PHASES.STOP_AIR;
                
                break;

            case SHELL_PHASES.STOP_AIR:////////
                if (target != null)
                    transform.LookAt(target.transform);

                rb.velocity = Vector3.zero;
                rb.useGravity = false;
                //Instantiate fire
                gameObject.transform.Find("Fire").gameObject.SetActive(true);
                shake_originalPos = transform.position;

                phases = SHELL_PHASES.CHARGE_PROJECTILE;
                break;

            case SHELL_PHASES.CHARGE_PROJECTILE:///////

                if (target != null)
                    transform.LookAt(target.transform);

                if (channelingPowerTime > 0)
                    ShakeObj(shake_originalPos);
                channelingPowerTime -= Time.deltaTime;
                if (SoundOnce == false)
                {
                    FindObjectOfType<AudioManager>().Play("ChargingShell");
                    SoundOnce = true;
                }

                if (channelingPowerTime <= 0)
                    phases = SHELL_PHASES.SHOOT_OUT;
                break;

            case SHELL_PHASES.SHOOT_OUT:///////

                if (target != null)
                    transform.LookAt(target.transform);

                gameObject.transform.Find("ShockWave").gameObject.SetActive(true);
                channelingPowerTime -= Time.deltaTime;
                if (SoundOnce2 == false)
                {
                    FindObjectOfType<AudioManager>().Play("MegaShellShoot");
                    SoundOnce2 = true;
                }
                if (target != null)
                {
                    Vector3 dir = target.transform.position - transform.position;
                    gameObject.transform.position += dir.normalized * (speed * Time.deltaTime);
                }
                break;
        }
        
      

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
            Destroy(gameObject);
    }

   

    public void SetTarget(GameObject tank)
    {
        target = tank;
    }


    private void ShakeObj(Vector3 originalpos)
    {
        float speed = 50.0f;
        float amount = 0.05f;

        Vector3 newpos;
        newpos.x = originalpos.x + Mathf.Sin(Time.time * speed) * amount;
        newpos.y = originalpos.y + Mathf.Sin(Time.time * speed) * amount;
        newpos.z = originalpos.z + Mathf.Sin(Time.time * speed) * amount;

        gameObject.transform.position = newpos;
    }
}
