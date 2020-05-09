using ItemData;
using Items.Inventory;

namespace ItemGeneration
{
    internal interface IItemGenerator
    {
        Equipment GenerateBonuses(EquipmentDefinition definition, RarityModifier rarity);
    }
}
