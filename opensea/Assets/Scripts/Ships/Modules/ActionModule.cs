namespace Assets.Scripts.Ships.Modules
{
    public abstract class ActionModule : Module
    {
        protected GameInputs m_inputActions;
        
        public override void Initialize(Ship attachedShip)
        {
            base.Initialize(attachedShip);
            
            m_inputActions = new GameInputs();
        }

        public override void ShipSelect()
        {
            m_inputActions.Enable();
        }

        public override void ShipDeselect()
        {
            m_inputActions.Disable();
        }

        protected override void InternalPreUpdateModule(float deltaTime)
        {
            
        }

        protected override void InternalUpdateModule(float deltaTime)
        {
            
        }
    }
}