using UnityEngine;

public class BossEnemy : Enemy
{
    protected override void Move()
    {
        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);
        transform.position = position;
    }
}