using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] private float _maxRange;
    [SerializeField] private Transform _rangeIndicator;

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

    private void Start()
    {
        _rangeIndicator.localScale = _maxRange * 2 * Vector3.one;
        _rangeIndicator.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_isGrounded && Input.GetMouseButtonDown(0)) Launch();

        if (Input.GetKeyDown(KeyCode.Q)) PlayerHealth.Instance.Die();

        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene(0);
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapBox(_groundCheck.position, _groundCheckSize, 0, _groundCheckLayer);

        if (_isGrounded && !_rangeIndicator.gameObject.activeInHierarchy && _rb2D.linearVelocity == Vector2.zero) _rangeIndicator.gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) _rb2D.linearVelocityX = 0;
    }

    private void Launch()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 diff = mousePos - (Vector2)transform.position;

        //Add 0.1f so the player doesn't strictly have to click within the circle.
        //Cancel the launch if the player clicks outside the circle to prevent unintended launches.
        if (diff.magnitude > _maxRange + 0.1f) return;

        diff = Vector2.ClampMagnitude(diff, _maxRange);

        _rb2D.linearVelocity = diff * _launchForce;

        _rangeIndicator.gameObject.SetActive(false);
    }
}
