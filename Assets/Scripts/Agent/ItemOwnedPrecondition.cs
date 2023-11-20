using UnityEngine;

[CreateAssetMenu(menuName = "GOAP/Preconditions/ItemOwned", fileName = "New Item Owned Precondition", order = 0)]
public class ItemOwnedPrecondition : Prerequisite
{
    public Item Item;
    public int Amount;

    public override bool IsValid(Agent.IAgent agent)
    {
        return agent.Inventory.ContainsItem(Item, Amount);
    }
}
