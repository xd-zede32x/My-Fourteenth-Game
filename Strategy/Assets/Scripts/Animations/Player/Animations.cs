using UnityEngine;

public class Animations : MonoBehaviour
{
    private readonly int Attack = Animator.StringToHash(nameof(Attack));
    private readonly int Damage = Animator.StringToHash(nameof(Damage));
    private readonly int Walk = Animator.StringToHash(nameof(Walk));
    private readonly int WingsWalk = Animator.StringToHash(nameof(WingsWalk));

    [SerializeField] private Animator _animator;

    public void PlayAttack()
    {
        _animator.SetTrigger(Attack);
    }

    public void PlayDamage()
    {
        _animator.SetTrigger(Damage);
    }

    public void PlayWalk()
    {
        _animator.SetTrigger(Walk);
    }

    public void PlayWalkWing()
    {
        _animator.SetTrigger(WingsWalk);
    }   
}