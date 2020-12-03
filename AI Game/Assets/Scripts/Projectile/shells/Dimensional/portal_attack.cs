using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal_attack : MonoBehaviour
{
    public ParticleSystem mantain;
    public ParticleSystem decrease;

    public GameObject dimensionalShell; //Hi ha algun error amb la dimensional shell

    private Vector3 destination;
    // Start is called before the first frame update
    private bool launched = false;
    void Start()
    {
        //StartCoroutine(SpawnShell());
       // SpawnShell();
        Invoke("SpawnShell", 0.3f); //Invokes function when portal has completed open action (visual)
    //decrease.Pause();
        gameObject.GetComponent<ParticleSystem>().Play();
        StartCoroutine(ClosePortal());
    }
    //IEnumerator SpawnShell()
    //{
    //    yield return new WaitForSeconds(1f);

    //    GameObject objshell = Instantiate(dimensionalShell, transform.position, Quaternion.identity) as GameObject;
    //    Vector3 direction = destination - objshell.transform.position;
    //    Quaternion rotation = Quaternion.LookRotation(direction);
    //    objshell.transform.localRotation = Quaternion.Lerp(objshell.transform.rotation, rotation, 1);
    //    launched = true;
    //    DimensionalShell_attack_movement attck_shell = objshell.GetComponent<DimensionalShell_attack_movement>();
    //    if (attck_shell != null) //maybe null
    //        attck_shell.SetDestination(destination);

    //}
    IEnumerator ClosePortal()
    {

        yield return new WaitForSeconds(1.5f);

        //while (launched)
        //    yield return null;
        if (launched)
        {
            decrease.Clear();
            decrease.Play();
        }
        yield return null;

        while(decrease.isPlaying)
            yield return null;


        Destroy(gameObject);
        Debug.Log("End attack portal coroutine");
        //end corroutine
    }
    // Update is called once per frame
    //void Update()
    //{

    //    if (!gameObject.GetComponent<ParticleSystem>().IsAlive())
    //    {

    //    }

    //}

    private void SpawnShell() //FIX!!!  Invoke never gets called
    {
        GameObject objshell = Instantiate(dimensionalShell, transform.position, Quaternion.identity) as GameObject;
        Vector3 direction = destination - objshell.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        objshell.transform.localRotation = Quaternion.Lerp(objshell.transform.rotation, rotation, 1);
        launched = true;
        DimensionalShell_attack_movement attck_shell = objshell.GetComponent<DimensionalShell_attack_movement>();
        if (attck_shell != null) //maybe null
            attck_shell.SetDestination(destination);
    }

    public void SetDestination(Vector3 transf)
    {
        destination = transf;
    }
}
