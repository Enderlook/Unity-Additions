namespace Enderlook.Unity.Serializables.Atoms
{
    public abstract class AtomGetterWrapper<T, U> : AtomGetter<T, U> where T : IGet<U>
    {
        /// <inheritdoc />
        public override U Value => value.Value;
    }
}