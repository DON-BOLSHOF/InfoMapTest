namespace System.UserActivity
{
    public interface IActivityStageVisitor
    {
        void Accept(ActivityStage activityStage, UserStage userStage);
    }
}