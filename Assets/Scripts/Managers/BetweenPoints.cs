using System.Collections.Generic;
using UnityEngine;

public class BetweenPoints : MonoBehaviour
{
    public List<GameObject> points;
    public float moveSpeed;
    public float rotSpeed;
    public bool rotate = true;
    public float speedMultiplier;
    public int currentPoint = 0;

    private Vector3[] targets;
    private Vector3 target;

    private void Start()
    {
        target = transform.TransformPoint(points[currentPoint].transform.localPosition);
        targets = new Vector3[points.Count];
        moveSpeed = Random.Range(moveSpeed, moveSpeed * speedMultiplier);

        for (int i = 0; i < points.Count; i++)
        {
            targets[i] = transform.TransformPoint(points[i].transform.localPosition);
        }
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(transform.position, target) <= 0.2f)
        {
            if (currentPoint >= points.Count - 1)
            {
                currentPoint = 0;
            }
            else
            {
                currentPoint++;
            }
        }

        target = targets[currentPoint];
    }
}
