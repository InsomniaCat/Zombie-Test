using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soldier : MonoBehaviour
{
    [HideInInspector]
    public Settings settings;

    public GameObject bulletPrefab;
    public Vector3 BulletOffset;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
            transform.up = direction;
        }
    }

    void Fire()
    {
        Vector3 offset = transform.rotation * BulletOffset;

        var bullet = Instantiate(bulletPrefab);

        bullet.transform.SetParent(Game.Instance.grass, false);
        bullet.transform.position = transform.position + offset;
        bullet.transform.rotation = transform.rotation;

        bullet.GetComponent<MoveForward>().speed = settings.bulletSpeed;
        bullet.GetComponent<Image>().color = settings.bulletColor;
        Destroy(bullet, 2.0f);
    }

    public IEnumerator Shot()
    {
        while (Game.Instance.isPlaying)
        {
            Fire();
            yield return new WaitForSeconds(1.0f/settings.bulletsPerSecond);
        }
    }
}
