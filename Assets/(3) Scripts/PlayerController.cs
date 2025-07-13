using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] private float _launchForce;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Vector2 _groundCheckSize;
    [SerializeField] private LayerMask _groundCheckLayer;
    private bool _isGrounded;

    private Rigidbody2D _rb2D;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isGrounded && Input.GetMouseButtonDown(0)) Launch();
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapBox(_groundCheck.position, _groundCheckSize, 0, _groundCheckLayer);
    }

    private void Launch()
    {

    }
}
