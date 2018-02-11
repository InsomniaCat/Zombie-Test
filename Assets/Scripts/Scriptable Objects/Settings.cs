using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings")]
public class Settings : ScriptableObject
{
    public bool isSaving;
    [Tooltip("Радус спавна. Больше - дальше")]
    public float zombieSpawnRadius;
    [Range(1,180)]
    public float zombieSpawnAngle;

    [Tooltip("Сколько зомби спавнится за раз")]
    public int zombieSpawnCount;
    [Tooltip("Начальная задержка перед спавном")]
    public float zombieSpawnTime;
    [Tooltip("Раз в сколько секунд учащается спавн зомби")]
    public float zombieSpawnReduceTime;
    [Tooltip("На сколько секунд учащается спавн")]
    public float zombieSpawnRecudeSec;
    [Tooltip("Минимальное время спавна")]
    public float zombieSpawnMinimumDelay;

    public float bulletSpeed;
    public Color bulletColor;
    public float bulletsPerSecond;
    public int bulletDamage;
}
