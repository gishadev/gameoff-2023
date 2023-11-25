namespace gameoff.Enemy.States
{
    public class PrepareMeleeAttack : StateWithElapsedTime
    {
        public override void Tick()
        {
        }

        public override void OnEnter()
        {
            SetStartTime();
        }

        public override void OnExit()
        {
        }
    }
}