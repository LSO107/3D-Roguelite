using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animator m_Animation;

    private const int m_LightAttack = 1;
    private const int m_MediumAttack = 2;
    private const int m_HeavyAttack = 3;

    private static readonly int AttackInt = Animator.StringToHash("Attack");


    // Intermediary script that both the player and enemy can use to reset
    // animation clips using Animation Events
    //
    private void Awake()
    {
        m_Animation = GetComponent<Animator>();
    }

    public void ResetAttackAnimation()
    {
        m_Animation.SetInteger(AttackInt, 0);
    }
}
