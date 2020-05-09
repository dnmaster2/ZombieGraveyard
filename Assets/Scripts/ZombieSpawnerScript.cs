using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnerScript : MonoBehaviour
{
    public GameObject zombie;
    public Transform hordeParent;
    public List<Transform> zombieSpawns;
    bool roundRunning;
    public static int rounds;

    void Start()
    {
        foreach(GameObject temp in GameObject.FindGameObjectsWithTag("Spawn"))
        {
            zombieSpawns.Add(temp.transform);
        }
        StartCoroutine(SpawnCounter(1));
        rounds++;
    }

    private void Update()
    {
        if (roundRunning)
        {
            int remainingZombies = hordeParent.childCount;
            if(remainingZombies == 0)
            {
                roundRunning = false;
                StartCoroutine(SpawnCounter(1 + rounds));
            }
        }
    }

    IEnumerator SpawnCounter(int zombiesToSpawn)
    {
        yield return new WaitForSeconds(3f);
        rounds++;
        for(int i = 0; i < zombiesToSpawn; i++)
        {
            Vector3 spawnPoint = zombieSpawns[Random.Range(0, zombieSpawns.Count)].position;
            spawnPoint.y += 2f;
            Instantiate(zombie, spawnPoint, Quaternion.identity, hordeParent);
        }
        roundRunning = true;
    }
}
