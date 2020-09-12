using System;

using UnityEngine;

namespace Enderlook.Unity.Navigation
{
    [Serializable]
    internal struct Pair
    {
        [SerializeField]
        public int from;

        [SerializeField]
        public int to;

        public Pair(int from, int to)
        {
            this.from = from;
            this.to = to;
        }
    }
}