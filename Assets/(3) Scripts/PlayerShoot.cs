using UnityEngine;
using TMPro;

public class PlayerShoot : MonoBehaviour
{
    public static PlayerShoot Instance { get; private set; }

    [SerializeField] private Transform _vialGun;
    [SerializeField] private Transform _firePoint;

    [SerializeField] private Projectile _plantVial, _lightVial;
    [SerializeField] private int _plantVialCount, _lightVialCount;
    private Projectile _currentVial;

    [SerializeField] private TextMeshProUGUI _plantVialCountText, _lightVialCountText;

    [SerializeField] private float _angleOffset;
    [SerializeField] private Vector2 _posOffset;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        //Initialize vial count UI.
        PlantVialCount(0);
        LightVialCount(0);

        _currentVial = _plantVial;

        _plantVialCountText.fontStyle = FontStyles.Italic;
        _lightVialCountText.fontStyle = FontStyles.Normal;
    }

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 rawPos = mousePos - (Vector2)transform.position + _posOffset;
        float newAngle = (Mathf.Atan2(rawPos.y, rawPos.x) * Mathf.Rad2Deg) - _angleOffset;
        _vialGun.localEulerAngles = new Vector3(0, 0, newAngle);

        if (Input.GetMouseButtonDown(1)) Shoot();

        if (Input.GetKeyDown(KeyCode.Space)) SwapVial();
    }

    private void Shoot()
    {
        if (_currentVial == _plantVial && _plantVialCount > 0)
        {
            Instantiate(_currentVial, _firePoint.position, _vialGun.rotation);
            PlantVialCount(-1);
        }
        else if (_currentVial == _lightVial && _lightVialCount > 0)
        {
            Instantiate(_currentVial, _firePoint.position, _vialGun.rotation);
            LightVialCount(-1);
        }
    }

    private void SwapVial()
    {
        if (_currentVial == _plantVial)
        {
            _currentVial = _lightVial;

            _plantVialCountText.fontStyle = FontStyles.Normal;
            _lightVialCountText.fontStyle = FontStyles.Italic;
        }
        else if (_currentVial == _lightVial)
        {
            _currentVial = _plantVial;

            _plantVialCountText.fontStyle = FontStyles.Italic;
            _lightVialCountText.fontStyle = FontStyles.Normal;
        }
    }

    public void PlantVialCount(int count)
    {
        _plantVialCount += count;
        _plantVialCountText.text = "Plant Vials: " + _plantVialCount;
    }

    public void LightVialCount(int count)
    {
        _lightVialCount += count;
        _lightVialCountText.text = "Light Vials: " + _lightVialCount;
    }
}
