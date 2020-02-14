using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;

namespace SqlTools
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [Guid("e8fe6622-1282-4bcc-874e-ef26d37daea3")]
    public sealed class SqlToolsPackage  : AsyncPackage
    {
    }
}
