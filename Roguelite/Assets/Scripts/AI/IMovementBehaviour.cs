using System;
using Character.Movement;

namespace AI
{
    internal interface IMovementBehaviour
    {
        void Initialize(Random random, CharacterMovement movement);

        void ProcessData(AiDataObject dataObject);

        void Execute();

        void Stop();
    }
}
