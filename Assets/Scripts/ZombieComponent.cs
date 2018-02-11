using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieComponent : MonoBehaviour
{
    [HideInInspector]
    public Zombie zombie;

    public int HP
    {
        get { return zombie.hp; }
        set
        {
            zombie.hp = value;

            if (HP <= 0)
                Death();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "bullet")
        {
            HP -= Game.Instance.settings.bulletDamage;
            Destroy(collision.gameObject);
        }

        if (collision.transform.tag == "soldier")
        {
            Game.Instance.DeathScreen();
        }
    }

    private void Death()
    {
        Game.Instance.gamePoints += zombie.killPoints;
        Destroy(gameObject);
    }
}
