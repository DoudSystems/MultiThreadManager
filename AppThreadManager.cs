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
    private static string AppThreadManagerLogFile = "AppThreadManager.log";
    private static string DateTimeFormat = "yyyy-MMM-dd hh:mm:ss";
    private static DateTime Start = default!;

    public static PriorityQueue<AppThread, int> PriorityAppThreads = new();
    public static List<AppThread> AppThreads = new();
    public static List<Task> Tasks { get; set; } = new();
    private static List<string> Messages = new();

    public static void Add(AppThread appThread) {
        AppThreads.Add(appThread);
        PriorityAppThreads.Enqueue(appThread, appThread.Priority);
    }
    public static void AddRange(params AppThread[] appThreads) {
        foreach (AppThread appThread in appThreads) {
            Add(appThread);
        }
    }

    public static void Execute(bool RunSynchronous = false) {
        if(RunSynchronous) 
            Log("Running synchronous ..."); 
        else 
            Log("Running asynchronous ...");
        Start = DateTime.Now;
        var count = PriorityAppThreads.Count;
        for(var idx = 0; idx < count; idx++) {
            var appThread = PriorityAppThreads.Dequeue();
            Task task = new Task(() => appThread.ExecAppThreadAsync());
            Tasks.Add(task);
            if(RunSynchronous) {
                task.RunSynchronously();
            } else {
                task.Start();
            }
        }
        Task.WaitAll(Tasks.ToArray());
        TimeSpan ts = DateTime.Now - Start;
        Log($"All tasks completed in {ts:c}");
        WriteLogMessages();
    }

    public static void Log(string message) {
        var msg = $"AppThreadMgr: {DateTime.Now.ToString(DateTimeFormat)} {message}";
        Messages.Add(msg);
        Console.WriteLine(msg);
    }

    public static void WriteLogMessages() {
        var fullpath = $"./{AppThreadManagerLogFile}";
        List<string> currentLog = new();
        if(!System.IO.File.Exists(fullpath)) {
            System.IO.File.WriteAllLines(fullpath, new string[] {});
        }
        currentLog.AddRange(System.IO.File.ReadAllLines(fullpath));
        currentLog.Add($"---{Start.ToString(DateTimeFormat)}---------------------------------------------------");
        currentLog.AddRange(Messages);
        System.IO.File.WriteAllLines(fullpath, currentLog.ToArray());
    }

}
