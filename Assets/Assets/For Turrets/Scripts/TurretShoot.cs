using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    GameObject currentTarget;

    FindDestinationPoint currentTargetCode;

    public GameObject turretCore;
    public GameObject turretGun;
    public TurretData turretData;
    //public AudioSource turretFiringSound;

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
        turretGunStartRotation = turretGun.transform.localRotation;
    }

    bool coolDown = true;

    void CoolDown()
    {
        coolDown = true;
    }

    void ShootTarget()
    {
        if (currentTarget && coolDown)
        {
            currentTargetCode.Hit((int)turretData.damage);
            //turretFiringSound.Play();
            coolDown = false;
            Invoke("CoolDown", turretData.reloadTime);
        }
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
                                                Time.deltaTime * turretData.turnSpeed);

            //turretCore.transform.LookAt(aimAt);
            turretCore.transform.rotation = Quaternion.Slerp(turretCore.transform.rotation,
                                                Quaternion.LookRotation(aimAt - turretCore.transform.position),
                                                Time.deltaTime * turretData.turnSpeed);
            Vector3 directionToTarget = currentTarget.transform.position - turretGun.transform.position;

            if (Vector3.Angle(directionToTarget, turretGun.transform.forward) < turretData.aimingAccuracy)
                if (Random.Range(0, 100) < turretData.accuracy)
                    ShootTarget();
        }
        else
        {
            turretGun.transform.localRotation = Quaternion.Slerp(turretGun.transform.localRotation,
                                                turretGunStartRotation,
                                                Time.deltaTime * turretData.turnSpeed);

            turretCore.transform.rotation = Quaternion.Slerp(turretCore.transform.rotation,
                                                turretCoreStartRotation,
                                                Time.deltaTime * turretData.turnSpeed);
        }
    }
}
