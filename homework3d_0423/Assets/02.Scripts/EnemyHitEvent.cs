using UnityEngine;

public class EnemyHitEvent : MonoBehaviour
{
    public Enemy MyEnemy;

    public void AttackEenvet()
    {
        MyEnemy.Attack();
        Debug.Log("PlayerAttack");
    }
}
