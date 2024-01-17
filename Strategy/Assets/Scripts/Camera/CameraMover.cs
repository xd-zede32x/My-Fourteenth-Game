using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _moverSpeed;
    [SerializeField] private GameObject _prefab;

    [SerializeField] private float _leftPosition, _rightPosition, _upPosition, _downPosition;

    [SerializeField] private bool _cameraScroll = true;
    [SerializeField] private bool _cameraMove = true;

    [SerializeField] private float _minZoom, _maxZoom;

    private void Update()
    {
        float position = Input.GetAxis("Mouse ScrollWheel");

        if (position != 0 && _cameraScroll)
        {
            _prefab.transform.Translate(new Vector3(0, 0, position * _moverSpeed / 10));
            _prefab.transform.localPosition = new Vector3(_prefab.transform.localPosition.x, Mathf.Clamp(_prefab.transform.localPosition.y, _minZoom, _maxZoom), transform.localPosition.z);
        }

        if (_cameraMove)
        {
            if ((transform.position.z <= _leftPosition) && Input.mousePosition.x < 2)
                transform.position += transform.forward * _moverSpeed * Time.deltaTime;

            if ((transform.position.z >= _rightPosition) && (int)Input.mousePosition.x > Screen.width - 2)
                transform.position -= transform.forward * _moverSpeed * Time.deltaTime;

            if ((transform.position.x <= _upPosition) && Input.mousePosition.y > Screen.height - 2)
                transform.position += transform.right * _moverSpeed * Time.deltaTime;

            if ((transform.position.x >= _downPosition) && Input.mousePosition.y < 2)
                transform.position -= transform.right * _moverSpeed * Time.deltaTime;
        }
    }
}