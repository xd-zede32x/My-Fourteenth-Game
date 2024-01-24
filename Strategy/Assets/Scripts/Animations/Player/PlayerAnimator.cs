using UnityEngine;

[RequireComponent(typeof(Animations))]
public class PlayerAnimator : MonoBehaviour
{
    private Animations _clickAnimation;

    private void Start()
    {
        _clickAnimation = GetComponent<Animations>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _clickAnimation.PlayAttack();

        if (Input.GetMouseButtonDown(1))
            _clickAnimation.PlayDamage();
    }
}