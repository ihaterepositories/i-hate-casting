using UnityEngine;
using UnityEngine.Serialization;

namespace Models.Creatures.Base
{
    public class Creature : MonoBehaviour
    {
        [FormerlySerializedAs("stats")] public CreatureStats _stats;
    }
}