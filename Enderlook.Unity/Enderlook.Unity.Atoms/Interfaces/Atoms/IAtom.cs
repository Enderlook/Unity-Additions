namespace Enderlook.Unity.Atoms
{
    public interface IAtom
    {
#if UNITY_EDITOR
        /// <summary>
        /// Description of this <see cref="Atom"/>. Only use inside Unity Editor.
        /// </summary>
        string DeveloperDescription { get; }
#endif
    }
}
