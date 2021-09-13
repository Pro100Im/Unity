using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothSpeed;

    private void LateUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 desiredPosition = new Vector3(0, 0, _target.position.z) + _offset;
        Vector3 SmoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);

        transform.position = SmoothedPosition;
        transform.LookAt(_target);
    }
}
