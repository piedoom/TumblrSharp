# Authenticating
*** 

To use TumblrSharp properly, you'll need to authenticate with a user account.  Tumblr does provide some unauthenticated API endpoints in their V1 API, but TumblrSharp doesn't cover those (and will most likely never).

## without OAuth Flow
*** 

Often, apps that consume 3rd party APIs use OAuth.  There are a few ways to setup TumblrSharp with OAuth, but the easiest is to use the [Tumblr Console](https://api.tumblr.com/console). Note - this method is only suitable for your own usage (e.g., if you are making a personal bot or anything specific to your own user account.  Please look below if you're looking to make an app that anyone can use).

Sign into your account, create an application, and then enter your Consumer key and Consumer Secret into the [Tumblr Console](https://api.tumblr.com/console).  Tumblr will then show four sensitive keys - a `consumer key`, `consumer secret`, `oauth_token` and `oauth_token_secret`.  

Here is an example class -

```cs
using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;

public static class Tumblr
{
    public static string Blog { get; set; }
    public static TumblrClient Client;
    const string CONSUMER_KEY = "xxx";
    const string CONSUMER_SECRET = "xxx";
    const string OAUTH_TOKEN = "xxx";
    const string OAUTH_TOKEN_SECRET = "xxx";

    static Tumblr()
    {
        // create our client
        Client = new TumblrClientFactory().Create<TumblrClient>(CONSUMER_KEY, CONSUMER_SECRET, new DontPanic.TumblrSharp.OAuth.Token(OAUTH_TOKEN, OAUTH_TOKEN_SECRET));
    }
}
```

With that static class set up, you can call methods easily

```cs
UserInfo userInfo = await Tumblr.Client.GetUserInfoAsync();
```


## with OAuth Flow
*** 


This section is for production applications.  If you want individual user access, you'll need to use OAuth's regular flow.
Before continuing, I recommend reading the [OAuth Bible](http://oauthbible.com/#oauth-10a-three-legged) if you are not familiar with how OAuth works.  (We're doing Three-Legged OAuth 1.0A).

This section also is **not** going to show how to implement C#/WPF/Winforms specific functions, like capturing a custom URI protocol.  (However, an examples repository will be up soon!)

### Overview

Before we begin, let's take a minute to deconstruct the steps we'll need to accomplish in order to sign in a user.

1. Create our Tumblr Application
2. Request a `Request` Token
3. Open a web browser and prompt a user for permission
4. Capture a `callback` URI
5. Request an `Access` Token
6. Perform authenticated requests

Although this may seem complex, it's not terribly difficult.  The hardest part is capturing the `callback` URI, as it differs depending on if you are using Xamarin, WPF, UWP, etc.

### Creating the Tumblr Application

You will first need to register a Tumblr application.  This can be accomplished by signing into your account, clicking Settings > Apps > Register New Application.  If you're already signed in, just [click here](https://www.tumblr.com/oauth/register).

The form will ask for several necessary fields.  You can fill out everything but the `Default callback URL` anyway you'd like.  For the `Default callback URL`, you have a few choices.  If you're using TumblrSharp with an ASP.NET web project, you'll want to set this to your website's URL.  If you're creating anything that doesn't live on a web server, you're going to want to specify a custom URI 

> A URI scheme is a simple way to associate certain calls to a specific application. For instance, `https://` opens with a web browser, and `ftp://` is used with FTP clients like Filezilla.  URI schemes are fairly arbitrary.  Music streaming company Spotify uses the URI `spotify://` to communicate between the browser and their desktop application.  

For our own application, we can use a custom URI.  This can be anything, but I recommend using your app's name.  If my application were "Noted", my `Default callback URL` would be `noted://`.

After you have chosen a callback URL, save the application.

### Getting a request token

The first step in the OAuth flow is to get a request token.  

```cs
// create our OAuth client
OAuthClient oauthClient = new OAuthClient(
    new DontPanic.TumblrSharp.HmacSha1HashProvider(),
    CONSUMER_KEY,
    CONSUMER_SECRET);

// get a request token
// replace "noted://" with your own callback URI
Token requestToken = await oauthClient.GetRequestTokenAsync("noted://");
```

Next, we need to ask the user for their permission. We can do this in a variety of ways, but preferably we want to open the default browser to a specific URL, which is a specific site route with the requestToken key appended.  

```cs
var authenticateUrl = "https://www.tumblr.com/oauth/authorize?oauth_token=" + requestToken.Key;

// depending on your project, you might open up a web browser a different way
System.Diagnostics.Process.Start(authenticateUrl);
```

Once the webpage opens, the user will hopefully click "Allow", and your callback URL will be triggered.

### Capturing the callback URL 

This is the toughest part, since it requires a little research.  There is a different way for every platform, so look up how to associate your application with a URI protocol.  For instance, if you were using Xamarin with Android, you would flag your method like this: 

```cs
[IntentFilter(new[] { Intent.ActionView }, DataScheme = "noted", Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault })]
```

We want a specific method to fire when the callback URI is called.

### Getting access tokens

Once you've captured your callback, you're ready for the final step: requesting access tokens.  Let's parse our verifier url.

```cs
// this will be significantly different depending on what type of project you're making.  You'll need to consult the documentation for the service in question.
var verifierUrl = Intent.Data.EncodedQuery;
```

Once we have the query, we can make a request for an Access Token, and then we can initialize our TumblrClient class!

```cs
Token accessToken = await oauthClient.GetAccessTokenAsync(requestToken, verifierUrl);
TumblrClient client = new TumblrClientFactory().Create<TumblrClient>(CONSUMER_KEY, CONSUMER_SECRET, accessToken);
```

### Using the TumblrClient

Once we're done, we have access to all of our necessary API endpoints.

```cs
UserInfo info = await client.GetUserInfoAsync();
```

### Code


```cs

Token requestToken;

async void Authenticate()
{
    // create a new instance of our OAuthClient we will use to authenticate
    OAuthClient oauthClient = new OAuthClient(
        new DontPanic.TumblrSharp.HmacSha1HashProvider(),
        CONSUMER_KEY,
        CONSUMER_SECRET);

    // Get our request token with the URI scheme "noted://"
    requestToken = await oauthClient.GetRequestTokenAsync("noted://");

    // open up the authenticateUrl in the user's default browser
    // var authenticateUrl = "https://www.tumblr.com/oauth/authorize?oauth_token=" + requestToken.Key;
    var authenticateUrl = oauthClient.GetAuthorizeUrl(requestToken).AbsoluteUri;
    System.Diagnostics.Process.Start(authenticateUrl);
}

// this method is called when the user allows access through the web browser
// this will look different depending on what your platform uses to open URI schemes
[SomeFlagThatTellsThisMethodWhatToOpen]
async void OnAllow(string data)
{
    // get our access token and instantiate a new TumblrClient
    Token accessToken = await oauthClient.GetAccessTokenAsync(requestToken, data);
    TumblrClient client = new TumblrClientFactory().Create<TumblrClient>(CONSUMER_KEY, CONSUMER_SECRET, accessToken);

    // call a method that requires authentication
    UserInfo info = await client.GetUserInfoAsync();
}
```
  
## Example for WinForms / Asp.Net
*** 

- [WinForms](https://github.com/piedoom/TumblrSharp/tree/master/Examples/.Net%20Framework/Windows/Authenticate)
- [Asp.Net]()