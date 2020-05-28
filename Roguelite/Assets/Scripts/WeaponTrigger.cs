using System.Runtime.InteropServices;
using Character.Combat;
using Character.Movement;
using Player;
using UnityEngine;

internal sealed class WeaponTrigger : MonoBehaviour
{
    private ActorData m_MyActorData;
    private CharacterCombat m_MyCombatData;

    [SerializeField] private AudioClip m_HitAudioClip = null;

    private void Awake()
    {
        m_MyActorData = GetComponentInParent<ActorData>();
        m_MyCombatData = GetComponentInParent<CharacterCombat>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ActorData>() == null || m_MyActorData == null)
            return;

        var targetCombatData = other.GetComponent<CharacterCombat>();

        if (targetCombatData == null)
            return;

        if (m_MyCombatData.CombatState != CombatState.Attacking)
            return;

        if (m_MyActorData.ActorType == ActorType.Player)
        {
            if (targetCombatData.CombatState == CombatState.Blocking)
            {
                Debug.Log("Blocked Attack.");
                other.GetComponent<CharacterStats>().splatMarker.Show(0);
                other.GetComponent<Animator>().SetTrigger("Impact");
                return;
            }

            Debug.Log("Hit enemy");
            PlayerManager.Instance.SoundEffects.Play(m_HitAudioClip);
            other.GetComponentInParent<CharacterMovement>().KnockBack(transform.position);
            var damage = m_MyActorData.GetComponentInParent<PlayerStats>().Damage.GetBaseValue();
            other.GetComponent<CharacterStats>().TakeDamage(damage);
        }
        else if (m_MyActorData.ActorType == ActorType.Enemy)
        {
            if (!other.CompareTag("Player"))
                return;

            if (targetCombatData.CombatState == CombatState.Blocking)
            {
                Debug.Log("Blocked Attack.");
                other.GetComponent<Animator>().SetTrigger("Impact");
                return;
            }

            other.GetComponentInParent<CharacterMovement>().KnockBack(transform.position);
            var damage = m_MyActorData.GetComponentInParent<CharacterStats>().Damage.GetBaseValue();
            other.GetComponent<PlayerStats>().TakeDamage(damage);
        }
    }
}
