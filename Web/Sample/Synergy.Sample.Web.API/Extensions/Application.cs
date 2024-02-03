using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Castle.Core.Internal;
using JetBrains.Annotations;
using Microsoft.Extensions.Hosting;
using Synergy.Contracts;
using Synergy.Sample.Web.API.Services.Infrastructure;

namespace Synergy.Sample.Web.API.Extensions
{
    public static class Application
    {
        [NotNull]
        public static Assembly GetRootAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }

        [NotNull, ItemNotNull]
        public static ReadOnlyCollection<Assembly> GetApplicationAssemblies()
        {
            return ReflectionUtil.GetApplicationAssemblies(Application.GetRootAssembly()).AsReadOnly();
        }

        public static Info GetApplicationInfo()
        {
            var assembly = Application.GetRootAssembly();
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            var createdOn = File.GetLastWriteTime(assembly.Location);

            return new Info(fileVersionInfo, createdOn);
        }

        /// <summary>
        /// Checks if the current host environment name is <see cref="Application.Environment.Tests"/>.
        /// </summary>
        /// <param name="hostEnvironment">An instance of <see cref="IHostEnvironment"/>.</param>
        /// <returns>True if the environment name is <see cref="Application.Environment.Tests"/>, otherwise false.</returns>
        public static bool IsTests([NotNull] this IHostEnvironment hostEnvironment)
        {
            Fail.IfNull(hostEnvironment);

            return hostEnvironment.IsEnvironment(Environment.Tests);
        }

        public static class Environment
        {
            public const string Tests = "Tests";
        }

        public struct Info
        {
            public string ProductName { get; }
            public string FileVersion { get; }
            public DateTime CreatedOn { get; }

            public Info([NotNull] FileVersionInfo fileVersionInfo, DateTime createdOn)
            {
                this.ProductName = fileVersionInfo.ProductName;
                this.FileVersion = fileVersionInfo.FileVersion;
                this.CreatedOn = createdOn;
            }
        }
    }
}