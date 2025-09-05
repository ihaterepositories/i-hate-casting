using UnityEngine;

namespace Models.Items.Artefacts.Modifiers.Base
{
    public abstract class Modifier : MonoBehaviour
    {
        private void OnEnable()
        {
            ActivateModifier();
        }

        private void OnDisable()
        {
            DeactivateModifier();
        }
        
        protected abstract void ActivateModifier();
        protected abstract void DeactivateModifier();
    }
}