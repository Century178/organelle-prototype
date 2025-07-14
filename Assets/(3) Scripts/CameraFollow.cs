using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _followSpeed = 1f;

    private void LateUpdate()
    {
        Vector3 newPos = Vector3.Lerp(transform.position, _target.position, Time.deltaTime * _followSpeed);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }
}
