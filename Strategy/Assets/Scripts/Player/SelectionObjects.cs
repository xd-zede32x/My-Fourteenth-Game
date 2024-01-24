using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

[RequireComponent(typeof(Camera))]
public class SelectionObjects : MonoBehaviour
{
    private readonly int Walk = Animator.StringToHash(nameof(Walk));

    [SerializeField] private GameObject _template;
    [SerializeField] private LayerMask _layerMask, _layerPlayer;
    [SerializeField] private List<GameObject> _players;
    [SerializeField] private Animator _wingsAnimator;

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
        if (Input.GetMouseButtonDown(1) && _players.Count > 0)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit agentTarget, 1000f, _layerMask))
            {
                foreach (var player in _players)
                {
                    player.GetComponent<NavMeshAgent>().SetDestination(agentTarget.point);

                    _wingsAnimator.SetTrigger(Walk);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            foreach (var player in _players)
            {
                player.transform.GetChild(0).gameObject.SetActive(false);
                player.transform.GetChild(1).gameObject.SetActive(false);
            }

            _players.Clear();

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out _hit, 1000f, _layerMask))
            {
                _selectionCube = Instantiate(_template, new Vector3(_hit.point.x, 0.5f, _hit.point.z), Quaternion.identity);
            }
        }

        if (_selectionCube)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitDrag, 1000f, _layerMask))
            {
                float xScale = (_hit.point.x - hitDrag.point.x) * -1;
                float zScale = _hit.point.z - hitDrag.point.z;

                if (xScale < 0.0f && zScale < 0.0f)
                    RotateSelectionCube(0, 180, 0);

                else if (xScale < 0.0f)
                    RotateSelectionCube(0, 0, 180);

                else if (zScale < 0.0f)
                    RotateSelectionCube(180, 0, 0);

                else
                    RotateSelectionCube(0, 0, 0);

                _selectionCube.transform.localScale = new Vector3(Mathf.Abs(xScale), 1, Mathf.Abs(zScale));
            }
        }

        if (Input.GetMouseButtonUp(0) && _selectionCube)
        {
            RaycastHit[] hits = Physics.BoxCastAll(_selectionCube.transform.position, _selectionCube.transform.localScale, Vector3.up, Quaternion.identity, 0, _layerPlayer);

            foreach (var hit in hits)
            {
                _players.Add(hit.transform.gameObject);

                hit.transform.GetChild(0).gameObject.SetActive(true);
                hit.transform.GetChild(1).gameObject.SetActive(true);
            }

            Destroy(_selectionCube);
        }
    }

    private void RotateSelectionCube(int x, int y, int z)
    {
        _selectionCube.transform.localRotation = Quaternion.Euler(new Vector3(x, y, z));
    }
}