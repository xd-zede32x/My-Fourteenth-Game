using UnityEngine;

[RequireComponent(typeof(Animations))]
public class PlayerAnimator : MonoBehaviour
{
    private Animations _animations;

    private void Start()
    {
        _animations = GetComponent<Animations>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _animations.PlayAttack();

        if (Input.GetMouseButtonDown(1))
            _animations.PlayDamage();
    }
}