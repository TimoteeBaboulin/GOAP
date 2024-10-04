using System;
using UnityEngine;

[Serializable]
public class ItemNotOwnedPrecondition : Prerequisite
{
    public Item Item;
    public int Amount;

    public ItemNotOwnedPrecondition()
    {
        Item = null;
        Amount = 0;
        Type = PrerequisiteType.NotHaveItem;
    }
    
    public override bool IsValid( Agent.IAgent agent)
    {
        return !agent.Inventory.ContainsItem(Item, Amount);
    }
}
