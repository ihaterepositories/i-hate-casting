using UnityEngine;

namespace Models.WorldObjects.Creatures.PlayerImpl.DataContainers
{
    public class PlayerPositionTracker : MonoBehaviour
    {
        public Vector3 Position => transform.position;
    }
}