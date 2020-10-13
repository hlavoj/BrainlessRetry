# BrainlessRetry
Simple library for retry of any method. 


## Retry with wait
If you want to try call method 10-times and after each fail wait 1000ms to next try.

```
IWaitAndRetry retry = new WaitAndRetry();
var result = retry.Retry<string>(10, 1000, () => YourMethod(...) );
```


## Installation
In proggress not available now.
```
PM> Install-Package BrainlessRetry
```

