using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;

    public EnemyType Type;
    public enum EnemyType
    {
        None, //Enemies that appear in both plant and animal cells would have the None type.
        Animal,
        Plant
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0) Destroy(gameObject);
    }
}
