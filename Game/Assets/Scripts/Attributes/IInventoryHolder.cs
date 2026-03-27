namespace Items
{
    //this is for somebody who can have an inventory
    public interface IInventoryHolder
    {
        public Inventory GetInventoryReference();
        //pass what, where
        public void PassToInventory(ItemData item);
    }
}