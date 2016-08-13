﻿using System;
using System.Reflection;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

// ReSharper disable once CheckNamespace
namespace Microsoft.ApplicationInsights.Extensibility
{
    public class ComponentVersionContextInitializer : ITelemetryInitializer
    {
        /// <summary>
        /// Lazily builds the value for the <see cref="ComponentContext.Version"/> property of the <see cref="TelemetryContext.Component"/> property in <see cref="ITelemetry.Context"/>.
        /// </summary>
        private readonly Lazy<string> _componentVersion;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentVersionContextInitializer" /> class.
        /// </summary>
        public ComponentVersionContextInitializer()
        {
            _componentVersion = new Lazy<string>(() => Assembly.GetEntryAssembly()?.ToString() ?? "Unknown");
        }

        #region Implementation of ITelemetryInitializer

        /// <summary>
        /// Initializes the given <see cref="Microsoft.ApplicationInsights.Channel.ITelemetry"/>.
        /// </summary>
        public void Initialize(ITelemetry telemetry)
        {
            if (String.IsNullOrWhiteSpace(telemetry.Context.Component.Version))
            {
                telemetry.Context.Component.Version = _componentVersion.Value;
            }
        }

        #endregion
    }
}