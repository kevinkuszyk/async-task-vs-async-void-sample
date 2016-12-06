# async task vs async void sample

This repro contains the sample code for my [question on Stack Overflow](http://stackoverflow.com/questions/41000686/async-void-vs-async-task-in-web-api-an-asynchronous-operation-was-still-pending).

## Background

We have Web API controllers that look like this:

```c#
public class HomeController : ApiController
{
    Logger logger = new Logger();

    [HttpGet, Route("")]
    public IHttpActionResult Index()
    {
        logger.Info("Some info logging");

        return Ok();
    }
}
```

Internally, our loggers use the `HttpClient` to `POST` data to an Azure Event Hub.  This means that the logger has a synchronous method which internally calls an asynchronous method.

## The broken implementation

When our logger has a private method with an `async void` signature like this:

```c#
public class Logger
{
    public void Info(string message)
    {
        LogAsync(message);
    }

    private async void LogAsync(string message)
    {
        await PostAsync(message);
    }
}
```

We see the following error:

![Yellow screen of death](https://raw.githubusercontent.com/kevinkuszyk/async-task-vs-async-void-sample/master/images/yellow-screen-of-death.PNG)

## The working implementation

If we change our logger to use an `async Task` signature like this it works:

```c#
public class Logger
{
    public void Info(string message)
    {
        LogAsync(message);
    }

    private async Task LogAsync(string message)
    {
        await PostAsync(message);
    }
}
```

## Running the sample app

The sample app in this repro is an stripped down Web API application.

The following requests use the `AsyncVoidLogger` and will result in the `InvalidOperationException`:

```
GET http://localhost:7144
GET http://localhost:7144/async-void
```

This request uses the `AsyncTaskLogger` and will return an `HTTP 200` result:

```
GET http://localhost:7144/async-task
```

## The question

In both cases our logging messages are sent to the Event Hub.   I'd like to know why `async Task` allows the controller to return the expected result, and `async void` results in an `InvalidOperationException`?

Also, is there any best practice regarding calling into asynchronous code from a synchronous method?
