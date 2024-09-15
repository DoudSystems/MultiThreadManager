using System;
using Dsi.MultiThreadManager;

namespace MultiThreadManager;

public class TestThreadSleep : AppThread {

    public int Sleep { get; set; } = 3000;

    public override void ExecAppThreadAsync()
    {   
        StartTimer();
        Thread.Sleep(Sleep);
        FinishTimer();
    }

}
