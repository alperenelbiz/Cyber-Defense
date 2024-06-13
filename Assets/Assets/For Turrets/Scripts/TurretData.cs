using UnityEngine;

[CreateAssetMenu(fileName = "TurretData", menuName = "ScriptableObjects/TurretDetails", order = 2)]
public class TurretData : ScriptableObject
{
    public float damage;
    public float accuracy;
    public float turnSpeed;
    public float reloadTime;
}
