using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cardinal.Utils.Extensions
{
    /// <summary>
    /// Extensões para <see cref="Task"/>
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Extensão para obter resultado assíncrono de uma
        /// task.
        /// </summary>
        /// <typeparam name="T">Tipo do resultado da Task.</typeparam>
        /// <param name="task">Objeto referenciado.</param>
        /// <param name="onSuccess">Ação executada quando a task é concluida com sucesso.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Objeto referenciado.</returns>
        public static Task Result<T>(this Task<T> task, Action<T> onSuccess, CancellationToken cancellationToken = default)
        {
            return task.ContinueWith(x =>
            {
                if (x.IsCompletedSuccessfully)
                {
                    onSuccess.Invoke(x.Result);
                }
            }, cancellationToken);
        }

        /// <summary>
        /// Extensão para obter resultado assíncrono de uma
        /// task.
        /// </summary>
        /// <typeparam name="T">Tipo do resultado da Task.</typeparam>
        /// <param name="task">Objeto referenciado.</param>
        /// <param name="onSuccess">Ação executada quando a task é concluida com sucesso.</param>
        /// <param name="onFail">Ação executada quando a task é concluída com falha.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Objeto referenciado.</returns>
        public static Task Result<T>(this Task<T> task, Action<T> onSuccess, Action<Exception> onFail, CancellationToken cancellationToken = default)
        {
            return task.ContinueWith(x =>
            {
                if (x.IsCompletedSuccessfully)
                {
                    onSuccess.Invoke(x.Result);
                }
                else if (x.IsFaulted)
                {
                    onFail.Invoke(x.Exception);
                }
            }, cancellationToken);
        }

        /// <summary>
        /// Extensão para obter resultado assíncrono de uma
        /// task.
        /// </summary>
        /// <typeparam name="T">Tipo do resultado da Task.</typeparam>
        /// <param name="task">Objeto referenciado.</param>
        /// <param name="onSuccess">Ação executada quando a task é concluida com sucesso.</param>
        /// <param name="onFail">Ação executada quando a task é concluída com falha.</param>
        /// <param name="onCancel">Ação executada quando a task é cancelada.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Objeto referenciado.</returns>
        public static Task Result<T>(this Task<T> task, Action<T> onSuccess, Action<Exception> onFail, Action<TaskStatus> onCancel, CancellationToken cancellationToken = default)
        {
            return task.ContinueWith(x =>
            {
                if (x.IsCompletedSuccessfully)
                {
                    onSuccess.Invoke(x.Result);
                }
                else if (x.IsFaulted)
                {
                    onFail.Invoke(x.Exception);
                }
                else if (x.IsCanceled)
                {
                    onCancel.Invoke(x.Status);
                }

            }, cancellationToken);
        }
    }
}
