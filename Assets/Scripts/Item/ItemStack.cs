using System;

namespace GDD.Item
{
    [Serializable]
    public class ItemStack
    {
        public ItemData itemData;
        public int amount;

        public ItemStack(ItemData data, int amount = 1)
        {
            this.itemData = data;
            this.amount = amount;
        }
        
        public bool AddAmount(int value)
        {
            if (amount + value > itemData.maxStackSize) return false;
            amount += value;
            return true;
        }
    }
}