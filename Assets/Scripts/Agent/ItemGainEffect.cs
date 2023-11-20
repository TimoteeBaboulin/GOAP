using UnityEngine;

namespace Agent
{
    [CreateAssetMenu(menuName = "GOAP/Effects/ItemGain", fileName ="New Item Gain Effect", order = 0)]
    public class ItemGainEffect : Effect
    {
        public Item Item;
        public int Amount;

        public override void Activate(IAgent agent)
        {
            if (!agent.Inventory.AddItem(Item, Amount))
                throw new System.Exception("Couldn't add " + Item.Name + " to the inventory");
        }
    }
}

