using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] public float speed;
    private bool isHit = false;
    [SerializeField] public int currentHealth;                    // The current health the enemy has.
    [SerializeField] public int damage;
    [SerializeField] public int scoreValue = 10;               // The amount added to the player's score when the enemy dies.


    private void Update()
    {
        Move();

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }

        if (currentHealth <= 0)
        {
            DestroyEnemy();
        }
    }

    protected virtual void DestroyEnemy()
    {
        ScoreManager.Instance.AddScore(scoreValue);
        Destroy(gameObject);
    }

    protected virtual void Move()
    {
        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBulletTag" && !isHit)
        {
            isHit = true;
            currentHealth -= damage;
            isHit = false;
        }
    }
}
