using System.Collections.Generic;
using GDD.Item;

namespace GDD.Player.Inventory
{
    [System.Serializable]
    public class InventoryData
    {
        // 매우 중요!!!!!
        public int maxInventory = 4;
        
        public int rum;
        public int gold;
        public int coin;

        public ItemStack weapon;

        public List<ItemStack> slots;
    }
}