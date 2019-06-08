using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private List<GameObject> enemies = new List<GameObject>();
    public GameObject enemyObject;
    public float speedMultiplier = 1.3f;
    private int splits = 2;
    private float offsetX;
    private float offsetY;
    private int amountOfEnemies = 1;
    private int enemiesSpawned = 0;

    private void Start()
    {
        offsetX = Random.Range(1f, 4f);
        offsetY = Random.Range(1f, 4f);

        for (int i = 1; i <= splits; i++)
        {
            amountOfEnemies += splits * i;
        }

        GameObject newEnemy = Instantiate(enemyObject, (Vector2)transform.position + new Vector2(offsetX, offsetY), Quaternion.identity);
        SplitEnemy currentEnemy = newEnemy.GetComponent<SplitEnemy>();
        currentEnemy.splits = splits;
        currentEnemy.enemyController = this;
        enemies.Add(newEnemy);
    }

    public void EnmemyDestroyed(SplitEnemy enemy)
    {

        if (enemy.splits > 0)
        {
            Vector2 nextPosition = (Vector2)enemy.transform.position;
            Vector2 offset = enemy.gameObject.GetComponent<SpriteRenderer>().bounds.size;
            nextPosition.x -= offset.x * 2;
            nextPosition.y += offset.y * 2.5f;

            for (int i = 0; i < 2; i++)
            {
                GameObject newEnemy = Instantiate(enemyObject, nextPosition, Quaternion.identity);
                newEnemy.transform.localScale = enemy.transform.localScale * 0.5f;

                SplitEnemy currentEnemy = newEnemy.GetComponent<SplitEnemy>();
                currentEnemy.speed = enemy.speed * speedMultiplier;
                currentEnemy.splits = enemy.splits - 1;
                currentEnemy.enemyController = this;

                enemies.Add(newEnemy);

                nextPosition.x += offset.x * 2;
            }
        }
        enemies.Remove(enemy.gameObject);
        AreAllSpawned();
    }

    private void AreAllSpawned()
    {
        enemiesSpawned++;
        if (enemiesSpawned >= amountOfEnemies)
        {
            ScoreManager.Instance.AddMultiplier();
            Destroy(gameObject);
        }
    }
}
