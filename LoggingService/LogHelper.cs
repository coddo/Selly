using System;
using System.Runtime.CompilerServices;
using NLog;

namespace LoggingService
{
    public class LogHelper
    {
        private static Logger mDefaultLogger;

        /// <summary>
        ///     Property that exposes the interface of NLog Logger. (Property is readonly)
        /// </summary>
        private static Logger Logger => mDefaultLogger ?? (mDefaultLogger = LogManager.GetCurrentClassLogger());

        /// <summary>
        ///     This method will create a new logger with a specific name, because the default name is the actual name for the
        ///     current class.
        /// </summary>
        /// <typeparam name="T">the calling class from which the logging is done and name will be extracted</typeparam>
        /// <returns>the new logger with the name of T</returns>
        private static Logger CreateSpecificLogger<T>() where T : class
        {
            var loggerName = typeof(T).FullName;

            Logger result = null;
            if (!string.IsNullOrWhiteSpace(loggerName))
            {
                result = LogManager.GetLogger(loggerName);
            }

            return result;
        }

        /// <summary>
        ///     Allow the logging of an exeception as an error. The log entry will contain the stack trace of the execption and
        ///     an additional message in case the user provides it.
        /// </summary>
        /// <typeparam name="T">the calling class from which the logging is done and name will be extracted</typeparam>
        /// <param name="e">the execption to be logged</param>
        /// <param name="additionalMessage">an additional message that will be logged if provided</param>
        /// <param name="memberName">The name of the component that calls the exception logging</param>
        public static void LogException<T>(Exception e, string additionalMessage = null,
            [CallerMemberName] string memberName = "") where T : class
        {
            if (e == null)
            {
                return;
            }

            var specificLogger = CreateSpecificLogger<T>();
            specificLogger?.Error(e, CreateExceptionMessage(e, memberName, additionalMessage));
        }

        /// <summary>
        ///     Allow the logging of an exceptional case without having an actual .Net object exception.
        /// </summary>
        /// <typeparam name="T">the calling class from which the logging is done and name will be extracted</typeparam>
        /// <param name="exceptionMessage">the execption message to be logged</param>
        /// <param name="memberName">The name of the component that calls the exception logging</param>
        public static void LogException<T>(string exceptionMessage, [CallerMemberName] string memberName = "")
            where T : class
        {
            if (string.IsNullOrWhiteSpace(exceptionMessage))
            {
                return;
            }

            var specificLogger = CreateSpecificLogger<T>();
            specificLogger?.Error($"{memberName}: {exceptionMessage}");
        }

        /// <summary>
        ///     Allow the logging of an exeception as an error. The log entry will contain the stack trace of the execption and
        ///     an additional message in case the user provides it. The name of the logger in the logfiles will be the name of the
        ///     current class: "LaunchPad.Helpers.LogHelper"
        /// </summary>
        /// <param name="e">the execption to be logged</param>
        /// <param name="additionalMessage">an additional message that will be logged if provided</param>
        /// <param name="memberName">The name of the component that calls the exception logging</param>
        public static void LogException(Exception e, string additionalMessage = null,
            [CallerMemberName] string memberName = "")
        {
            if (e != null)
            {
                Logger.Error(e, CreateExceptionMessage(e, memberName, additionalMessage));
            }
        }

        /// <summary>
        ///     Allow the logging of an exceptional case without having an actual .Net object exception. The name of the logger in
        ///     the logfiles will be the name of the current class: "LaunchPad.Helpers.LogHelper"
        /// </summary>
        /// <param name="exceptionMessage">the execption message to be logged</param>
        /// <param name="memberName">The name of the component that calls the exception logging</param>
        public static void LogException(string exceptionMessage, [CallerMemberName] string memberName = "")
        {
            if (!string.IsNullOrWhiteSpace(exceptionMessage))
            {
                Logger.Error($"{memberName}: {exceptionMessage}");
            }
        }

        /// <summary>
        ///     Creates a message to log from the exception using a particular template
        /// </summary>
        /// <param name="e">the exception to log</param>
        /// <param name="memberName">The name of the component that calls the exception logging</param>
        /// <param name="additionalMessage">the aditional message to add to the log</param>
        /// <returns>the formated string template based on the exception</returns>
        private static string CreateExceptionMessage(Exception e, string memberName, string additionalMessage = null)
        {
            var message = string.Empty;
            if (e != null)
            {
                if (!string.IsNullOrWhiteSpace(additionalMessage))
                {
                    message += $"Message: {additionalMessage} \n Stack Trace: \n {e}";
                }
                else
                {
                    message += $"Stack Trace: \n {e}";
                }
            }
            if (!string.IsNullOrWhiteSpace(memberName))
            {
                message = $"{memberName}: {message}";
            }
            return message;
        }

        /// <summary>
        ///     Allow the logging of an informational message. The log entry will contain the message. The name of the logger in
        ///     the logfiles will be the name of the current class: "LaunchPad.Helpers.LogHelper"
        /// </summary>
        /// <param name="infoMessage">a message that will be logged if provided</param>
        /// <param name="memberName">The name of the component that calls the exception logging</param>
        public static void LogInfo(string infoMessage, [CallerMemberName] string memberName = "")
        {
            if (!string.IsNullOrWhiteSpace(infoMessage))
            {
                Logger.Info($"{memberName}: {infoMessage}");
            }
        }

        /// <summary>
        ///     Allow the logging of an informational message. The log entry will contain the message. The name of the logger in
        ///     the logfiles will be the name of T
        /// </summary>
        /// <typeparam name="T">The class for which a new logger will be created</typeparam>
        /// <param name="infoMessage">a message that will be logged if provided</param>
        /// <param name="memberName">The name of the component that calls the exception logging</param>
        public static void LogInfo<T>(string infoMessage, [CallerMemberName] string memberName = "") where T : class
        {
            if (string.IsNullOrWhiteSpace(infoMessage))
            {
                return;
            }

            var specificLogger = CreateSpecificLogger<T>();
            specificLogger?.Info($"{memberName}: {infoMessage}");
        }

        public static string CreateExceptionMessage(string exceptionFormat, params object[] args)
        {
            string result = null;
            if (!string.IsNullOrWhiteSpace(exceptionFormat))
            {
                result = string.Format(exceptionFormat, args);
            }
            return result;
        }
    }
}
