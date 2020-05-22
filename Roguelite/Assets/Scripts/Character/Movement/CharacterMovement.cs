using System.Collections;
using UnityEngine;

namespace Character.Movement
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
	internal sealed class CharacterMovement : MonoBehaviour
	{
		[SerializeField] private float m_MoveSpeedMultiplier = 1f;
		[SerializeField] private float m_AnimSpeedMultiplier = 1f;

		private Rigidbody m_Rigidbody;
		private Animator m_Animator;

		private float m_ForwardAmount;
		private float m_LateralAmount;

        private static readonly int Forward = Animator.StringToHash("Forward");
        private static readonly int Lateral = Animator.StringToHash("Lateral");

        public bool CanMove = true;

        private void Awake()
		{
			m_Animator = GetComponent<Animator>();
			m_Rigidbody = GetComponent<Rigidbody>();
        }

        public void Move(Vector3 move)
		{
            if (move.magnitude > 1f || move.magnitude < 0)
            {
				move.Normalize();
            }

			move = transform.InverseTransformDirection(move);

            m_LateralAmount = move.x;
			m_ForwardAmount = move.z;

            UpdateAnimator(move);
		}

        public void KnockBack(Vector3 attackerPosition)
        {
            m_Rigidbody.AddExplosionForce(200, attackerPosition, 50, 0f);
            CanMove = false;
            StartCoroutine(ResetCanMove());
        }

        private IEnumerator ResetCanMove()
        {
            yield return new WaitForSeconds(2);
            CanMove = true;
        }

        private void UpdateAnimator(Vector3 move)
        {
            m_Animator.SetFloat(Forward, m_ForwardAmount, 0.1f, Time.deltaTime);
			m_Animator.SetFloat(Lateral, m_LateralAmount, 0.1f, Time.deltaTime);

			if (move.magnitude > 0)
			{
				m_Animator.speed = m_AnimSpeedMultiplier;
			}
			else
			{
                m_Animator.speed = 1;
			}
		}

        public void OnAnimatorMove()
		{
            if (!(Time.deltaTime > 0) || !CanMove) 
                return;

            var v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;
            v.y = m_Rigidbody.velocity.y;
            m_Rigidbody.velocity = v;
        }
    }
}
