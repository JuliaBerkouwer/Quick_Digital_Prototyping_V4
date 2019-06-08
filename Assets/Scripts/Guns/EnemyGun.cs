using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public float enemyFireRate;
    public GameObject enemyBulletGO;

    private void Start()
    {
        StartCoroutine("SpawnBullet");
    }

    void FireEnemyBullet()
    {
        GameObject bullet = Instantiate(enemyBulletGO);

        bullet.transform.position = transform.position;

        Vector3 direction = transform.up;

        bullet.GetComponent<EnemyBullet>().SetDirection(direction);
    }

    IEnumerator SpawnBullet()
    {
        yield return new WaitForSeconds(enemyFireRate);
        FireEnemyBullet();
        StartCoroutine("SpawnBullet");
        yield return null;
    }
}
