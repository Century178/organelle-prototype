using UnityEngine;
using TMPro;

public class PlayerCrafting : MonoBehaviour
{
    public static PlayerCrafting Instance { get; private set; }

    [SerializeField] private int _energyCount, _waterCount, _fireCount;

    [SerializeField] private TextMeshProUGUI _energyCountText, _waterCountText, _fireCountText;

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
        //Initialize resource count UI;
        ChangeEnergy(0);
        ChangeWater(0);
        ChangeFire(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && _waterCount > 0 && _energyCount > 0)
        {
            PlayerShoot.Instance.PlantVialCount(1);
            ChangeEnergy(-1);
            ChangeWater(-1);
        }

        if (Input.GetKeyDown(KeyCode.X) && _fireCount > 0 && _energyCount > 0)
        {
            PlayerShoot.Instance.LightVialCount(1);
            ChangeEnergy(-1);
            ChangeFire(-1);
        }
    }

    public void ChangeEnergy(int count)
    {
        _energyCount += count;
        _energyCountText.text = "Energy: " + _energyCount;
    }

    public void ChangeWater(int count)
    {
        _waterCount += count;
        _waterCountText.text = "Water: " + _waterCount;
    }

    public void ChangeFire(int count)
    {
        _fireCount += count;
        _fireCountText.text = "Fire: " + _fireCount;
    }
}
