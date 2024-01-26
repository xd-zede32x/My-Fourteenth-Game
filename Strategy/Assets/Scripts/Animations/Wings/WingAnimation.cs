using UnityEngine;

[RequireComponent(typeof(Animations))]
public class WingAnimation : MonoBehaviour
{
    private Animations _animations;

    private void Start()
    {
        _animations = GetComponent<Animations>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
            _animations.PlayWalkWing();
    }
}   