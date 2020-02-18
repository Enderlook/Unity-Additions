﻿namespace Enderlook.Unity.Serializables.Atoms
{
    public interface IGetSet<T> : IGet<T>, IGetSet
    {
        new T Value { get; set; }
    }
}