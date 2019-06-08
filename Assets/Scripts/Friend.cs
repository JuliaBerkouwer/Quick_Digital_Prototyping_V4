using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : MonoBehaviour
{
    public FriendPositions fp;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Contains("EnemyBullet") || col.gameObject.tag.Contains("BossBullet"))
        {
            FindObjectOfType<ParticleRotationCenter>().RemoveFriend(fp);
            Destroy(transform.parent.gameObject);
        }
    }
}
