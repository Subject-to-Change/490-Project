using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSpawner : MonoBehaviour
{
    public GameObject log;
    public float minSpawnTime = 3f;
    public float maxSpawnTime = 6f;

    private void Start()
    {
        StartCoroutine(SpawnLogs());
    }

    private IEnumerator SpawnLogs()
    {
        while (true)
        {
            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnTime);

            Instantiate(log, transform.position, Quaternion.identity);
        }
    }
}