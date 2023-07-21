using System.UserActivity;
using Cinemachine;
using UI;
using UniRx;
using UnityEngine;
using Zenject;

namespace System
{
    public class Arbiter: MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private Describer _describer;

        [Inject] private ActivityStage _activityStage;

        private Camera _mainCamera;

        private Vector3 _startMousePosition;
        private float _startOrthographicSize;
        
        private void Start()
        {
            _mainCamera = Camera.main;
            _startMousePosition = _mainCamera.transform.position;

            _startOrthographicSize = _virtualCamera.m_Lens.OrthographicSize;
            
            _activityStage.OnStageChanged
                .Where(stage => stage == UserStage.Inactivity)
                .Subscribe(_ => ResetAllChanges())
                .AddTo(this)
                ;
        }

        private void ResetAllChanges()
        {
            _mainCamera.transform.position = _startMousePosition;
            _virtualCamera.m_Lens.OrthographicSize = _startOrthographicSize;

            _describer.Close();
        }
    }
}