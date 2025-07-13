using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance { get; private set; }

    private Vector2 _spawnPos;

    private float _spawnInvulnTime;

    [SerializeField] private AudioClip _deathSound;

    private AudioSource _aud;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _aud = GetComponent<AudioSource>();

        _spawnPos = transform.position;
    }

    private void Update()
    {
        _spawnInvulnTime -= Time.deltaTime;
    }

    public void Die()
    {
        if (_spawnInvulnTime > 0) return;

        transform.position = _spawnPos;
        _aud.PlayOneShot(_deathSound);
        _spawnInvulnTime = 1f;
    }

    public void Checkpoint(Vector2 pos)
    {
        _spawnPos = pos;
    }
}
