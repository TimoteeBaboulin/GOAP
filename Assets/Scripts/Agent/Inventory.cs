using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityInventory
{
    private Dictionary<Item, int> _inventory = new();
    public Dictionary<Item, int> Inventory => _inventory;

    public bool AddItem(Item item, int amount = 1)
    {
        if (amount <= 0) return false;

        if (_inventory.ContainsKey(item))
            _inventory[item] += amount;
        else
            _inventory.Add(item, amount);

        return true;
    }

    public bool RemoveItem(Item item, int amount = 1)
    {
        if (amount <= 0) return false;

        if (Inventory.ContainsKey(item))
        {
            if (Inventory[item] < amount)
                return false;

            if (Inventory[item] == amount)
                Inventory.Remove(item);
            else if (Inventory[item] > amount)
                Inventory[item] -= amount;

            return true;
        }
        return false;
    }

    public bool ContainsItem(Item item, int amount = 1)
    {
        if (amount <= 0) return false;

        if (Inventory.ContainsKey(item) && Inventory[item] >= amount)
            return true;

        return false;
    }

    public bool ContainsItem(string name, int amount = 1) 
    {
        if (amount <= 0) return false;

       

        if (Inventory.Keys.Any(item => item.name == name))
        {
            Item item = Inventory.Keys.First(obj => obj.name == name);

            if (Inventory[item] < amount) return false;
            return true;
        }
        else return false;
    }

    public EntityInventory()
    {
        _inventory = new();
    }

    public EntityInventory(EntityInventory inventory)
    {
        _inventory = new Dictionary<Item, int>(inventory._inventory);
    }
}
