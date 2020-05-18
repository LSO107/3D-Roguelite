using UnityEngine;

namespace Shops
{
    internal abstract class Shop : MonoBehaviour
    {
        public abstract void PurchaseItem(int slotIndex);
    }
}
