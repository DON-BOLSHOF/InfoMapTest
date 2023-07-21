using System.UserActivity;
using Cinemachine;
using UniRx;
using UnityEngine;
using Utils;
using Zenject;

namespace System
{
    public class CameraHandler: MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        [SerializeField] private PinchDetection _detection;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;

        [Inject] private AppStageHandler _appStageHandler;

        private Camera _mainCamera;

        private bool _isPossibleToHandle;

        private void Start()
        {
            _mainCamera = Camera.main;

            _appStageHandler.StageObservable
                .Subscribe(stage => _isPossibleToHandle = stage == AppStage.Map)
                .AddTo(this);

            _detection.OnMoved
                .Where(_ => _isPossibleToHandle)
                .Subscribe(MoveCamera)
                .AddTo(this);

            _detection.OnZoomed
                .Where(_ => _isPossibleToHandle)
                .Subscribe(ZoomCamera)
                .AddTo(this);
        }

        private void MoveCamera(Vector2 position)
        {
            _mainCamera.transform.position += Util.ToWorldPosition(_mainCamera, position).normalized * _speed;
        }

        private void ZoomCamera(PinchDetection.ZoomType type)
        {
            switch (type)
            {
                case PinchDetection.ZoomType.Approximation:
                    if (_virtualCamera.m_Lens.OrthographicSize - _speed > 170)
                        _virtualCamera.m_Lens.OrthographicSize -= _speed;
                    break;
                case PinchDetection.ZoomType.Removal:
                    if (_virtualCamera.m_Lens.OrthographicSize + _speed < 360)
                        _virtualCamera.m_Lens.OrthographicSize += _speed;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}