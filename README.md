[![Build Status](https://dev.azure.com/hlavoj/BrainlessRetry/_apis/build/status/hlavoj.BrainlessRetry?branchName=main)](https://dev.azure.com/hlavoj/BrainlessRetry/_build/latest?definitionId=4&branchName=main)
[![NuGet version (Newtonsoft.Json)](https://img.shields.io/nuget/v/BrainlessRetry.svg?style=flat)](https://www.nuget.org/packages/BrainlessRetry/)
[![Test codecoverage](https://img.shields.io/azure-devops/coverage/hlavoj/BrainlessRetry/4/main)](https://dev.azure.com/hlavoj/BrainlessRetry/_build?definitionId=4)
[![Build test](https://img.shields.io/azure-devops/tests/hlavoj/BrainlessRetry/4/main)](https://dev.azure.com/hlavoj/BrainlessRetry/_build?definitionId=4)




# <img src="BrainlessRetryIcon.png" alt="drawing" width="30"  /> BrainlessRetry
Simple library for retry of any method. 


## Retry with wait
If you want to try call method 10-times and after each fail wait 1000ms to next try:

```
var result = WaitAndRetry.Retry<string>(10, 1000, () => YourMethod(...) );
```

### Async methods:
```
var result = await WaitAndRetry.RetryAsync<string>(10, 1, () => YourMethodAsync(...) );
```

## Installation
```
PM> Install-Package BrainlessRetry
```

**For more examples check out** [Unit tests](https://github.com/hlavoj/BrainlessRetry/blob/main/src/BrainlessRetry.Tests/WaitAndRetryUnitTest.cs) **or** [BrainlessRetry.ConsoleExample](https://github.com/hlavoj/BrainlessRetry/tree/main/src/BrainlessRetry.ConsoleExample).