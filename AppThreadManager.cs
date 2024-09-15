using System;

namespace Dsi.MultiThreadManager;
/// <summary>
/// The AppThreadManaager will execute all the classes that inherit 
/// the AppThread class asynchrnously and in a priority sequence. 
/// 
/// The Priority is an integer and lower number are executed
/// first. The default priority is 5 so priorities less than 5 will be executed
/// before priorities greater than or equal to 5.
/// 
/// Class instances must be added to the priority queue using the Add() or
/// AddRange() methods. After all instances are loaded, the Execute() method
/// shoule be called to being running all the tasks.
/// </summary>
public static class AppThreadManager
{
    public static PriorityQueue<AppThread, int> PriorityAppThreads = new();
    public static List<Task> Tasks { get; set; } = new List<Task>();

    public static void Add(AppThread appThread) {
        PriorityAppThreads.Enqueue(appThread, appThread.Priority);
    }
    public static void AddRange(params AppThread[] appThreads) {
        foreach (AppThread appThread in appThreads) {
            Add(appThread);
        }
    }

    public static void Execute() {
        DateTime start = DateTime.Now;
        var count = PriorityAppThreads.Count;
        for(var idx = 0; idx < count; idx++) {
            var appThread = PriorityAppThreads.Dequeue();
            Task task = new Task(() => appThread.ExecAppThreadAsync());
            Tasks.Add(task);
            task.Start();
        }
        Task.WaitAll(Tasks.ToArray());
        TimeSpan ts = DateTime.Now - start;
        Log($"All tasks completed in {ts:c}");
    }

    public static void Log(string message) {
        Console.WriteLine($"AppThreadMgr: {message}");
    }

}
