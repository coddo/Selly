using System;
using System.Threading;
using System.Threading.Tasks;

namespace Selly.BusinessLogic.Utility
{
    public class Timer
    {
        private readonly TimeSpan mInterval;
        private bool mIsTimerRunning;
        private CancellationTokenSource mCancellationTokenSource;
        private readonly Func<Task> mTimerCallback;

        public Timer(TimeSpan timerInterval, Func<Task> timerCallback)
        {
            mInterval = timerInterval;
            mTimerCallback = timerCallback;
        }

        public bool IsBusy { get; private set; }

        public void Start()
        {
            if (mIsTimerRunning)
            {
                return;
            }

            mCancellationTokenSource = new CancellationTokenSource();
            mIsTimerRunning = true;

            RunTimer().ConfigureAwait(false);
        }

        public void Stop()
        {
            if (!mIsTimerRunning)
            {
                return;
            }

            mCancellationTokenSource.Cancel();
            mCancellationTokenSource.Dispose();
        }

        public void Reset()
        {
            Stop();

            Task.Run(() =>
            {
                while (mIsTimerRunning)
                {
                    Task.Delay(1000).ConfigureAwait(false);
                }
            }).ConfigureAwait(false).GetAwaiter().OnCompleted(Start);
        }

        private async Task RunTimer()
        {
            var cancellationToken = mCancellationTokenSource.Token;

            while (!cancellationToken.IsCancellationRequested)
            {
                IsBusy = true;
                if (!cancellationToken.IsCancellationRequested)
                {
                    await mTimerCallback.Invoke().ConfigureAwait(false);
                }
                IsBusy = false;

                await Task.Delay(mInterval, cancellationToken).ContinueWith(task => { }).ConfigureAwait(false);
            }

            mIsTimerRunning = false;
        }
    }
}