using System.Reflection;

[assembly: AssemblyCompany("Alexander Lozada; Ulf (Cataurus) Prill; et al.")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#endif
#if RELEASE
[assembly: AssemblyConfiguration("Release")]
#endif
[assembly: AssemblyCopyright("Copyright 2017-2020")]

#if (NETSTANDARD1_1)
[assembly: AssemblyDescription("Tumblr# extension for .NETStandard 1.1")]
#endif
#if (NETSTANDARD1_2)
[assembly: AssemblyDescription("Tumblr# extension for .NETStandard 1.2")]
#endif
#if (NETSTANDARD1_3)
[assembly: AssemblyDescription("Tumblr# extension for .NETStandard 1.3")]
#endif
#if (NETSTANDARD2_0)
[assembly: AssemblyDescription("Tumblr# extension for .NETStandard 2.0")]
#endif
#if (NETCOREAPP2_2)
[assembly: AssemblyDescription("Tumblr# extension for .NetCore 2.2")]
#endif

#if (NETSTANDARD1_1)
[assembly: AssemblyProduct("DontPanic.TumblrSharp.Client for .NETStandard 1.1")]
#endif
#if (NETSTANDARD1_2)
[assembly: AssemblyProduct("DontPanic.TumblrSharp.Client for .NETStandard 1.2")]
#endif
#if (NETSTANDARD1_3)
[assembly: AssemblyProduct("DontPanic.TumblrSharp.Client for .NETStandard 1.3")]
#endif
#if (NETSTANDARD2_0)
[assembly: AssemblyProduct("DontPanic.TumblrSharp.Client for .NETStandard 2.0")]
#endif
#if (NETCOREAPP2_2)
[assembly: AssemblyProduct("DontPanic.TumblrSharp.Client for .NetCore 2.2")]
#endif

[assembly: AssemblyTitle("DontPanic.TumblrSharp.Client")]

[assembly: AssemblyFileVersion("1.2.0.0")]

[assembly: AssemblyInformationalVersion("1.2.0.0")]

[assembly: AssemblyVersion("1.2.0.0")]
