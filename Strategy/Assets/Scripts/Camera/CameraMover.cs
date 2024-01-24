using UnityEngine;

public class CameraMover : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _zoomSpeed;
    [SerializeField] private float _rotateSpeed;

    [SerializeField] private float _minZoom;
    [SerializeField] private float _maxZoom;

    private void Update()
    {
        TryRotate(-_rotateSpeed, KeyCode.Q);
        TryRotate(_rotateSpeed, KeyCode.E);

        Move();
        Zoom();
    }

    private void TryRotate(float rotateSpeed, KeyCode key)
    {
        if (Input.GetKey(key))
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }

    private void Move()
    {
        float horizontal = Input.GetAxis(Horizontal);
        float vertical = Input.GetAxis(Vertical);

        transform.Translate(new Vector3(horizontal, 0, vertical) * Time.deltaTime * _moveSpeed, Space.Self);
    }

    private void Zoom()
    {
        transform.position += transform.up * _zoomSpeed * Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _minZoom, _maxZoom), transform.position.z);
    }
}