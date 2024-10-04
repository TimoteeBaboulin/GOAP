using System;
using UnityEngine;

[Serializable]
public class ItemOwnedPrecondition : Prerequisite
{
    public Item Item;
    public int Amount;
    
    public ItemOwnedPrecondition()
    {
        Item = null;
        Amount = 0;
        Type = PrerequisiteType.HaveItem;
    }
    
    public ItemOwnedPrecondition(Item item, int amount)
    {
        Item = item;
        Amount = amount;

        Type = PrerequisiteType.HaveItem;
    }
    
    public override bool IsValid(Agent.IAgent agent)
    {
        return agent.Inventory.ContainsItem(Item, Amount);
    }
}
