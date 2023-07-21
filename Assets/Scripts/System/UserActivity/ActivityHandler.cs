using UniRx;
using UnityEngine;
using Utils;
using Zenject;

namespace System.UserActivity
{
    public class ActivityHandler : MonoBehaviour, IActivityStageVisitor
    {
        [SerializeField] private UserBeholder _userBeholder;
        [SerializeField] private int _secondsToUnderstandInactivity;

        [Inject] private ActivityStage _activityStage;

        private Timer _timer; 
        
        private void Start()
        {
            _timer = new(_secondsToUnderstandInactivity);

            _timer.OnExpiredObservable
                .Subscribe(_ => UserShowInactivity())
                .AddTo(this)
                ;
            
            _userBeholder.OnUserActivity
                .Subscribe(_ => UserShowActivity())
                .AddTo(this)
                ;
        }

        private void UserShowInactivity()
        {
            _timer.StopTimer();
            
            Accept(_activityStage, UserStage.Inactivity);
        }

        private void UserShowActivity()
        {
            _timer.ReloadTimer();
            
            Accept(_activityStage, UserStage.Activaty);
        }

        public void Accept(ActivityStage activityStage, UserStage userStage)
        {
            activityStage.Visit(this, userStage);
        }
    }
}