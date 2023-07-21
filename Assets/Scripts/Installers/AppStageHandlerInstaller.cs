using System;
using Zenject;

namespace Installers
{
    public class AppStageHandlerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<AppStageHandler>().FromNew().AsSingle();
        }
    }
}