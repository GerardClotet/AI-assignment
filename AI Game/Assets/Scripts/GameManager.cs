using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject redTank;
    public GameObject blueTank;
    private float redTankLife = 100f;
    private float blueTankLife = 100f;

    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        if( redTankLife <= 0f || blueTankLife <= 0f)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    private void SetHealthSlider(float amount)
    {
        // redTank.byname
    }

    public void DecreaseRedTankLife(float amount)
    {
        redTankLife -= amount;
        redTank.GetComponentInChildren<Slider>().value = redTankLife;

        ////
       GameObject tankhit = redTank.transform.Find("TankHit").gameObject;
        
        if (redTankLife <= 80f && redTankLife > 60f)
            EnableTankImpact(tankhit.transform.Find("Smoke"));
        else if (redTankLife <= 60f && redTankLife > 40f)
            EnableTankImpact(tankhit.transform.Find("Particles Floating"));
        else if (redTankLife <= 40f)
            EnableTankImpact(tankhit.transform.Find("Fire"));

    }
    public void DecreaseBlueTankLife(float amount)
    {
        blueTankLife -= amount;
        blueTank.GetComponentInChildren<Slider>().value = blueTankLife;

        ////
        GameObject tankhit = blueTank.transform.Find("TankHit").gameObject;

        if (blueTankLife <= 80f && blueTankLife > 60f)
            EnableTankImpact(tankhit.transform.Find("Smoke"));
        else if (blueTankLife <= 60f && blueTankLife > 40f)
            EnableTankImpact(tankhit.transform.Find("Particles Floating"));
        else if (blueTankLife <= 40f)
            EnableTankImpact(tankhit.transform.Find("Fire"));


    }
    private void EnableTankImpact(Transform obj)
    {
        obj.gameObject.SetActive(true);


    }
}
