using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScreen : MonoBehaviour
{
    public float moveSpeed;

    void Update()
    {
        if (transform.position.y < 222)
            transform.position += Vector3.up * moveSpeed;
    }

}
