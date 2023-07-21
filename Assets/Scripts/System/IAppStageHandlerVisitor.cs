namespace System
{
    public interface IAppStageHandlerVisitor
    {
        void Accept(AppStageHandler appStageHandler, ElementState elementState);
    }
}