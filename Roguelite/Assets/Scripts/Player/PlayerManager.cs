using System.Collections.Generic;
using Items.Definitions;
using Items.Inventory;
using UnityEngine;

namespace Player
{
    internal sealed class PlayerManager : MonoBehaviour
    {
        public PlayerInventory PlayerInventory;

        private void Start()
        {
            var inventory = new List<Item>();
            PlayerInventory = new PlayerInventory(inventory);
        }
    }
}
