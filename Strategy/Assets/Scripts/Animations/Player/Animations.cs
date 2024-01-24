using UnityEngine;

public class Animations : MonoBehaviour
{
    private readonly int Attack = Animator.StringToHash(nameof(Attack));
    private readonly int Damage = Animator.StringToHash(nameof(Damage));

    [SerializeField] private Animator _animator;

    public Animator Animator => _animator;

    public void PlayAttack()
    {
        _animator.SetTrigger(Attack);
    }

    public void PlayDamage()
    {
        _animator.SetTrigger(Damage);
    }
}