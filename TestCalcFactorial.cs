using System;
using Dsi.MultiThreadManager;

namespace MultiThreadManager;

public class TestCalcFactorial : AppThread {

    public int n { get; set; } = 6;

    private int CalcFactorial(int n) {
        int fact = 1;
        for(int i = n; i > 1; i--) {
            fact *= i;
        }
        return fact;
    }
    
    public override void ExecAppThreadAsync() {
        StartTimer();
        ReturnResult = CalcFactorial(n);
        FinishTimer();
    }
}
