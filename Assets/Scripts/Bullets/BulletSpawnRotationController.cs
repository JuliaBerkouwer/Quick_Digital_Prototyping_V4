using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnRotationController : MonoBehaviour
{
    Vector3 eulerAngle;
    public float moveSpeed;

    private void Update()
    {

        eulerAngle = new Vector3(0, 0, moveSpeed);

        Quaternion target = Quaternion.Euler(eulerAngle);
        //print(eulerAngle + " : " + target);
        transform.Rotate(eulerAngle);
    }
}
