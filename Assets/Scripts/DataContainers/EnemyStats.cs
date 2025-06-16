using UnityEngine;

[CreateAssetMenu(fileName ="New EnemyStats", menuName ="Enemies/Stats")]
public class EnemyStats : ScriptableObject
{
    public int health;
    public float speed;
    public int damage;
    public string name;

    public GameObject enemyObject;
}
