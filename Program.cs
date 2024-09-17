using Dsi.MultiThreadManager;
using MultiThreadManager;

TestThreadSleep t1 = new() { TaskName = "T1", Priority = 7, Sleep = 7000 };
TestThreadSleep t2 = new() { TaskName = "T2", Sleep = 3000};
TestThreadSleep t3 = new() { TaskName = "T3", Priority = 9, Sleep = 5000};
TestThreadLongCounter t4 = new() { TaskName = "T4", Priority = 1, counter = 200000 };

AppThreadManager.AddRange(t1, t2, t3, t4);

AppThreadManager.Execute();

AppThreadManager.AppThreads.ForEach(at => {
    if(at.ReturnResult is null)
        Console.WriteLine($"{at.TaskName} did not return a value.");
    else 
        Console.WriteLine($"{at.TaskName} returned {at.ReturnResult.ToString()}");
});