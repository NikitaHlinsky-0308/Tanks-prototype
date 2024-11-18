namespace CodeBase.Interfaces
{
    public interface IStates
    {
       

        void TakeDamage(int amount);

        void Heal(int amount);

        void Died();

    }
}