using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private int _damage;

    [SerializeField] private float _moveSpeed;

    [SerializeField] private float _offset;

    private Rigidbody2D _rb2D;

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _rb2D.linearVelocity = _moveSpeed * transform.up;
        Invoke(nameof(Expire), 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.Die();
        }

        Expire();
    }

    private void Expire()
    {
        Destroy(gameObject);
    }
}
