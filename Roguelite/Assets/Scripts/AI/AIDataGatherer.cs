using Character.Health;
using Items.Inventory;
using UnityEngine;

namespace AI
{
    internal static class AIDataGatherer
    {
        private static HealthObject PlayerHealth => GameManager.Instance.PlayerManager.Health;
        private static Vector3 PlayerLocation => GameManager.Instance.PlayerManager.transform.position;
        private static PlayerEquipment PlayerEquipment => GameManager.Instance.PlayerManager.Equipment;

        public static AIDataObject GetData(Transform requester)
        {
            var distance = GetDistance(requester.position);
            return new AIDataObject(PlayerHealth.CurrentHealth, PlayerLocation, distance);
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

        public AIDataObject(int playerCurrentHealth, Vector3 playerLocation, float distanceFromPlayer)
        {
            PlayerCurrentHealth = playerCurrentHealth;
            PlayerLocation = playerLocation;
            DistanceFromPlayer = distanceFromPlayer;
        }
    }
}
