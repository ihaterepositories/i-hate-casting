using Core.ItemSpawners;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.RoundControl
{
    public class RoundStarter : MonoBehaviour
    {
        [FormerlySerializedAs("defaultWeaponSpawner")] [SerializeField] private DefaultWeaponSpawner _defaultWeaponSpawner;

        private void Start()
        {
            _defaultWeaponSpawner.ShowSelection();
        }
    }
}