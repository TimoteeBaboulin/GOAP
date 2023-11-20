using UnityEngine;

[CreateAssetMenu(menuName = "GOAP/Preconditions/ItemNotOwned", fileName ="New Item Not Owned Precondition", order = 1)]
public class ItemNotOwnedPrecondition : Prerequisite
{
    public Item Item;
    public int Amount;

    public override bool IsValid( Agent.IAgent agent)
    {
        return !agent.Inventory.ContainsItem(Item, Amount);
    }
}
