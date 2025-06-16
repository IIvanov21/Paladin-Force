using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStats stats;
    GameObject enemyObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Enemy HP's: "+ stats.health);

        enemyObject=Instantiate(stats.enemyObject, transform.position, Quaternion.identity);
        enemyObject.transform.SetParent(transform);
        transform.name=stats.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
