using Character.Combat;
using Character.Movement;
using Player;
using UnityEngine;

internal sealed class WeaponTrigger : MonoBehaviour
{
    private ActorData m_ActorData;

    private void Awake()
    {
        m_ActorData = GetComponentInParent<ActorData>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ActorData>() == null )
            return;

        if (m_ActorData.ActorType == ActorType.Player)
        {
            print("BOOOOOOM");
            other.GetComponentInParent<CharacterMovement>().KnockBack(transform.position);
            other.GetComponent<CharacterStats>().TakeDamage();
        }
        else if (m_ActorData.ActorType == ActorType.Enemy)
        {
            Debug.Log("ENEMY HIT ME");
            GetComponentInParent<CharacterMovement>().KnockBack(transform.position);
            var damage = m_ActorData.GetComponentInParent<CharacterStats>().Damage.GetBaseValue();
            GetComponentInParent<PlayerStats>().TakeDamage(damage);
        }
    }
}
