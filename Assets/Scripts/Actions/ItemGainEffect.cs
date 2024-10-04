using UnityEngine;

namespace Agent
{
    [CreateAssetMenu(menuName = "GOAP/Effects/ItemGain", fileName ="New Item Gain Effect", order = 0)]
    public class ItemGainEffect : Effect
    {
        public Item Item;
        public int Amount;

        public override bool Activate(IAgent agent)
        {
            if (Amount > 0)
            {
                return agent.Inventory.AddItem(Item, Amount);
            }
            else
            {
                return !agent.Inventory.RemoveItem(Item, -Amount);
            }
            
        }
    }
}

