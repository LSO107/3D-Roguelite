namespace AI
{
    internal interface ICombatBehaviour
    {
        void Initialize(System.Random random);
        void ProcessData(AiDataObject data);
        void UpdateState(CombatState state);
        void Execute();
        void Stop();
    }
}