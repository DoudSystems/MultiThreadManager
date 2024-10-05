using System;
using Dsi.MultiThreadManager;

namespace MultiThreadManager;

public class TestThreadLongCounter : AppThread {

    public int counter { get; set; } = 10;
    public double sum { get; set; } = 0;

    private void LongCounter(long counter) {
        double total = 0;
        for(int i = 0; i < counter; i++) {
            total += Math.Pow(i, 3) - Math.Pow(i, 2) + i - 7;
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
