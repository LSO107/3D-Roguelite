using System.Collections.Generic;
using System.Linq;
using Character.Health;
using UnityEngine;

namespace AI
{
    internal sealed class AiDataObject
    {
        public int PlayerCurrentHealth { get; }
        public Vector3 PlayerLocation { get; }
        public float DistanceFromPlayer { get; }
        public IReadOnlyCollection<Transform> SurroundingTransforms { get; }
        public EnemyHealth Health { get; }

        public AiDataObject(int playerCurrentHealth,
            Vector3 playerLocation,
            float distanceFromPlayer,
            IEnumerable<Transform> surroundingTransforms,
            EnemyHealth health)
        {
            PlayerCurrentHealth = playerCurrentHealth;
            PlayerLocation = playerLocation;
            DistanceFromPlayer = distanceFromPlayer;
            SurroundingTransforms = surroundingTransforms.ToArray();
            Health = health;
        }
    }
}