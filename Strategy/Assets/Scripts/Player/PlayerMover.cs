using UnityEngine;
using UnityEngine.AI;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private SelectionObjects _selectionObjects;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private CursorState _cursorState;
    [SerializeField] private Animations _animations;
    [SerializeField] private Texture2D[] _cursors;

    private void Update() => Move();

    private void Move()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (_selectionObjects.Players.Count == 0) return;

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit agentTarget, 1000f, _layerMask))
            {
                foreach (var player in _selectionObjects.Players)
                {
                    player.GetComponent<NavMeshAgent>().SetDestination(agentTarget.point);

                    _animations.PlayWalk();
                    _cursorState.ChangeCursorState(_cursors[1]);
                }
            }
        }

        else
            _cursorState.ChangeCursorState(_cursors[0]);
    }
}