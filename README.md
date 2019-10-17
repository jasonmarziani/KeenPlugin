## NOTE FROM JASON



# MVP.LIVE Metrics for Unity3D

This is the repository of MVP Interactive's MVP.LIVE Metrics Unity3D plugin. This plugin is based off of Helios Keen.io plugin, a write-only Unity3D plugin to send (and cache to send later) events to the Keen.io service.  Since Keen.io has transitioned to $$paid$$ plans, MVP Interactive has included a Keen.io-like analytics service to the MVP.LIVE platform.  This plugin will write Unity3D events to that service.

Caching feature can come in handy if you are using MVP.LIVE's Metrics in situations where Internet connectivity is sparse. This library caches your calls and sends them at a later time when connection is back online and stable.

## Build notes

Remember to change your runtime configuration from **.NET subset to .NET** in build settings before building your Unity3D project (not needed if you are not using the SQLite cache provider).

Binaries for SQLite on Windows are checked in (x86 and x86_64). You may obtain additional binaries for other platforms from [SQLite project's website](http://www.sqlite.org/) if you need to.

## Tested Platforms

Virtually you can use this library on all platforms as long as you take care of providing a native build of `sqlite3`. A binary is checked in for followin **tested** platforms:

  - Windows x86
  - Windows x64
  - OSX x64
  - iOS x64

On iOS, you must use `Application.persistentDataPath` as your cache directory (this is one of the fewest directories write-able on iOS devices).

## Usage

Include the Assets/Helios and Assets/MVP folders into your project.
Add MVPMetricsTracking.cs to a GameObject in the Heirarchy.

MVPMetricsTracking class provides 3 shortcut methods for common game events:
GameStartedEvent
RegisteredEvent
GameOverEvent

These events prepopulate the event name.  Each of these events takes a "registered" boolean.  GameOverEvent takes a generic "score" integer.

Each of these shortcut event classes extend from a generic TrackedEvent class.

Usage:

In a class wishing to track events, establish a reference to MVPMetricsTracking instance.

```C#
if(_analytics == null) _analytics = GetComponent<MVPMetricsTracking>();
```
or
```C#
_analytics = MVPMetricsTracking.Instance;
```
then,
```C#
_analytics.TrackEvent(new MVPMetricsTracking.GameStartedEvent(){ registered = false });

_analytics.TrackEvent(new MVPMetricsTracking.GameOverEvent(){ registered = _registered, score = Mathf.CeilToInt(Random.value * 100) });
```
or simply
```C#
var eventToTrack = new MVPMetricsTracking.TrackedEvent();
eventToTrack.name = "FaceNotRecognized";
MVPMetricsTracking.Instance.TrackEvent(eventToTrack);
```

See TestMetricsTracking for in-context examples of how to interface with MVPMetricsTracking


Below is Helios / Keen.io documentation for sending events.
-----

And start sending events:

```C#
// This is an example of sending Helios specific events
MetricsClient.SendQuizEvent(new Helios.Keen.Client.QuizEvent
{
	quizId = "IQ test",
	quizResult = "failed",
	experienceData  = new Helios.Keen.Client.ExperienceData
	{
		experienceLabel = "Keen Plugin",
		versionNumber   = "1.0.0",
		location        = "never land"
	}
});

// This is an example of using custom data types
MetricsClient.SendEvent("custom_event", new CustomData
{
	data_member_1 = "test string",
	data_member_2 = 25000.0f,
	data_member_3 = new CustomNestedData
	{
		data_member_1 = "\"nested\" string",
		data_member_2 = 25000d,
	}
});
```

Don't forget to cleanup after yourself! (In case you used `AddComponent`)

```C#
Destroy(MetricsClient);
```

Take a look at `MetricsExample.cs` for more in-depth usage examples.
Also `SessionExample.cs` shows you how to use `StateAwareClient` class.

## Built-in JSON serializer notes

There is an extremely simplistic JSON serializer built into this library (about 60 lines of code with comments!) which provides a portable and backwards compatible C# implementation for serializing **FLAT CLASSES, FLAT STRUCTS, and POD data types**.

This means the serializer *does not support fancy features such as inheritance*. You can absolutely use a custom and more advanced JSON serializer; if you absolutely need to work with more complicated data types. Here's an example of using [Unity 5.3's JSON serializer](http://docs.unity3d.com/Manual/JSONSerialization.html) and using the `SendEvent(string, string)` overload:

```C#
MetricsClient.SendSession("eventName", JsonUtility.ToJson(myComplexObject));
```

## Source notes

There are two important namespaces:

 1. `Helios.Keen`
 2. `Helios.Keen.Cache`

All cache implementations go under `Helios.Keen.Cache` namespace. For now it only contains a SQLite cache implementation. This is an optional dependency. You are more than welcome to strip it out and provide a custom `ICacheProvider` implementation of your own.

SQLite cache provider has a dependency on native `sqlite3` binaries. They are checked in under `Plugins` folder for Windows 32/64 bit and Android x86/armabi-v7. *Note that SQLite provider is optional. You can provide your own caching mechanism*.

class `Helios.Keen.Client` is the actual client library. class `Helios.Keen.Cache.Sqlite` is the SQLite cache provider to be used optionally with the client instance.

## SQLite cache implementation notes

For optimal performance I highly recommend multiple instances of `Helios.Keen.Client` in conjunction with `Helios.Keen.Cache.Sqlite` instances. Each instance of `Helios.Keen.Cache.Sqlite` must point to a separate database for optimal performance otherwise it causes race conditions/lock issues between multiple instances pointing to the same database.

You may also consider installing [Visual C++ Redistributable for Visual Studio 2015](https://www.microsoft.com/en-us/download/details.aspx?id=48145) if you are using binaries provided in this repository under Windows.

## Helios extensions

Helios internally uses some conventions when dealing with Keen.IO. These conventions can be found in `Helios/Keen/ClientHeliosExtension.cs`.
