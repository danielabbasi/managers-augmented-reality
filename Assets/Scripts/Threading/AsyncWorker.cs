using System;
using System.ComponentModel;
using System.Threading;
using Assets.Scripts.Exceptions;
using JetBrains.Annotations;

public sealed class AsyncWorker<TCallbackType>
    where TCallbackType : class
{
    static AsyncWorker()
    {
        MainThreadId = Thread.CurrentThread.ManagedThreadId;
    }

    private AsyncWorker(Action<AsyncWorker<TCallbackType>> execute, Action<TCallbackType> callback)
    {
        if (execute == null) throw new ArgumentNullException("execute");

        if (callback == null) throw new ArgumentNullException("callback");

        UnderlyingBackgroundWorker = new BackgroundWorker
        {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true
        };

        UnderlyingBackgroundWorker.DoWork += (sender, eventArgs) => execute.Invoke(this);
        UnderlyingBackgroundWorker.ProgressChanged += (sender, eventArgs) => callback.Invoke((TCallbackType)eventArgs.UserState);
    }

    #region Variables



    #endregion

    #region Properties

    private static int MainThreadId { get; set; }

    private BackgroundWorker UnderlyingBackgroundWorker { get; set; }

    #endregion

    #region Methods

    public static AsyncWorker<TCallbackType> Dispatch(Action<AsyncWorker<TCallbackType>> execute, Action<TCallbackType> callback)
    {
        if (execute == null) throw new ArgumentNullException("execute");

        if (callback == null) throw new ArgumentNullException("callback");

        return new AsyncWorker<TCallbackType>(execute, callback);
    }

    public void ReportProgress(TCallbackType callback)
    {
        UnderlyingBackgroundWorker.ReportProgress(0, callback);
    }

    public void RunAsync()
    {
        if (Thread.CurrentThread.ManagedThreadId != MainThreadId)
        {
            throw new InvalidThreadException();
        }

        UnderlyingBackgroundWorker.RunWorkerAsync();
    }

    #endregion
}