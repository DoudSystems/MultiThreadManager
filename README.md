# MultiThreadManager

MultiThreadManager is designed to make it easy for developers to take advantage of the multi-core cpus and have their methods run simultaneously in the background rather than serially. 

The project allows giving priority to methods that should be started before other methods. This is useful when there are many methods to run and some will run longer than others. In this case, it makes sense to get those methods started as soon as possible.

The project has an example that demonstrates the efficiency attained by running methods simultaneously rather than serially.