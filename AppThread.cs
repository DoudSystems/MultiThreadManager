using System;

namespace Dsi.MultiThreadManager;

/// <summary>
/// The AppThread is an abstract class that must be inherited
/// by any class that wants to be executed by the AppThreadManager.
/// 
/// Classes inherting AppThread MUST implement the ExecAppThreadAsync()
/// method as that is the method called by the AppThreadManager for
/// execution asynchronously.
/// 
/// The 'TaskName' is an user defined identifier for the task used when
/// log messages are displayed.
/// 
/// The 'StartTimer()' and 'FinishTimer()' can be used to measure the 
/// amount of time the particular task executes.
/// </summary>
public abstract class AppThread {

    public string TaskName { get; set; } = default!;
    public int Priority { get; set; } = 5;
    public object? ReturnResult = null;
    private DateTime Start { get; set; } = default!;
    private DateTime Finish { get; set; } = default!;

    public abstract void ExecAppThreadAsync();

    public virtual void StartTimer() {
        Start = GetDateTime();
        Log($"Task {TaskName} started ...");
    }
    public virtual void FinishTimer() {
        Finish = GetDateTime();
        Log($"Task {TaskName} finished  ...");
        TimeSpan ts = Finish - Start;
        Log($"Task {TaskName} ran in {ts:c} ...");
    }
    private DateTime GetDateTime() {
        return DateTime.Now;
    }

    private void Log(string message) {
        AppThreadManager.Log(message);
    }

}