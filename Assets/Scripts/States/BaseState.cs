namespace States
{
    public interface IState
    {
        public void Execute();
    }

    public abstract class State : IState
    {
        public abstract void Execute();
    }
}
