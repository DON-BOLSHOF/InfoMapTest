using UniRx;
using UnityEngine;

namespace System.UserActivity
{
    public class UserBeholder : MonoBehaviour
    {
        private TouchControll _controlls;

        public ReactiveCommand OnUserActivity = new();

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
            _controlls.Touch.PrimaryTouchContact.started += _ => Activity();
        }

        [ContextMenu("ShowActivity")]
        private void Activity()
        {
            OnUserActivity?.Execute();
        }
    }
}