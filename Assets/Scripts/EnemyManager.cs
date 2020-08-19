using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public SlimeGreenController slimePrefab;
    public List<GameObject> enemy = new List<GameObject>();
    public List<Transform> enemyLocations = new List<Transform>();

    private void Awake()
    {
        instance = this;
    }

    public void SpawnEnemy()
    {
        if (enemy.Count > 0)
        {
            foreach(GameObject thisEnemy in enemy)
            {
                Destroy(thisEnemy);
            }
            enemy.Clear();
        }
        for (int i=0; i<enemyLocations.Count; i++)
        {
            SlimeGreenController thisSlime = Instantiate(slimePrefab);
            thisSlime.transform.position = enemyLocations[i].transform.position;
            enemy.Add(thisSlime.gameObject);
        }
    }
}
