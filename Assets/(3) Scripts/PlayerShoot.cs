using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static PlayerShoot Instance { get; private set; }

    [SerializeField] private Transform _vialGun;
    [SerializeField] private Transform _firePoint;

    [SerializeField] private Projectile _vial1, _vial2;
    private Projectile _currentVial;

    [SerializeField] private float _offset;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _currentVial = _vial1;
    }

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float newAngle = (Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg) - _offset;
        _vialGun.localEulerAngles = new Vector3(0, 0, newAngle);

        if (Input.GetMouseButtonDown(1)) Shoot();

        if (Input.GetKeyDown(KeyCode.Space)) SwapVial();
    }

    private void Shoot()
    {
        Instantiate(_currentVial, _firePoint.position, _vialGun.rotation);
    }

    private void SwapVial()
    {
        if (_currentVial == _vial1) _currentVial = _vial2;
        else _currentVial = _vial1;
    }
}
