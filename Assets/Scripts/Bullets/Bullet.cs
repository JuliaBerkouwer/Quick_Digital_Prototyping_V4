using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D bulletRigidbody;
    public Vector2 velocity;
    public GameObject impactEffect;

    public float speed;

    private void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 position = transform.position;

        position = new Vector2(position.x, position.y + speed * Time.deltaTime);
        transform.position = position;

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        if (transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
    }

    private void Explosion()
    {
        GameObject ImpactEffect = Instantiate(impactEffect, transform.position, Quaternion.identity);
        ImpactEffect.GetComponent<ParticleSystem>().Play();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "EnemyShipTag") || (collision.gameObject.tag == "BossBulletTag") || (collision.gameObject.tag == "WallTag") || (collision.gameObject.tag == "EnemyBulletTag"))
        {
            Explosion();
            Destroy(gameObject);
        }
    }


}
