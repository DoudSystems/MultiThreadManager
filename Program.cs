using Dsi.MultiThreadManager;
using MultiThreadManager;

TestThreadSleep t1 = new() { TaskName = "T1", Priority = 7, Sleep = 7000 };
TestThreadSleep t2 = new() { TaskName = "T2", Sleep = 3000};
TestThreadSleep t3 = new() { TaskName = "T3", Priority = 9, Sleep = 5000};
TestThreadLongCounter t4 = new() { TaskName = "T4", Priority = 1, counter = 20000 };
TestThreadLongCounter t5 = new() { TaskName = "T5", Priority = 1, counter = 10000 };
TestThreadLongCounter t6 = new() { TaskName = "T6", Priority = 1, counter = 50000 };

AppThreadManager.AddRange(t1, t2, t3, t4, t5, t6);

AppThreadManager.Execute();

AppThreadManager.AppThreads.ForEach(at => {
    if(at.ReturnResult is null)
        Console.WriteLine($"{at.TaskName} did not return a value.");
    else 
        Console.WriteLine($"{at.TaskName} returned {at.ReturnResult.ToString()}");
});