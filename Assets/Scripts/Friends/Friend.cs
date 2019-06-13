using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : MonoBehaviour
{
    public GameObject deathEffect;
    public GameObject impactEffect;

    public int damage = 3;
    public int currentHealth = 9;
    private bool isFriendHit = false;

    public FriendPositions fp;

    void Update()
    {
        if (currentHealth <= 0)
        {
            DestroyFriend();
        }
    }

    private void DeathVFX()
    {
        GameObject DeathEffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        DeathEffect.GetComponent<ParticleSystem>().Play();
    }

    private void ImpactVFX()
    {
        GameObject ImpactEffect = Instantiate(impactEffect, transform.position, Quaternion.identity);
        ImpactEffect.GetComponent<ParticleSystem>().Play();
    }

    private void DestroyFriend()
    {
        DeathVFX();
        FindObjectOfType<ParticleRotationCenter>().RemoveFriend(fp);
        Destroy(transform.parent.gameObject);

    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Contains("EnemyBullet") || col.gameObject.tag.Contains("BossBullet"))
        {
            isFriendHit = true;
            currentHealth -= damage;
            ImpactVFX();
            isFriendHit = false;
        }
    }
}
