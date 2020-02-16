﻿namespace Enderlook.Unity.Serializables.Atoms
{
    public abstract class AtomVariable<T> : AtomConstant<T>, IGetSet<T>
    {
        /// <inheritdoc cref="AtomConstant{T}.Value"/>
        public new T Value {
            get => value;
            set => this.value = value;
        }

        /// <inheritdoc cref="AtomGet{T}.ObjectValue"/>
        public new object ObjectValue {
            get => Value;
            set => Value = (T)value;
        }
    }
}