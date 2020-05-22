using AI;
using Character.Combat;
using Extensions;
using UnityEngine;
using CombatState = AI.CombatState;
using Random = System.Random;

internal sealed class BlockingBehaviour : MonoBehaviour, ICombatBehaviour
{
    private EnemyAICombat m_EnemyAiCombat;
    private CombatState m_CurrentState;

    private float m_BlockTime = 2;
    private float m_CurrentTime;

    private const float MinimumBlockTime = 2.0f;
    private const float MaximumBlockTime = 6.0f;

    private Random m_Random;

    private static readonly int IsBlocking = Animator.StringToHash("IsBlocking");

    public void Initialize(Random random)
    {
        m_EnemyAiCombat = GetComponent<EnemyAICombat>();
        m_Random = random;
    }

    public void UpdateState(CombatState state)
    {
        m_CurrentState = state;
    }

    public void Execute()
    {
        Debug.Log("Blocking now");
        m_EnemyAiCombat.Animator.SetBool(IsBlocking, true);
        m_EnemyAiCombat.UpdateCombatState(Character.Combat.CombatState.Blocking);
    }

    public void Stop()
    {
        Debug.Log("Stopped blocking");
        m_EnemyAiCombat.Animator.SetBool(IsBlocking, false);
    }

    public void ProcessData(AIDataObject data)
    {
        if (data.DistanceFromPlayer <= 5)
        {
            m_CurrentTime += Time.deltaTime;

            if (m_CurrentTime >= m_BlockTime)
            {
                var randomTime = m_Random.RandomFloat(MinimumBlockTime, MaximumBlockTime);
                m_EnemyAiCombat.RegisterExecutionRequest(this, randomTime);
                m_CurrentTime = 0f;
            }
        }
        else
        {
            m_EnemyAiCombat.UnregisterExecutionRequest(this);
            m_CurrentTime = 0;
        }
    }
}
