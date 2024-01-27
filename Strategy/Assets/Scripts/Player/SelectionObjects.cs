using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Camera))]
public class SelectionObjects : MonoBehaviour
{
    private Quaternion ROTATETION_X => Quaternion.Euler(180, 0, 0);
    private Quaternion ROTATETION_Y => Quaternion.Euler(0, 180, 0);
    private Quaternion ROTATETION_Z => Quaternion.Euler(0, 0, 180);

    [SerializeField] private GameObject _template;
    [SerializeField] private LayerMask _layerMask, _layerPlayer;
    [SerializeField] private List<GameObject> _players;

    public IReadOnlyList<GameObject> Players => _players;

    private Camera _camera;
    private RaycastHit _hit;
    private GameObject _selectionCube;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        UserInput();
    }

    private void UserInput()
    {
        if (Input.GetMouseButtonDown(0))
            CreateSelectionZone();

        if (_selectionCube)
            TransformSelectionCube();

        if (Input.GetMouseButtonUp(0) && _selectionCube)
            FinishedSelectPlyer();
    }


    private void CreateSelectionZone()
    {
        ClearListSelectionPlayers();

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out _hit, 1000f, _layerMask))
        {
            _selectionCube = Instantiate(_template, new Vector3(_hit.point.x, 0.5f, _hit.point.z), Quaternion.identity);
        }
    }

    private void SetActiveChildrenPlayer(Transform transform, bool isActive)
    {
        transform.GetChild(0).gameObject.SetActive(isActive);
        transform.GetChild(1).gameObject.SetActive(isActive);
    }

    private void RotateSelectionCube(Quaternion rotationCube)
    {
        _selectionCube.transform.localRotation = rotationCube;
    }

    private void RotateCubSelection(float xScale, float zScale)
    {
        if (xScale < 0.0f && zScale < 0.0f)
            RotateSelectionCube(ROTATETION_Y);

        else if (xScale < 0.0f)
            RotateSelectionCube(ROTATETION_Z);

        else if (zScale < 0.0f)
            RotateSelectionCube(ROTATETION_X);
    }

    private void ClearListSelectionPlayers()
    {
        foreach (var player in _players)
        {
            SetActiveChildrenPlayer(player.transform, false);
        }

        _players.Clear();
    }

    private void FinishedSelectPlyer()
    {
        RaycastHit[] hits = Physics.BoxCastAll(_selectionCube.transform.position, _selectionCube.transform.localScale, Vector3.up, Quaternion.identity, 0, _layerPlayer);

        foreach (var hit in hits)
        {
            _players.Add(hit.transform.gameObject);

            SetActiveChildrenPlayer(hit.transform, true);
        }

        Destroy(_selectionCube);
    }

    private void TransformSelectionCube()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitDrag, 1000f, _layerMask))
        {
            float xScale = (_hit.point.x - hitDrag.point.x) * -1;
            float zScale = _hit.point.z - hitDrag.point.z;

            RotateCubSelection(xScale, zScale);

            _selectionCube.transform.localScale = new Vector3(Mathf.Abs(xScale), 1, Mathf.Abs(zScale));
        }
    }
}