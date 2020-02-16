namespace Enderlook.Unity.Serializables.Atoms
{
    public interface IGet<T> : IGet
    {
        T Value { get; }
    }
}