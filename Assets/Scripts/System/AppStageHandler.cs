using UI;
using UniRx;

namespace System
{
    public class AppStageHandler
    {
        private ReactiveProperty<AppStage> _appStage = new();

        public IObservable<AppStage> StageObservable => _appStage.AsObservable();

        public void Visit(Describer visitor, ElementState elementState)
        {
            _appStage.Value = elementState == ElementState.Enter ? AppStage.Description : AppStage.Map;
        }

        public void Visit(HandScreen handScreen, ElementState elementState)
        {
            _appStage.Value = elementState == ElementState.Enter ? AppStage.HandScreen : AppStage.Map;
        }
    }

    public enum AppStage
    {
        HandScreen,
        Map,
        Description
    }

    public enum ElementState
    {
        Enter,
        Exit
    }
}