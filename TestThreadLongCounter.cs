using System;
using Dsi.MultiThreadManager;

namespace MultiThreadManager;

public class TestThreadLongCounter : AppThread {

    public int counter { get; set; } = 10;
    public int sum { get; set; } = 0;

    private void LongCounter(long counter) {
        int total = 0;
        for(int i = 0; i < counter; i++) {
            total += (i % 2 == 0) ? 2 * i : -3 * i;
        }
        sum = total;
    }
    
    public override void ExecAppThreadAsync() {
        StartTimer();
        LongCounter(this.counter);
        FinishTimer();
        ReturnResult = sum;
    }
}
