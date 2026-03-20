
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class Inventory
    {
        private Dictionary<ItemData, int> items  = new();
        
        public event Action<ItemData, int> OnInventoryChanged;
        
        public void Add(ItemData item)
        {
            if(items.ContainsKey(item)) items[item]++;
            else items.Add(item, 1);
            Debug.Log("added " +items[item] +" " + item + " with name " + item.ItemName);
            
            //for quest and UI systems
            OnInventoryChanged?.Invoke(item, items[item]);
        }

        //this method currently removes one item from inventory
        public void Remove(ItemData item)
        {
            if (items.ContainsKey(item))
            {
                if (items[item] > 1)
                {
                    items[item]--;
                }
                else
                {
                    items.Remove(item);
                }
            }
            Debug.Log("removed 1 " + item + " with name " + item.ItemName);

            //if there are no items of such type left in inventory, return 0
            OnInventoryChanged?.Invoke(item, items.ContainsKey(item) ? items[item]:0);
        }
    }
}