using Player.Moving.InputHandlers;
using Player.Moving.Interfaces;
using Zenject;

namespace Core.Infrastructure
{
    public class InputHandlerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputHandler>().To<MobileJoystickInputHandler>().FromComponentInHierarchy().AsSingle();
        }
    }
}