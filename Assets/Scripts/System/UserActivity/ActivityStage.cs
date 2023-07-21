using UniRx;

namespace System.UserActivity
{
    public class ActivityStage
    {
        private ReactiveProperty<UserStage> Stage = new(UserStage.Inactivity);
        public IObservable<UserStage> OnStageChanged;

        public ActivityStage()
        {
            OnStageChanged = Stage.AsObservable();
        }
        
        public void Visit(IActivityStageVisitor stageVisitor, UserStage userStage)
        {
            Stage.Value = userStage;
        }
    }

    public enum UserStage
    {
        Inactivity,
        Activaty
    }
}