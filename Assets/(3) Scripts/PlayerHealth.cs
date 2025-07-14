using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance { get; private set; }

    private Vector2 _spawnPos;

    private float _spawnInvulnTime;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _spawnPos = transform.position;
    }

    private void Update()
    {
        _spawnInvulnTime -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -10) Die();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn")) Checkpoint(collision.transform.position);

        if (collision.gameObject.CompareTag("Finish")) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Die()
    {
        if (_spawnInvulnTime > 0) return;

        transform.position = _spawnPos;
        _spawnInvulnTime = 1f; //Prevents being instantly killed again if you get unlucky.
    }

    public void Checkpoint(Vector2 pos)
    {
        _spawnPos = pos;
    }
}
