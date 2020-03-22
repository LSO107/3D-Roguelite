namespace AI
{
    internal interface IBehaviour
    {
        void Initialize();
        void ProcessData(AIDataObject data);
        void UpdateState(BehaviourState state);
        void Execute();
    }
}

internal enum BehaviourState
{
    Circling,
    Attacking,
    Blocking
}
