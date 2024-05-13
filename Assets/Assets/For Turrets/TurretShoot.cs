using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    public GameObject turretCore;
    public GameObject turretGun;

    GameObject currentTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("goob") && currentTarget == null)
        {
            currentTarget = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentTarget)
            currentTarget = null;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget != null)
        {
            Vector3 aimAt = new Vector3(currentTarget.transform.position.x,
                                            turretCore.transform.position.y,
                                            currentTarget.transform.position.z);

            //turretGun.transform.LookAt(currentTarget.transform.position);
            float distanceToTarget = Vector3.Distance(aimAt, turretGun.transform.position);
            Vector3 relativeTargetPosition = turretGun.transform.position +
                                                (turretGun.transform.forward * distanceToTarget);

            //turretCore.transform.LookAt(aimAt);
            turretCore.transform.rotation = Quaternion.Slerp(turretCore.transform.rotation,
                                                Quaternion.LookRotation(aimAt - turretCore.transform.position),
                                                Time.deltaTime);
        }
    }
}
