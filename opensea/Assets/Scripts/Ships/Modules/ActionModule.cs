namespace Assets.Scripts.Ships.Modules
{
    public abstract class ActionModule : Module
    {
        protected GameInputs m_inputActions;
        
        public override void Initialize(Ship attachedShip)
        {
            base.Initialize(attachedShip);
            
            m_inputActions = new GameInputs();
            RegisterActions();
        }

        public override void ShipSelect()
        {
            m_inputActions.Enable();
        }

        public override void ShipDeselect()
        {
            m_inputActions.Disable();
        }

        protected abstract void RegisterActions();

        protected override void InternalPreUpdateModule(float deltaTime)
        {
            
        }

        protected override void InternalUpdateModule(float deltaTime)
        {
            
        }
    }
}