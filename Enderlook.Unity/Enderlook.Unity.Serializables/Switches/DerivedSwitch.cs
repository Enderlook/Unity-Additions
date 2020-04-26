using Enderlook.Unity.Attributes;
using Enderlook.Unity.Utils.Interfaces;

using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables
{
    [Serializable, PropertyPopup(nameof(mode))]
    public abstract class DerivedSwitch<T> : IGet<T>
    {
        [SerializeField]
        private int mode = 1;

        /// <summary>
        /// Current mode.
        /// </summary>
        protected int Mode => mode;

        /// <inheritdoc cref="IGet{T}.GetValue"/>
        public abstract T GetValue();
    }
}