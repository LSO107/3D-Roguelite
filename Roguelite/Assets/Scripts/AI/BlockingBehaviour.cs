using AI;
using Extensions;
using UnityEngine;
using Random = System.Random;

internal sealed class BlockingBehaviour : MonoBehaviour, ICombatBehaviour
{
    private EnemyCombatLogic m_EnemyCombat;

    private float m_BlockTime = 2;
    private float m_CurrentTime;

    private const float MinimumBlockTime = 2.0f;
    private const float MaximumBlockTime = 6.0f;

    private Random m_Random;

    private static readonly int IsBlocking = Animator.StringToHash("IsBlocking");

    public void Initialize(Random random)
    {
        m_EnemyCombat = GetComponent<EnemyCombatLogic>();
        m_Random = random;
    }

    public void Execute()
    {
        Debug.Log("Blocking now");
        m_EnemyCombat.Animator.SetBool(IsBlocking, true);
        m_EnemyCombat.UpdateCombatState(Character.Combat.CombatState.Blocking);
    }

    public void Stop()
    {
        Debug.Log("Stopped blocking");
        m_EnemyCombat.Animator.SetBool(IsBlocking, false);
    }

    public void ProcessData(AiDataObject data)
    {
        if (data.DistanceFromPlayer <= 5)
        {
            m_CurrentTime += Time.deltaTime;

            if (m_CurrentTime >= m_BlockTime)
            {
                var randomTime = m_Random.RandomFloat(MinimumBlockTime, MaximumBlockTime);
                m_EnemyCombat.RegisterExecutionRequest(this, randomTime);
                m_CurrentTime = 0f;
            }
        }
        else
        {
            m_EnemyCombat.UnregisterExecutionRequest(this);
            m_CurrentTime = 0;
        }
    }
}
