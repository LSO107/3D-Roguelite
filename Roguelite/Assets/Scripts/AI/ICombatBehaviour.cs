namespace AI
{
    internal interface ICombatBehaviour
    {
        void Initialize(System.Random random);
        void ProcessData(AIDataObject data);
        void UpdateState(CombatState state);
        void Execute();
        void Stop();
    }
}