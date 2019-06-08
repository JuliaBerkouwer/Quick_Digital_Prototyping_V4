using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    private Vector3 _direction;
    private bool isReady;
    public GameObject impactEffect;

    void Awake()
    {
        isReady = false;
    }
    public void SetDirection(Vector3 direction)
    {
        _direction = direction.normalized;

        isReady = true;
    }

    void Update()
    {
        if (isReady)
        {
            Vector3 position = transform.position;

            position += _direction * speed * Time.deltaTime;

            Vector3 direction = position - transform.position;
            transform.up = -direction;

            transform.position = position;

            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

            if ((transform.position.x < min.x) || (transform.position.x > max.x) || (transform.position.y > max.y) || (transform.position.y > max.y))
            {
                Destroy(gameObject);
            }
        }
    }

    private void Explosion()
    {
        GameObject ImpactEffect = Instantiate(impactEffect, transform.position, Quaternion.identity);
        ImpactEffect.GetComponent<ParticleSystem>().Play();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.tag == "PlayerShipTag") || (col.gameObject.tag == "WallTag") || (col.gameObject.tag == "Friend") || (col.gameObject.tag == "PlayerBulletTag"))
        {
            Destroy(gameObject);
            if (col.gameObject.tag == "PlayerShipTag")
            {
                Explosion();
            }
        }
    }
}
