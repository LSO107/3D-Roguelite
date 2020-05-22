using System.Collections.Generic;
using System.Linq;
using Character.Health;
using Items.Inventory;
using Player;
using UnityEngine;

namespace AI
{
    internal static class AIDataGatherer
    {
        private static PlayerHealth PlayerHealth => PlayerManager.Instance.Health;
        private static Vector3 PlayerLocation => PlayerManager.Instance.transform.position;
        private static PlayerEquipment PlayerEquipment => PlayerManager.Instance.Equipment;

        public static AIDataObject GetData(Transform requester)
        {
            var distance = GetDistance(requester.position);
            var colliders = GetSurroundingTransforms();
            return new AIDataObject(PlayerHealth.CurrentHealth, PlayerLocation, distance, colliders);
        }

        private static IEnumerable<Transform> GetSurroundingTransforms()
        {
            var colliders = Physics.OverlapSphere(PlayerLocation, 5);

            return colliders.Select(c => c.transform);
        }

        private static float GetDistance(Vector3 requesterPosition)
        {
            return Vector3.Distance(requesterPosition, PlayerLocation);
        }
    }

    internal sealed class AIDataObject
    {
        public int PlayerCurrentHealth { get; }
        public Vector3 PlayerLocation { get; }
        public float DistanceFromPlayer { get; }
        public IReadOnlyCollection<Transform> SurroundingTransforms { get; }

        public AIDataObject(int playerCurrentHealth,
                            Vector3 playerLocation,
                            float distanceFromPlayer,
                            IEnumerable<Transform> surroundingTransforms)
        {
            PlayerCurrentHealth = playerCurrentHealth;
            PlayerLocation = playerLocation;
            DistanceFromPlayer = distanceFromPlayer;
            SurroundingTransforms = surroundingTransforms.ToArray();
        }
    }
}
