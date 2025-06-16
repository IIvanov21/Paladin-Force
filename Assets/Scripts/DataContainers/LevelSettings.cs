using UnityEngine;

[CreateAssetMenu(fileName ="Level Settings", menuName ="Level/Settings")]
public class LevelSettings: ScriptableObject
{
    public float enemySpawnRate;
    public float initialDelay;

}
