using UnityEngine;

public class Projectile : MonoBehaviour
{
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
        Expire();
    }

    private void Expire()
    {
        Destroy(gameObject);
    }
}
