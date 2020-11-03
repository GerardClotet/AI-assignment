using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    //public GameObject redTank;
    //public GameObject blueTank;
    private float redTankLife = 100f;
    private float blueTankLife = 100f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DecreaseRedTankLife(float amount)
    {
        redTankLife -= amount;
    }
    public void DecreaseBlueTankLife(float amount)
    {
        blueTankLife -= amount;
    }

}
