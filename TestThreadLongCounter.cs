using System;
using Dsi.MultiThreadManager;

namespace MultiThreadManager;

public class TestThreadLongCounter : AppThread {

    public long counter { get; set; } = 1000;

    private void LongCounter(long counter) {
        for(int i = 0; i < counter; i++) {
            counter += (i % 2 == 0) ? i : -i;
        }
    }
    
    public override void ExecAppThreadAsync() {
        StartTimer();
        LongCounter(this.counter);
        FinishTimer();
    }
}
