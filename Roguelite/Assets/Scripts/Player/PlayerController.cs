using System.Collections;
using Character.Combat;
using Character.Movement;
using UnityEngine;

namespace Player
{
    internal sealed class PlayerController : MonoBehaviour
    {
        private Animator m_Animator;
        private CharacterCombat m_CharacterCombat;
        private CharacterMovement m_CharacterMovement;

        private int m_AttackType;

        public bool IsInputBlocked { get; private set; }

        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_CharacterCombat = GetComponent<CharacterCombat>();
            m_CharacterMovement = GetComponent<CharacterMovement>();
        }

        private void Update()
        {
            if (IsInputBlocked)
                return;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (CanDoAction)
                {
                    StartCoroutine(Attack());
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (CanDoAction)
                {
                    StartCoroutine(Block());
                }
            }
        }

        public void ToggleInputBlocked()
        {
            IsInputBlocked = !IsInputBlocked;
        }

        private IEnumerator Attack()
        {
            m_CharacterCombat.UpdateState(CombatState.Attacking);

            m_Animator.SetInteger("Attack Type", m_AttackType);
            m_Animator.SetTrigger("Attack");
            m_AttackType++;

            if (m_AttackType >= 5)
                m_AttackType = 0;

            yield return new WaitUntil(() => m_Animator.GetBool("Attack") == false);
            m_CharacterCombat.UpdateState(CombatState.None);
        }

        private IEnumerator Block()
        {
            m_CharacterCombat.UpdateState(CombatState.Blocking);
            m_Animator.SetBool("IsBlocking", true);

            yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Mouse1));

            m_CharacterCombat.UpdateState(CombatState.None);
            m_Animator.SetBool("IsBlocking", false);
        }

        private bool CanDoAction => m_CharacterCombat.CombatState == CombatState.None;
    }
}
