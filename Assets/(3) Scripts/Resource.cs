using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private ResourceType _type;
    private enum ResourceType
    {
        None,
        Energy,
        Water,
        Fire
    }

    [SerializeField] private int _resourceValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (_type)
        {
            case ResourceType.Energy:
                PlayerCrafting.Instance.ChangeEnergy(_resourceValue);
                break;
            case ResourceType.Water:
                PlayerCrafting.Instance.ChangeWater(_resourceValue);
                break;
            case ResourceType.Fire:
                PlayerCrafting.Instance.ChangeFire(_resourceValue);
                break;
            default:
                break;
        }

        Destroy(gameObject);
    }
}
