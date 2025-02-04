using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

//This class is just to explain how to use CancellationToken in C#.
namespace BasketContext.Application.Abstractions
{
    internal class UsingCancellationToken
    {
        //Pass the CancellationToken to an asynchronous task.
        //Check for cancellation within the task.
        //Handle the cancellation if it occurs.
        public async Task MyAsyncMethod1(CancellationToken cancellationToken)
        {
            while (true)
            {
                // Perform some work
                await Task.Delay(1000, cancellationToken);

                // Check for cancellation
                if (cancellationToken.IsCancellationRequested)
                {
                    // Perform any necessary cleanup
                    Console.WriteLine("Operation cancelled.");
                    break;
                }
            }
        }

        //Use ThrowIfCancellationRequested to throw an exception if cancellation is requested.
        public async Task MyAsyncMethod2(CancellationToken cancellationToken)
        {
            while (true)
            {
                // Perform some work
                await Task.Delay(1000, cancellationToken);

                // This will throw OperationCanceledException if cancellation is requested
                cancellationToken.ThrowIfCancellationRequested();
            }
        }

        //Pass the same CancellationToken to multiple tasks to cancel all of them simultaneously.
        public async Task MyAsyncMethod3(CancellationToken cancellationToken)
        {
            var task1 = Task.Run(() => DoWork(cancellationToken), cancellationToken);
            var task2 = Task.Run(() => DoMoreWork(cancellationToken), cancellationToken);

            await Task.WhenAll(task1, task2);
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            await Task.Delay(1000, cancellationToken);
        }

        public async Task DoMoreWork(CancellationToken cancellationToken)
        {
            await Task.Delay(2000, cancellationToken);
        }


        //Create a CancellationTokenSource to manually trigger cancellation.
        public async Task RunTasksWithCancellation()
        {
            var cts = new CancellationTokenSource();
            var task = MyAsyncMethod1(cts.Token);

            // Simulate some condition to cancel the task
            await Task.Delay(5000);
            cts.Cancel();

            try
            {
                await task;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Task was cancelled.");
            }
        }

    }
}
