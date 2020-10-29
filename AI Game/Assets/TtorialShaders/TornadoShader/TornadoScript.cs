using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoScript : MonoBehaviour
{
    // Start is called before the first frame update

    //public iTween.EaseType movementEaseType = iTween.EaseType.easeInOutSine;
    public float movementRadius = 30;
    public float movementSpeed = 0.5f;

    private Vector3 originalPosition;
    public float pullForce;
    public AnimationCurve pullForceCurve;

    public Transform pullingCenter;
    public AnimationCurve pullingCenterCurve;
    private float refreshRate = 1;
    void Start()
    {
        originalPosition = transform.position;
        StartCoroutine(Movement());
    }

    //void OnTriggerEnter(Collider co)
    //{
    //    if (co.tag == "Tornado_obj")
    //        StartCoroutine(IncreasePull(co, true));
    //}
    //void OnTriggerExit(Collider co)
    //{
    //    if (co.tag == "Bullet")
    //        StartCoroutine(IncreasePull(co, false));
    //}
    // Update is called once per frame
    //void Update()
    //{

    //}

    IEnumerator Movement()
    {
        Vector3 newPos = new Vector3(originalPosition.x + Random.Range(-movementRadius, movementRadius), originalPosition.y, originalPosition.z + Random.Range(-movementRadius, movementRadius));
        Vector3 distance = newPos - originalPosition;
        float time = distance.magnitude / movementSpeed;

      //  iTween.MoveTo(gameObject, iTween.Hash("position", newPos, "easeType", movementEaseType, "time", time));
        yield return new WaitForSeconds(time * 0.1f);
        StartCoroutine(Movement());
    }

    IEnumerator IncreasePull(Collider co, bool pull)
    {
        if (pull)
        {
            pullForce = pullForceCurve.Evaluate(((Time.time * 0.1f) % pullForceCurve.length));

            Vector3 forceDirection = pullingCenter.position - co.transform.position;

            co.GetComponent<Rigidbody>().AddForce(forceDirection.normalized * pullForce * Time.deltaTime *-1);
            pullingCenter.position = new Vector3(pullingCenter.position.x, pullingCenterCurve.Evaluate(((Time.time * 0.1f) % pullingCenterCurve.length)), pullingCenter.position.z);
            yield return refreshRate;

            StartCoroutine(IncreasePull(co, pull));
        }
        else yield return refreshRate;
    }
}
