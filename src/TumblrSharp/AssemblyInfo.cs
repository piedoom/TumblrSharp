using System.Reflection;

[assembly: AssemblyCompany("Alexander Lozada; Ulf (Cataurus) Prill; et al.")]
[assembly: AssemblyCopyright("Copyright 2017-2025")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#endif
#if RELEASE
[assembly: AssemblyConfiguration("Release")]
#endif

#if (NETSTANDARD2_0)
[assembly: AssemblyDescription("Tumblr# extension for .NETStandard 2.0")]
[assembly: AssemblyProduct("DontPanic.TumblrSharp for .NETStandard 2.0")]
#endif
#if (NET6_0)
[assembly: AssemblyDescription("Tumblr# extension for .NET 6.0")]
[assembly: AssemblyProduct("DontPanic.TumblrSharp for .NET 6.0")]
#endif

[assembly: AssemblyTitle("DontPanic.TumblrSharp")]
[assembly: AssemblyVersion("1.2.1.0")]
[assembly: AssemblyFileVersion("1.2.1.0")]
