using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI
{
    public class Describer : MonoBehaviour, IAppStageHandlerVisitor
    {
        [SerializeField] private DescriptionPanel _descriptionPanel;
        [SerializeField] private Animator _animator;

        [Inject] private AppStageHandler _appStageHandler;

        private static readonly int ShineKey = Animator.StringToHash("Shine");
        private static readonly int FadeKey = Animator.StringToHash("Fade");

        public void Init(List<IObservable<InfoPoint>> infoPointsClick)
        {
            foreach (var infoPointClick in infoPointsClick)
            {
                infoPointClick
                    .Subscribe(infoPoint =>
                    {
                        ShowDescription(infoPoint);
                        Accept(_appStageHandler, ElementState.Enter);
                    })
                    .AddTo(this)
                    ;
            }
        }

        private void ShowDescription(InfoPoint infoPoint)
        {
            _descriptionPanel.Show(infoPoint);
            _animator.SetTrigger(ShineKey);
        }

        public void Close()
        {
            if (!_descriptionPanel.gameObject.activeInHierarchy) return;

            Accept(_appStageHandler, ElementState.Exit);
            _descriptionPanel.Close();
            _animator.SetTrigger(FadeKey);
        }

        public void Accept(AppStageHandler appStageHandler, ElementState state)
        {
            appStageHandler.Visit(this, state);
        }
    }
}