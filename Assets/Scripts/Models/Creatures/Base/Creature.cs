using UnityEngine;
using UnityEngine.Serialization;

namespace Models.Creatures.Base
{
    public class Creature : MonoBehaviour
    {
        [FormerlySerializedAs("_stats")] [FormerlySerializedAs("stats")] public CreatureStats Stats;
    }
}