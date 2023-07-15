using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public List<EnemyController> enemies = new List<EnemyController>();
    public int countEnemies;
}
