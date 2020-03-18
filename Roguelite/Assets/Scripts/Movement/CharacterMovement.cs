using System.Collections;
using UnityEngine;

namespace Movement
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
	internal sealed class CharacterMovement : MonoBehaviour
	{
		[SerializeField] private float m_MovingTurnSpeed = 360;
		[SerializeField] private float m_StationaryTurnSpeed = 180;
		[SerializeField] private float m_MoveSpeedMultiplier = 1f;
		[SerializeField] private float m_AnimSpeedMultiplier = 1f;

		private Rigidbody m_Rigidbody;
		private Animator m_Animator;
		private float m_TurnAmount;
		private float m_ForwardAmount;

        private static readonly int Forward = Animator.StringToHash("Forward");
        private static readonly int Turn = Animator.StringToHash("Turn");

        public bool CanMove = true;

        private void Start()
		{
			m_Animator = GetComponent<Animator>();
			m_Rigidbody = GetComponent<Rigidbody>();
		}

        public void Move(Vector3 move)
		{
            if (move.magnitude > 1f)
            {
				move.Normalize();
            }

			move = transform.InverseTransformDirection(move);
			m_TurnAmount = Mathf.Atan2(move.x, move.z);
			m_ForwardAmount = move.z;

			ApplyExtraTurnRotation();
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
			Debug.Log("HI");
        }

        private void UpdateAnimator(Vector3 move)
		{
			m_Animator.SetFloat(Forward, m_ForwardAmount, 0.1f, Time.deltaTime);
			m_Animator.SetFloat(Turn, m_TurnAmount, 0.1f, Time.deltaTime);

			if (move.magnitude > 0)
			{
				m_Animator.speed = m_AnimSpeedMultiplier;
			}
			else
			{
                m_Animator.speed = 1;
			}
		}

        private void ApplyExtraTurnRotation()
		{
			var turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
			transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
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
