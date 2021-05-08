using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PackageManager.Shared
{
    public static class TaskHelpers
    {
        public static Task GetTask(Action action)
        {
            if (action == null)
            {
                return Task.CompletedTask;
            }

            return Task.Run(action);
        }

        public static Task<TResult> GetTask<TResult>(Func<TResult> action)
        {
            if(action == null)
            {
                return Task.FromResult(default(TResult));
            }

            return Task.Run(action);
        }

        public static Task<TResult> GetTask<T, TResult>(Func<T, TResult> action, T parameter)
        {
            if(action == null)
            {
                return Task.FromResult(default(TResult));
            }

            return Task.Run(() => action.Invoke(parameter) );
        }

        public static Task GetTask<TParameter>(Action<TParameter> action, TParameter parameter)
        {
            if (action == null)
            {
                return Task.CompletedTask;
            }

            return Task.Run(() => action?.Invoke(parameter));
        }

        public static Task Invoke(this Func<Task> value, Action replacement)
        {
            var promptTask = value?.Invoke() 
                ?? GetTask(replacement)
                ?? Task.CompletedTask;

            return promptTask;
        }

        public static Task<TResult> Invoke<TResult>(this Func<Task<TResult>> value, Func<TResult> replacement)
        {
            var promptTask = value?.Invoke() 
                ?? GetTask(replacement)
                ?? Task.FromResult<TResult>(default);

            return promptTask;
        }

        public static Task<TResult> Invoke<TParameter, TResult>(this Func<TParameter, Task<TResult>> value, TParameter parameter, Func<TParameter, TResult> replacement)
        {
            var promptTask = value?.Invoke(parameter) 
                ?? GetTask(replacement, parameter) 
                ?? Task.FromResult<TResult>(default);

            return promptTask;
        }

        public static Task Invoke<TParameter>(this Func<TParameter, Task> value, TParameter parameter, Action<TParameter> replacement)
        {
            var promptTask = value?.Invoke(parameter) 
                ?? GetTask(replacement, parameter) ?? Task.CompletedTask;

            return promptTask;
        }
    }

    public static class ConsoleInputErrorLoopHandler
    {
        public static async Task<string> Begin(
            Action prompt = default,
            Func<string, bool> handler = default,
            Action onInitialHandler = default,
            Action<string> failedAttemptHandler = default,
            Action<bool> onFinalHandler = default,
            Func<Task> promptAsync = default,
            Func<string, Task<bool>> handlerAsync = default,
            Func<Task> onInitialHandlerAsync = default,
            Func<string, Task> failedAttemptHandlerAsync = default,
            Func<bool, Task> onFinalHandlerAsync = default,
            int? maximumAttempts = null,
            CancellationToken cancellationToken = default)
        {
            int attempts = 0;
            bool successful = false;
            string lastInput = string.Empty;
            while (cancellationToken.IsCancellationRequested 
                || attempts == 0 
                || maximumAttempts.HasValue && attempts++ < maximumAttempts.Value
                || !successful)
            {
                if(attempts == 0)
                {
                    onInitialHandlerAsync?.Invoke(onInitialHandler);
                }

                await promptAsync.Invoke(prompt);

                lastInput = Console.ReadLine();

                if (await handlerAsync.Invoke(lastInput, handler))
                {
                    successful = true;
                    break;
                }
                else
                {
                    await failedAttemptHandlerAsync.Invoke(lastInput, failedAttemptHandler);
                }
            }

            await onFinalHandlerAsync.Invoke(successful, onFinalHandler);
            return lastInput;
        }

        
    }
}
