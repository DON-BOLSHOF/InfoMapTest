using System;
using System.UserActivity;
using Zenject;

namespace Installers
{
    public class ActivityStageInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ActivityStage>().FromNew().AsSingle();
        }
    }
}