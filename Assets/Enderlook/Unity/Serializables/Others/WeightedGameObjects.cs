using System;

using UnityEngine;

namespace Enderlook.Unity.Serializables
{
    [Serializable]
    public class WeightedGameObjects : WeightedElements<WeightedGameObject, GameObject> { }

    [Serializable]
    public class WeightedGameObject : WeightedElement<GameObject> { }
}
