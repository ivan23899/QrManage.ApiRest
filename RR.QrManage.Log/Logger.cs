using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Diagnostics;

namespace RR.QrManage.Log
{
    public interface ILogger
    {
    }

    public class Logger : ILogger
    {
        private static string? _pathLogFile;
        public const string header = "{0} - {1}";

        public Logger(string pathLogFile, int limit, string level)
        {
            switch (level)
            {
                case "INFORMATION":
                    _pathLogFile = pathLogFile;
                    Serilog.Log.Logger = new LoggerConfiguration()
                        .Enrich.With(new ThreadIdEnricher())
                        .WriteTo.File(pathLogFile, rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] <{ThreadId}> {Message:lj}{NewLine}{Exception}", retainedFileCountLimit: limit)
                        .MinimumLevel.Information()
                        .CreateLogger();
                    break;
                case "FATAL":
                    _pathLogFile = pathLogFile;
                    Serilog.Log.Logger = new LoggerConfiguration()
                        .Enrich.With(new ThreadIdEnricher())
                        .WriteTo.File(pathLogFile, rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] <{ThreadId}> {Message:lj}{NewLine}{Exception}", retainedFileCountLimit: limit)
                        .MinimumLevel.Fatal()
                        .CreateLogger();
                    break;
                case "WARNING":
                    _pathLogFile = pathLogFile;
                    Serilog.Log.Logger = new LoggerConfiguration()
                        .Enrich.With(new ThreadIdEnricher())
                        .WriteTo.File(pathLogFile, rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] <{ThreadId}> {Message:lj}{NewLine}{Exception}", retainedFileCountLimit: limit)
                        .MinimumLevel.Warning()
                        .CreateLogger();
                    break;
                case "ERROR":
                    _pathLogFile = pathLogFile;
                    Serilog.Log.Logger = new LoggerConfiguration()
                        .Enrich.With(new ThreadIdEnricher())
                        .WriteTo.File(pathLogFile, rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] <{ThreadId}> {Message:lj}{NewLine}{Exception}", retainedFileCountLimit: limit)
                        .MinimumLevel.Error()
                        .CreateLogger();
                    break;
                case "DEBUG":
                    _pathLogFile = pathLogFile;
                    Serilog.Log.Logger = new LoggerConfiguration()
                        .Enrich.With(new ThreadIdEnricher())
                        .WriteTo.File(pathLogFile, rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] <{ThreadId}> {Message:lj}{NewLine}{Exception}", retainedFileCountLimit: limit)
                        .MinimumLevel.Debug()
                        .CreateLogger();
                    break;
                default:
                    _pathLogFile = pathLogFile;
                    Serilog.Log.Logger = new LoggerConfiguration()
                        .Enrich.With(new ThreadIdEnricher())
                        .WriteTo.File(pathLogFile, rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] <{ThreadId}> {Message:lj}{NewLine}{Exception}", retainedFileCountLimit: limit)
                        .MinimumLevel.Verbose()
                        .CreateLogger();
                    break;
            }
        }

        class ThreadIdEnricher : ILogEventEnricher
        {
            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
                        "ThreadId", Thread.CurrentThread.ManagedThreadId));
            }
        }

        private static string GetStackTraceInfo()
        {
            var stackFrame = new StackTrace().GetFrame(2)?.GetMethod();
            var methodName = stackFrame?.Name == "MoveNext" ? string.Empty : "." + stackFrame?.Name;
            var className = stackFrame?.ReflectedType?.FullName;
            return string.Format("{0}{1}", className, methodName);
        }

        public static void Debug(string format, params object[] objects)
        {
            string location = GetStackTraceInfo();
            string message = string.Format(format, objects);
            Serilog.Log.Debug(string.Format(header, location, message));
        }

        public static void Debug(string message)
        {
            string location = GetStackTraceInfo();
            Serilog.Log.Debug(string.Format(header, location, message));
        }

        public static void Error(string format, params object[] objects)
        {
            string message = string.Format(format, objects);
            string location = GetStackTraceInfo();
            Serilog.Log.Error(string.Format(header, location, message));
        }

        public static void Error(string message)
        {
            string location = GetStackTraceInfo();
            Serilog.Log.Error(string.Format(header, location, message));
        }

        public static void Fatal(string format, params object[] objects)
        {
            string message = string.Format(format, objects);
            string location = GetStackTraceInfo();
            Serilog.Log.Fatal(string.Format(header, location, message));
        }

        public static void Fatal(string message)
        {
            string location = GetStackTraceInfo();
            Serilog.Log.Fatal(string.Format(header, location, message));
        }

        public static void Information(string format, params object[] objects)
        {
            string message = string.Format(format, objects);
            string location = GetStackTraceInfo();
            Serilog.Log.Information(string.Format(header, location, message));
        }

        public static void Information(string message)
        {
            string location = GetStackTraceInfo();
            Serilog.Log.Information(string.Format(header, location, message));
        }

        public static void Verbose(string format, params object[] objects)
        {
            string message = string.Format(format, objects);
            string location = GetStackTraceInfo();
            Serilog.Log.Verbose(string.Format(header, location, message));
        }

        public static void Verbose(string message)
        {
            string location = GetStackTraceInfo();
            Serilog.Log.Verbose(string.Format(header, location, message));
        }
    }
}