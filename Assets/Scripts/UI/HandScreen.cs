using System;
using System.UserActivity;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI
{
    public class HandScreen : MonoBehaviour, IAppStageHandlerVisitor
    {
        [SerializeField] private Animator _animator;

        [Inject] private ActivityStage _activityStage;
        [Inject] private AppStageHandler _appStageHandler;
    
        private static readonly int FadeKey = Animator.StringToHash("Fade");
        private static readonly int ShineKey = Animator.StringToHash("Shine");

        private void Start()
        {
            _activityStage.OnStageChanged
                .Where(currentStage => currentStage == UserStage.Inactivity)
                .Skip(1)
                .Subscribe(_ =>
                {
                    ActivateScreen();
                    Accept(_appStageHandler, ElementState.Enter);
                    
                })
                .AddTo(this)
                ;

            _activityStage.OnStageChanged
                .Where(currentStage => currentStage == UserStage.Activaty)
                .Subscribe(_ =>
                {
                    DeactivateScreen();
                    Accept(_appStageHandler, ElementState.Exit);
                })
                .AddTo(this)
                ;
        }

        private void ActivateScreen()
        {
            _animator.SetTrigger(ShineKey);
        }

        private void DeactivateScreen()
        {
            _animator.SetTrigger(FadeKey);
        }

        public void Accept(AppStageHandler appStageHandler, ElementState elementState)
        {
            appStageHandler.Visit(this, elementState);
        }
    }
}