using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;
    [SerializeField]
    float spawnDistance;
    [SerializeField]
    float timeBetweenSpawn;
    [SerializeField]
    float timeReduce;
    [SerializeField]
    int enemiesBySpawn;

    static float timerInit;

    static bool isNight;

    static float timer;
    static int enemies;

    System.Random rand;

    void Start()
    {
        rand = new System.Random();
        timer = timeBetweenSpawn;
        enemies = enemiesBySpawn;
        timerInit = timeBetweenSpawn;
        StartCoroutine(SpawnTimer());
    }

    public static void newDay()
    {
        timer = timerInit;
        enemies++;
        isNight = false;
    }

    public static void Night()
    {
        isNight = true;
    }

    IEnumerator SpawnTimer()
    {
        while (true)
        {
            if (isNight)
            {
                while (isNight)
                {
                    yield return new WaitForSeconds(timer);
                    Spawn();
                    if (timer > 1)
                    {
                        timer -= timeReduce;
                    }
                    else
                    {
                        timer = 1;
                    }
                }
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    void Spawn()
    {
        for (int i = 0; i < enemies; i++)
        {
            transform.eulerAngles = new Vector3(0, rand.Next(0, 360), 0);
            GameObject newEnemy = Instantiate(enemy, transform.forward * spawnDistance, Quaternion.identity) as GameObject;
        }
    }
}