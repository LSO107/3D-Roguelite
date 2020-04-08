using Character.Combat;
using Character.Movement;
using Player;
using UnityEngine;

internal sealed class WeaponTrigger : MonoBehaviour
{
    private ActorData m_MyActorData;
    private CharacterCombat m_MyCombatData;

    private void Awake()
    {
        m_MyActorData = GetComponentInParent<ActorData>();
        m_MyCombatData = GetComponentInParent<CharacterCombat>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ActorData>() == null )
            return;

        var targetCombatData = other.GetComponent<CharacterCombat>();

        if (targetCombatData == null)
            return;

        if (m_MyActorData.ActorType == ActorType.Player)
        {
            if (targetCombatData.CombatState == CombatState.Blocking)
            {
                Debug.Log("Blocked Attack.");
            }
            else
            {
                Debug.Log("Hit enemy");
                other.GetComponentInParent<CharacterMovement>().KnockBack(transform.position);
                other.GetComponent<CharacterStats>().TakeDamage();
            }
        }
        else if (m_MyActorData.ActorType == ActorType.Enemy)
        {
            if (m_MyCombatData.CombatState != CombatState.Attacking)
                return;

            if (targetCombatData.CombatState == CombatState.Blocking)
            {
                Debug.Log("Blocked Attack.");
                return;
            }

            Debug.Log("ENEMY HIT ME");
            other.GetComponentInParent<CharacterMovement>().KnockBack(transform.position);
            var damage = m_MyActorData.GetComponentInParent<CharacterStats>().Damage.GetBaseValue();
            other.GetComponentInParent<PlayerStats>().TakeDamage(damage);
        }
    }
}
