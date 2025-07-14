using UnityEngine;
using UnityEngine.SceneManagement;

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

        if (Input.GetKeyDown(KeyCode.R)) PlayerHealth.Instance.Die();

        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene(0);
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapBox(_groundCheck.position, _groundCheckSize, 0, _groundCheckLayer);
    }

    private void Launch()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 diff = mousePos - (Vector2)transform.position;
        _rb2D.AddForce(diff * _launchForce);
    }
}
