using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFriend : MonoBehaviour
{
    //public ParticleCollisionDetection _ParticleCollisionDetection;
    public GameObject pickUpEffect;
    public int scoreValue = 7;

    private void PickUp()
    {
        GameObject ImpactEffect = Instantiate(pickUpEffect, transform.position, Quaternion.identity);
        ImpactEffect.GetComponent<ParticleSystem>().Play();
    }

    public void DestoryFriend()
    {
        ScoreManager.Instance.AddScore(scoreValue);
        Destroy(gameObject);
        FindObjectOfType<ParticleRotationCenter>().CreateFriend();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "PlayerShipTag")
        {
            //_ParticleCollisionDetection.AddLifetimeParticles();
            // PickUp();
            DestoryFriend();
        }
    }
}

