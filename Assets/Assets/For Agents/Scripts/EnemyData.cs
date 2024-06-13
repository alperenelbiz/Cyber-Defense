using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyDetails", order = 1)]
public class EnemyData : ScriptableObject
{
    public string cName;
    public float speed;
    public int maxHealth;
}
