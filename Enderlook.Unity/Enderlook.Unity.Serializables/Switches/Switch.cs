using Enderlook.Unity.Attributes;

using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables
{
    [Serializable, PropertyPopup(nameof(mode))]
    public abstract class Switch
    {
        protected const string WRONG_MODE = "Can't read property {0} because " + nameof(Mode) + " is {1}.";

        [SerializeField]
        private int mode = 1;

        /// <summary>
        /// Current mode.
        /// </summary>
        public int Mode => mode;
    }
}