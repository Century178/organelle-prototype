using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _typeCritMultiplier;

    [SerializeField] private float _moveSpeed;

    [SerializeField] private float _offset;

    [SerializeField] private VialType _vialType;
    private enum VialType
    {
        None,
        Plant,
        Light
    }

    private Rigidbody2D _rb2D;

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _rb2D.linearVelocity = _moveSpeed * transform.up;
        Invoke(nameof(Expire), 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            if (TypeMatch(enemy)) enemy.TakeDamage(_damage * _typeCritMultiplier);
            else enemy.TakeDamage(_damage);
        }

        Expire();
    }

    private bool TypeMatch(Enemy enemy)
    {
        if (enemy.Type == Enemy.EnemyType.Animal && _vialType == VialType.Plant) return true;
        if (enemy.Type == Enemy.EnemyType.Plant && _vialType == VialType.Light) return true;

        return false;
    }

    private void Expire()
    {
        Destroy(gameObject);
    }
}
