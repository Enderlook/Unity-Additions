using System;
using System.Linq;

using UnityEngine;

using Random = UnityEngine.Random;

namespace Additions.Serializables
{
    [Serializable]
    public class WeightedElements<T, U> where T : WeightedElement<U>
    {
#pragma warning disable CS0649
        [SerializeField, Tooltip("Possible elements to get and their weights.")]
        private T[] weightedElements;

        [SerializeField, Tooltip("Weight change of not getting anything.")]
        private float emptyWeight;
#pragma warning restore CS0649
        [NonSerialized]
        private float? totalWeight;

        private float TotalWeight => totalWeight ?? (totalWeight = emptyWeight + weightedElements.Sum(e => e.Weight)).Value;

        public bool TryGetRandomElement(out U result)
        {
            float value = Random.Range(0, TotalWeight);
            float accumulated = 0;
            foreach (T element in weightedElements)
            {
                accumulated += element.Weight;
                if (accumulated >= value)
                {
                    result = element.Element;
                    return true;
                }
            }
            result = default;
            return false;
        }
    }

    [Serializable]
    public class WeightedElement<T>
    {
#pragma warning disable CS0649
        [SerializeField]
        private T element;
#pragma warning restore CS0649
        public T Element => element;

        [SerializeField]
        private float weight = 1;

        public float Weight => weight;
    }
}