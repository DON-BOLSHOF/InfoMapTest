using System.Collections;
using UniRx;
using UnityEngine;

namespace System.UserActivity
{
    public class PinchDetection : MonoBehaviour
    {
        private TouchControll _controlls;

        private Coroutine _moveCoroutine;
        private Coroutine _zoomCoroutine;

        public ReactiveCommand<Vector2> OnMoved = new();
        public ReactiveCommand<ZoomType> OnZoomed = new();

        private void Awake()
        {
            _controlls = new TouchControll();
        }

        private void OnEnable()
        {
            _controlls.Enable();
        }

        private void OnDisable()
        {
            _controlls.Disable();
        }

        private void Start()
        {
            _controlls.Touch.PrimaryTouchContact.started += _=> StartMove();
            _controlls.Touch.PrimaryTouchContact.canceled += _ => StopMove();
            
            _controlls.Touch.SecondaryTouchContact.started += _ => ZoomStart();
            _controlls.Touch.SecondaryTouchContact.canceled += _ => ZoomEnd();
        }

        private void StartMove()
        {
            _moveCoroutine = StartCoroutine(Move());
        }

        private void StopMove()
        {
            StopCoroutine(_moveCoroutine);
        }

        private IEnumerator Move()
        {
            var previousPosition = _controlls.Touch.PrimaryFingerPosition.ReadValue<Vector2>();

            while (_controlls.Touch.PrimaryTouchContact.IsPressed())
            {
                var currentPosition = _controlls.Touch.PrimaryFingerPosition.ReadValue<Vector2>();
                var distance = Vector2.Distance(currentPosition,
                    previousPosition);

                if (distance > 0.09f)
                {
                    OnMoved?.Execute(currentPosition);
                    
                    previousPosition = currentPosition;
                }
                
                yield return new WaitForSeconds(0.07f);
            }
        }

        private void ZoomStart()
        {
            _zoomCoroutine = StartCoroutine(ZoomDetection());
        }

        private void ZoomEnd()
        {
            StopCoroutine(_zoomCoroutine);
        }

        private IEnumerator ZoomDetection()
        {
            float previousDistance = 0f;

            while (true)
            {
                var distance = Vector2.Distance(_controlls.Touch.PrimaryFingerPosition.ReadValue<Vector2>()
                    , _controlls.Touch.SecondaryFingerPosition.ReadValue<Vector2>());

                if (distance > previousDistance)
                {
                    OnZoomed?.Execute(ZoomType.Removal);
                }
                else if (distance < previousDistance)
                {
                    OnZoomed?.Execute(ZoomType.Approximation);
                }

                previousDistance = distance;
                yield return new WaitForSeconds(0.07f);
            }
        }
        
        public enum ZoomType
        {
            Approximation,
            Removal
        }
    }
}