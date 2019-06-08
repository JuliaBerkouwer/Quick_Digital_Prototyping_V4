using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunToPlayer : MonoBehaviour
{
    public float enemyFireRate;
    public GameObject enemyBulletGO;
    void Start()
    {
        StartCoroutine("SpawnBullet");
    }

    void FireEnemyBullet()
    {

        GameObject playerShip = GameObject.Find("PlayerGO");

        if (playerShip != null)
        {
            GameObject bullet = (GameObject)Instantiate(enemyBulletGO);

            bullet.transform.position = transform.position;

            Vector2 direction = playerShip.transform.position - bullet.transform.position;

            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
    }

    IEnumerator SpawnBullet()
    {
        yield return new WaitForSeconds(enemyFireRate);
        FireEnemyBullet();
        StartCoroutine("SpawnBullet");
        yield return null;
    }
}
