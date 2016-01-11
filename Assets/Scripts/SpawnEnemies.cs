using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

    public GameObject[] enemies;
    public int _numOfEnemies;
    public float spawnTime;

    public enum SpawnPattern
    {
        Random,
        Cluster,
        RoundRamp
    }
    public SpawnPattern spawnPattern;

	public void StartSpawn(int numOfEnemies)
    {
        _numOfEnemies = numOfEnemies;
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        for (int i = 0; i < _numOfEnemies; i++)
        {
            int index = Random.Range(0, enemies.Length);
            Instantiate(enemies[index], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
