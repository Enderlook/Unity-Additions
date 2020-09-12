using System;

using UnityEngine;

namespace Enderlook.Unity.Navigation.D2
{
    /// <inheritdoc cref="RootSerializer"/>
    [Serializable]
    public class RootSerializer : RootSerializer<Node, Edge, Vector2, Unit>
    {
        protected override void Deserialize(Unit data, Edge edge) { }

        protected override Node Deserialize(Vector2 nodePayload) => new Node(nodePayload);

        protected override Unit Serialize(Edge edge) => default;

        protected override Vector2 Serialize(Node node) => node.Position;
    }
}