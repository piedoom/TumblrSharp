TumblrSharp
========

This is a continuation of the excellent [TumblrSharp](https://tumblrsharp.codeplex.com/) C# Library developed by [the community](https://tumblrsharp.codeplex.com/team/view).

Documentation
========

Please refer to the [Wiki](https://github.com/piedoom/TumblrSharp/wiki) to learn how to use TumblrSharp. 

Why?
========
TumblrSharp is a very nicely designed library, and perhaps the *only* usable C# library that currently exists.  However, development for the Codeplex project stopped in 2014.  While using the library, I've noticed several bugs which I'll fix and post to this repository.

What has been fixed in this new version?
========

If you download the old TumblrSharp version off of CodePlex or NuGet, you won't get any of the fixes this libary provides - most notably, support for `Asks` and `Submissions`.

- [x] Errors with getting submission posts and their new post `state` type
- [x] Allow posts to be sent with a `published` state, which is currently the only way to publish asks
- [x] New `CreateAnswer` method on `PostData` class allows for editing and publishing asks
- [x] Eliminated superfluous constructor overloads in `PostData` methods, allowing for shorter, more maintainable code
- [x] Eliminated unnecessary required parameters like `title` or `body` from a text post, as they are not required by the Tumblr API. 
- [x] Opted for default values in `PostData`.  This is important because specifying something simple like a `PostCreationState` on 
a photo post would require possibly unneeded data, like `tags`.
- [x] Move everything into a PCL for maximum compatibility.

What needs to be implemented?
========
- [ ] "Reblogged from" broken
- [ ] Add Reblog Trail support
- [ ] Better documentation
- [ ] Examples of how to use the library
- [ ] Getting dashboard posts *after* a date rather than *before*

What will *not* be implemented?
=========
- Chat
- Activity

(This is due to restrictions to the V2 API.  These endpoints exist but can only be accessed with the official Tumblr app API key).

Contributing 
========
Please feel free to contribute if you find any problems / have any features.

NuGet
=====
You can find the latest NuGet package [here](https://www.nuget.org/packages/NewTumblrSharp/).  An automated build system will push a new NuGet package when a tagged commit is merged into the master branch.

License
========
TumblrSharp follows the [MIT](https://tumblrsharp.codeplex.com/license) License.
