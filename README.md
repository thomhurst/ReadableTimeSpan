# ReadableTimeSpan
A TimeSpan that can be defined as a string in configuration files, and easy to read.

[![nuget](https://img.shields.io/nuget/v/TomLonghurst.ReadableTimeSpan.svg)](https://www.nuget.org/packages/TomLonghurst.ReadableTimeSpan/)
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/976b0c6b323b43ef94334f503af9b737)](https://www.codacy.com/app/thomhurst/ReadableTimeSpan?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=thomhurst/ReadableTimeSpan&amp;utm_campaign=Badge_Grade)

### Why?
Sometimes we want to define `TimeSpan`s in our `appsettings.json` (or other configuration) files - And that requires stringified TimeSpans.
The problem is, they look something like this: `00:01:00`
Does that mean 1 hour? 1 minute? 1 second?
Even if you know, it can be very easy to accidentally set the wrong value.
So this let's you write `TimeSpan`s in human readable language, and it'll do the conversion for you!

So I can instead write: `1 minute` or `1m`

### How can I write them?
The syntax is: `{number}{optional space}{unit}`

If you want to combine units, such as seconds and milliseconds, that's possible too, by using separators.

That would be more like: `{number}{optional space}{unit} {separator} {number}{optional space}{unit}`

Separators can be:
`:` `|` `+` `-` `and`

The units can be:

```
    T,
    Tick,
    Ticks,
    
    Ms,
    Mil,
    Mils,
    Milli,
    Millis,
    Millisecond,
    Milliseconds,
    
    S,
    Sec,
    Secs,
    Second,
    Seconds,
    
    M,
    Min,
    Mins,
    Minute,
    Minutes,
    
    H,
    Hr,
    Hrs,
    Hour,
    Hours,
    
    D,
    Day,
    Days
```

So a perfectly valid `TimeSpan` could look like:
`166 days | 13 hrs | 42 min | 29 seconds and 324ms`
or
`166d + 13h + 42m + 29s + 324ms`

### What else do I need to do?
`ReadableTimeSpan` works with `IConfiguration` and Binding. So if you use Microsoft `ConfigurationBuilder` or `IOptions`, the conversion from `appsettings.json` to Application Code happens automatically. All you need to do, is BEFORE you bind any config, call this static method:
`ReadableTimeSpan.EnableConfigurationBinding();`

If you want to use it outside of Configuration Binding, simply call `ReadableTimeSpan.Parse` or `ReadableTimeSpan.TryParse`

### ToReadableString()
`ToReadableString()` will give you a string version of the TimeSpan, but with words instead of just numbers. 
