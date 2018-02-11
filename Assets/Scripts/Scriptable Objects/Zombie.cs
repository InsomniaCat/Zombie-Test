using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Zombie")]
public class Zombie : ScriptableObject
{
    new public string name;
    public Sprite sprite;

    public float speed;
    public int hp;
    public int killPoints;

    [Range(1, 100)]
    public int spawnChance;
}
