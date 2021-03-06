﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ItsLogExceptionLogger.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper
{
    using System.Web.Http.ExceptionHandling;

    /// <summary>
    /// Logs all Web API exceptions.
    ///   Reference: https://stackoverflow.com/questions/16028919/catch-all-unhandled-exceptions-in-asp-net-web-api
    ///   Reference: http://www.asp.net/web-api/overview/error-handling/web-api-global-error-handling
    ///   For a lower-level OWIN exception handling see: http://www.jayway.com/2016/01/08/improving-error-handling-asp-net-web-api-2-1-owin/
    ///   and: http://stackoverflow.com/questions/34201527/how-do-i-disable-all-exception-handling-in-asp-net-web-api-2-to-make-room-for.
    /// </summary>
    /// <seealso cref="System.Web.Http.ExceptionHandling.ExceptionLogger" />
    public class ItsLogExceptionLogger : ExceptionLogger
    {
        /// <summary>
        /// Logs the exception synchronously.
        /// </summary>
        /// <param name="context">The exception logger context.</param>
        public override void Log(ExceptionLoggerContext context)
        {
            Its.Log.Instrumentation.Log.Write(context);
        }
    }
}
