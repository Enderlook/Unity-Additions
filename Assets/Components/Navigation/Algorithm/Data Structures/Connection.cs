﻿using System;

using UnityEngine;

namespace Additions.Components.Navigation
{
    [Serializable]
    public class Connection : ScriptableObject
    {
        /// <summary>
        /// Starting node.
        /// </summary>
        public Node start;

        /// <summary>
        /// Ending node.
        /// </summary>
        public Node end;

        /// <summary>
        /// Distance between <see cref="start"/> and <see cref="end"/>.
        /// </summary>
        public float Distance => Vector2.Distance(start.position, end.position);

        /// <summary>
        /// Whenever it's active or not.
        /// </summary>
        [field: SerializeField]
        public bool IsActive { get; private set; }

        /// <summary>
        /// Set if this <see cref="Connection"/> is active or not.
        /// </summary>
        /// <param name="active">Whenever it's active or not.</param>
        public void SetActive(bool active) => IsActive = active;

        /// <summary>
        /// Whenever this <see cref="Connection"/> must be jumped.
        /// </summary>
        public bool IsExtreme => start.isExtreme && end.isExtreme;

        public override string ToString() => $"<start:{start}; end:{end}; active:{IsActive}; distance:{Distance}; extreme:{IsExtreme}";

        public string ToString(NavigationGraph navigationGraph) => $"<start:{start.ToString(navigationGraph)}; end:{end.ToString(navigationGraph)}; active:{IsActive}; distance:{Distance}; extreme:{IsExtreme}";

        /// <summary>
        /// Create a new <see cref="Connection"/>.
        /// </summary>
        /// <param name="start">From <see cref="Node"/>.</param>
        /// <param name="end">To <see cref="Node"/>.</param>
        /// <param name="isActive">Whenever it's active or not.</param>
        /// <returns>New <see cref="Connection"/>.</returns>
        public static Connection CreateConnection(Node start, Node end, bool isActive)
        {
            Connection connection = CreateInstance<Connection>();
            connection.start = start;
            connection.end = end;
            connection.IsActive = isActive;
            return connection;
        }

        /// <summary>
        /// Remove this <see cref="Connection"/> from <see cref="start"/>
        /// </summary>
        public void DisconnectFromNode() => start.RemoveConnection(this);

        /// <summary>
        /// Check if <paramref name="connection"/> is the opposite connection of this one.
        /// </summary>
        /// <param name="connection">Connection to check.</param>
        /// <returns>Whenever they are opposite connections or not.</returns>
        public bool AreOpposite(Connection connection) => start == connection.end && end == connection.start;
    }
}