using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieSpawner : MonoBehaviour
{
    public Zombie[] zombies;

    [HideInInspector]
    public Settings settings;

    public GameObject zombiePrefab;

    float timeToDelay;
    float timeToSpawn;
    float delay;

    Vector2 centerOfSpawn;

    public void StartSpawn(Vector2 center)
    {
        centerOfSpawn = center;
        delay = settings.zombieSpawnTime;
        timeToSpawn = 0;
        timeToDelay = 0;
    }

    public void KillAll()
    {
        var zombiesToKill = GameObject.FindGameObjectsWithTag("zombie");

        foreach (var z in zombiesToKill)
        {
            Destroy(z);
        }
    }

    #region Zombie Spawn
    void Spawn(Zombie zombie)
    {
        float angle = Random.Range(0, settings.zombieSpawnAngle);
        angle -= settings.zombieSpawnAngle / 2;

        //Нужен клон объекта, иначе будет менять значения напрямую.
        var newZombie = Instantiate(zombie);

        Vector2 pos;
        pos.x = centerOfSpawn.x + settings.zombieSpawnRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.y = centerOfSpawn.y + settings.zombieSpawnRadius * Mathf.Cos(angle * Mathf.Deg2Rad);

        GameObject z = Instantiate(zombiePrefab);
        z.transform.SetParent(Game.Instance.grass, false);
        z.transform.position = pos;
        z.transform.up = (centerOfSpawn - (Vector2)z.transform.position).normalized;

        z.GetComponent<ZombieComponent>().zombie = newZombie;
        z.GetComponent<MoveForward>().speed = newZombie.speed;
        z.GetComponentInChildren<Image>().sprite = newZombie.sprite;
    }

    void GetZombie()
    {
        for (int i = 0; i < settings.zombieSpawnCount; i++)
        {
            RandomZombie();
        }
    }

    void RandomZombie()
    {
        int R = Random.Range(1, 101);
        int temp = zombies[0].spawnChance;

        //Будет багаться если неправильно указать шансы в настройках
        for (int i = 0; i < zombies.Length; i++)
        {
            if (i > 0)
                temp += zombies[i].spawnChance;

            if (R <= temp)           
            {
                Spawn(zombies[i]);
                break;
            }
        }
    }

    public IEnumerator TimeCoroutine()
    {
        while (Game.Instance.isPlaying)
        {
            timeToSpawn += 0.1f;
            timeToDelay += 0.1f;

            if (timeToSpawn >= delay)
            {
                timeToSpawn = 0;

                GetZombie();
            }

            if (timeToDelay >= settings.zombieSpawnReduceTime)
            {
                delay -= settings.zombieSpawnRecudeSec;
                timeToDelay = 0;

                if (delay < settings.zombieSpawnMinimumDelay)
                    delay = settings.zombieSpawnMinimumDelay;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    #endregion
}
