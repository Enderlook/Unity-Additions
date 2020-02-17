namespace Enderlook.Unity.Serializables.Atoms
{
    public interface IGetSet : IGet
    {
        new object ObjectValue { set; }
    }
}