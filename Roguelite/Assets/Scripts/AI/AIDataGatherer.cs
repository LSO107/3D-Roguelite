using System.Collections.Generic;
using System.Linq;
using Character.Health;
using Items.Inventory;
using Player;
using UnityEngine;

namespace AI
{
    internal static class AiDataGatherer
    {
        private static PlayerHealth PlayerHealth => PlayerManager.Instance.Health;
        private static Vector3 PlayerLocation => PlayerManager.Instance.transform.position;
        private static PlayerEquipment PlayerEquipment => PlayerManager.Instance.Equipment;

        public static AiDataObject GetData(Transform requester)
        {
            // Get Enemy health data, how much lost in last X seconds? Current health?
            // Time since last player attack?
            // How long has player been blocking for?

            var distance = GetDistance(requester.position);
            var colliders = GetSurroundingTransforms(requester.position);
            return new AiDataObject(PlayerHealth.CurrentHealth, PlayerLocation, distance, colliders);
        }

        private static IEnumerable<Transform> GetSurroundingTransforms(Vector3 requesterPosition)
        { 
            var colliders = Physics.OverlapSphere(requesterPosition, 5, 256);
            
            return colliders.Select(c => c.transform);
        }

        private static float GetDistance(Vector3 requesterPosition)
        {
            return Vector3.Distance(requesterPosition, PlayerLocation);
        }
    }
}
