using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static PlayerShoot Instance { get; private set; }

    [SerializeField] private Transform _vialGun;
    [SerializeField] private Transform _firePoint;

    [SerializeField] private Projectile _projectile;

    [SerializeField] private float _offset;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float newAngle = (Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg) - _offset;
        _vialGun.localEulerAngles = new Vector3(0, 0, newAngle);

        if (Input.GetMouseButtonDown(1)) Shoot();
    }

    private void Shoot()
    {
        Instantiate(_projectile, _firePoint.position, _vialGun.rotation);
    }
}
