using System.Collections;
using Character.Combat;
using UnityEngine;

namespace Player
{
    internal sealed class PlayerController : MonoBehaviour
    {
        private Animator m_Animator;
        private CharacterCombat m_CharacterCombat;

        private int m_SlashAnimation = 1;
        private int m_HighSpinAnimation = 2;
        private int m_SlideAnimation = 3;

        private static readonly int AttackParam = Animator.StringToHash("Attack");
        private static readonly int IsBlocking = Animator.StringToHash("IsBlocking");

        public bool IsInputBlocked { get; private set; }

        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_CharacterCombat = GetComponent<CharacterCombat>();
        }

        private void Update()
        {
            if (IsInputBlocked)
                return;

            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (CanDoAction)
                {
                    StartCoroutine(CombatAttack(m_SlashAnimation));
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse2) || Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (CanDoAction)
                {
                    StartCoroutine(CombatAttack(m_HighSpinAnimation));
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse3) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (CanDoAction)
                {
                    StartCoroutine(CombatAttack(m_SlideAnimation));
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

        public void ToggleIsInputBlocked(bool toggle)
        {
            IsInputBlocked = toggle;
        }

        private IEnumerator CombatAttack(int anim)
        {
            m_CharacterCombat.UpdateState(CombatState.Attacking);

            m_Animator.SetInteger(AttackParam, anim);

            yield return new WaitUntil(() => m_Animator.GetInteger(AttackParam) == 0);
            m_CharacterCombat.UpdateState(CombatState.None);
        }

        // Method is used in Animation Events to reset Attack
        // at the end of each animation clip
        //
        public void ResetAttackAnimation()
        {
            m_Animator.SetInteger(AttackParam, 0);
        }

        private IEnumerator Block()
        {
            m_CharacterCombat.UpdateState(CombatState.Blocking);
            m_Animator.SetBool(IsBlocking, true);

            yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Mouse1));

            m_CharacterCombat.UpdateState(CombatState.None);
            m_Animator.SetBool(IsBlocking, false);
        }

        private bool CanDoAction => m_CharacterCombat.CombatState == CombatState.None;
    }
}
