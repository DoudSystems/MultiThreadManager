# MultiThreadManager

MultiThreadManager is designed to make it easy for developers to take advantage of the multi-core cpus and have their methods run asnchronously in the background rather than synchronously in the foreground. This allows taking better advantage of the multi-core CPUs and it allows applications to finish sooner. It can run multiple methods asynchronously to make use of the multi-core CPUs

## What methods can run async?

The criteria for having multiple methods running asynchronously, both methods must be independent of each other. If method1 returns a value that must be pass into method2, these two methods cannot be run asynchronously because method2 cannot run until method1 is finished. But if method1 will validate a state code while method2 will validate a value within a range, these two methods can run asynchronously since they are independent of each other.

## AppThread class

The AppThread class has all the needed capabilities to allow easy asynchronous execution. It must be inherted by a class that wants to run one or more of its methods asynchronously. The following is a simple example of how to use it.

```cs
class UserCalcFactorial : AppThread {

    // this method override is required!
    public override void ExecAppThreadAsync() {
        ReturnResult = CalcFactorial(6)
    }

    private int CalcFactorial(int n) {
        var factorial = 1;
        for(var idx = n; idx > 1; idx--) {
            factorial *= idx;
        }
        return factorial;
    }

}
```

The `ExecAppThreadAsync()` method is *required* to be overridden and should simply call the method to be run asynchronously. In the body of the method, One or more methods can be executed. 

The `TaskName` property is a `string` type for giving each method a user defined name.

The `Priority` property is an `int` type that allows control as to which methods should be started before other methods. This is useful when there are multiple methods to run and some will run longer than others. In this case, it makes sense to get those longer running methods as soon as possible. Lower numbers represent higher priority. The default priority value is 5.

The `ReturnResult` property is an `object` type that can be used when the method to run returns a value.

The `StartTimer()` and `FinishTimer()` methods optionally allow capturing the start and end time that a method takes to complete. To use them, call the StartTime() method before executing the method. Right after the method is executed, the FinishTimer() should be called to display the runtime duration of the method.

## AppThreadManager class

The AppThreadManager class is responsible for executing all the methods to be run asynchronously after they are all loaded.

Once a class is ready, it should create a instance of itself and that instance should be loaded into the collection using either `Add()` or `AddRange()` within AppThreadManager. When all the instances are loaded, call the `Execute()` method

```cs
// create and load an instance of the user class
AppThreadManager.Add( new() { TaskName = "FACT1", Priority = 7, n = 6 } );

// load multiple instances at once
AppThreadManager.AddRange( 
    new UserCalcFactorial() { TaskName = "FACT2", Priority = 4, n = 8 },
    new UserCalcFactorial() { TaskName = "FACT3", Priority = 1, n = 3 }
);

// execute all the methods asynchronously
AppThreadManager.Execute();
```

When the methods have been executed, the completed methods will exist in the `AppThreads` collection.