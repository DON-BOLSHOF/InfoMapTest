using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Utils
{
    public class Timer
    {
        public bool IsRunning => _timerState == ActiveTimer.Active;

        public IObservable<Unit> OnExpiredObservable => OnTimeExpired.AsObservable();

        private int _totalTime;
        private ActiveTimer _timerState = ActiveTimer.Deactive;
        private CancellationTokenSource _cancellationTokenSource;
        private ReactiveCommand OnTimeExpired = new();

        public Timer(int time)
        {
            _totalTime = time;
        }

        public async void ReloadTimer()
        {
            StopTimer();

            _timerState = ActiveTimer.Active;
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;

            try
            {
                await UniTask.WaitForSeconds(_totalTime, cancellationToken: token);
                
                OnTimeExpired?.Execute();
                _timerState = ActiveTimer.Deactive;
            }
            catch (OperationCanceledException ex)
            {
                if (ex.CancellationToken == token)
                {
                    return;
                }
            }
        }

        public void StopTimer()
        {
            try
            {
                if (_cancellationTokenSource == null || _cancellationTokenSource.IsCancellationRequested) return;
                _timerState = ActiveTimer.Deactive;
                _cancellationTokenSource?.Cancel();
                _cancellationTokenSource?.Dispose();
            }
            catch (ObjectDisposedException e)
            {
                Debug.LogWarning("The object is already disposed." + e);
            }
        }

        private enum ActiveTimer
        {
            Deactive,
            Active
        }
    }
}