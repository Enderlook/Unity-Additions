namespace Enderlook.Unity.Serializables.Atoms
{
    public interface IReseteableWithEvent : IReseteable
    {
        void Reset(bool shouldExecuteEvent = false);
    }
}