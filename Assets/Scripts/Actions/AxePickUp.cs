using Agent;

namespace DefaultNamespace
{
    public class AxePickUp : GameplayAction
    {
        public override void OnStartUse(AgentBehaviour _agent)
        {
            _agent.TryUseAnimation("PickupGround");
        }
        
        public override void OnEndUse(AgentBehaviour _agent)
        {
            gameObject.SetActive(false);
        }
    }
}