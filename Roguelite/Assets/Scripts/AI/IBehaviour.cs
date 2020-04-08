using UnityEngine;

namespace AI
{
    internal interface IBehaviour
    {
        void Initialize(System.Random random);
        void ProcessData(AIDataObject data);
        void UpdateState(BehaviourState state);
        void Execute();
        void Stop();
    }
}

internal enum BehaviourState
{
    Circling,
    Attacking,
    Blocking
}
