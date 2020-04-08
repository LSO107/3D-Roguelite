using AI;
using Character.Combat;
using Extensions;
using UnityEngine;

using Random = System.Random;

internal sealed class BlockingBehaviour : MonoBehaviour, IBehaviour
{
    private EnemyAI m_EnemyAI;
    private BehaviourState m_CurrentState;

    private float m_BlockTime = 2;
    private float m_CurrentTime;

    private const float MinimumBlockTime = 2.0f;
    private const float MaximumBlockTime = 6.0f;

    private Random m_Random;

    public void Initialize(Random random)
    {
        m_EnemyAI = GetComponent<EnemyAI>();
        m_Random = random;
    }

    public void UpdateState(BehaviourState state)
    {
        m_CurrentState = state;
    }

    public void Execute()
    {
        Debug.Log("Blocking now");
        m_EnemyAI.UpdateCombatState(CombatState.Blocking);
    }

    public void Stop()
    {
        
    }

    public void ProcessData(AIDataObject data)
    {
        if (data.DistanceFromPlayer <= 5)
        {
            m_CurrentTime += Time.deltaTime;

            if (m_CurrentTime >= m_BlockTime)
            {
                var randomTime = m_Random.RandomFloat(MinimumBlockTime, MaximumBlockTime);
                m_EnemyAI.RegisterExecutionRequest(this, randomTime);
                m_CurrentTime = 0f;
            }
        }
        else
        {
            m_EnemyAI.UnregisterExecutionRequest(this);
            m_CurrentTime = 0;
        }
    }
}
