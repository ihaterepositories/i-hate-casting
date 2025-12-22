using System;
using Systems.ResourcesCleaning.Interfaces;

namespace Models.UI.StatusTexts.Services.ValueProviding.Interfaces
{
    public interface IStatusTextValueProvideService : IResourceCleanable
    {
        public event Action OnValueChanged;
        public string GetValueForText();
    }
}