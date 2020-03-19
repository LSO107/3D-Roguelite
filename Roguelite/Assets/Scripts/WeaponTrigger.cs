using Character.Combat;
using Character.Health;
using Character.Movement;
using Player;
using UnityEngine;

internal sealed class WeaponTrigger : MonoBehaviour
{
    private ActorData m_ActorData;
    private PlayerStats m_PlayerStats;

    private void Awake()
    {
        m_ActorData = GetComponentInParent<ActorData>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ActorData>() == null )
            return;

        m_PlayerStats = GameManager.Instance.PlayerManager.PlayerStats;

        if (m_ActorData.ActorType == ActorType.Player)
        {
            print("BOOOOOOM");
            other.GetComponentInParent<CharacterMovement>().KnockBack(transform.position);
            other.GetComponent<CharacterStats>().TakeDamage(m_PlayerStats.Damage.GetBaseValue());
        }
        else if (m_ActorData.ActorType == ActorType.Enemy)
        {
            Debug.Log("ENEMY");
        }
    }
}
