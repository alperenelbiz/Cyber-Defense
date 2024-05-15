using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    GameObject currentTarget;

    FindDestinationPoint currentTargetCode;

    public GameObject turretCore;
    public GameObject turretGun;

    Quaternion turretCoreStartRotation;
    Quaternion turretGunStartRotation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("goob") && currentTarget == null)
        {
            currentTarget = other.gameObject;
            currentTargetCode = currentTarget.GetComponent<FindDestinationPoint>();
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
        turretCoreStartRotation = turretCore.transform.rotation;
        turretGunStartRotation = turretGun.transform.rotation;
    }

    void ShootTarget()
    {
        if (currentTarget)
            currentTargetCode.Hit(1);
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
            relativeTargetPosition = new Vector3(relativeTargetPosition.x,
                                                    currentTarget.transform.position.y,
                                                    relativeTargetPosition.z);

            turretGun.transform.rotation = Quaternion.Slerp(turretGun.transform.rotation,
                                                Quaternion.LookRotation(relativeTargetPosition - turretGun.transform.position),
                                                Time.deltaTime);

            //turretCore.transform.LookAt(aimAt);
            turretCore.transform.rotation = Quaternion.Slerp(turretCore.transform.rotation,
                                                Quaternion.LookRotation(aimAt - turretCore.transform.position),
                                                Time.deltaTime);

            ShootTarget();
        }
        else
        {
            turretGun.transform.rotation = Quaternion.Slerp(turretGun.transform.rotation,
                                                turretGunStartRotation,
                                                Time.deltaTime);

            turretCore.transform.rotation = Quaternion.Slerp(turretCore.transform.rotation,
                                                turretCoreStartRotation,
                                                Time.deltaTime);
        }
    }
}
