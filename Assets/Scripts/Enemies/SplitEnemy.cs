using UnityEngine;

public class SplitEnemy : Enemy
{
    [HideInInspector] public EnemyController enemyController;
    public int splits;

    protected override void DestroyEnemy()
    {
        enemyController.EnmemyDestroyed(this);
        base.DestroyEnemy();
    }
}
