using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Core.Jobs
{
    public abstract class BaseJob : IBaseJob
    {
        private Task _task;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly TimeSpan _delay;

        protected BaseJob(TimeSpan delay)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _delay = delay;
        }

        public void Start()
        {
            if (_task != null && _task.Status == TaskStatus.Running) throw new Exception();

            _task = Task.Factory.StartNew(RunLoop, _cancellationTokenSource.Token, TaskCreationOptions.LongRunning,
                TaskScheduler.Default);
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }
        
        protected abstract Task ExecuteAsync();

        private async Task RunLoop()
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                await ExecuteAsync();
                await Task.Delay(_delay);
            }
        }

    }
}