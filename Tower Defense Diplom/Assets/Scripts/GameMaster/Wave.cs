using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    [Tooltip("Враги входящие в волну")] public List<EnemyController> enemies = new List<EnemyController>();
    [Tooltip("Количество врагов в волне")] public int countEnemies;
}
