using Core.Input.InputHandlers;
using Core.Input.Interfaces;
using Zenject;

namespace Core.Infrastructure
{
    public class InputHandlerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputHandler>().To<KeyboardInputHandler>().FromComponentInHierarchy().AsSingle();
        }
    }
}