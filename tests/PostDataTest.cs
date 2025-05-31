using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading.Tasks;

namespace TestTumblrSharp;

[TestClass]
public class PostDataTest
{
    private const string BLOGNAME = "newtsharp.tumblr.com";

    private const string AUDIO_URL = "https://freetestdata.com/wp-content/uploads/2021/09/Free_Test_Data_5MB_MP3.mp3";
    private const string AUDIO_FILE = @"..\..\..\..\demo_daten\Free_Test_Data_5MB_MP3.mp3";

    private const string PHOTO_URL = "https://www.burosch.de/images/TV_Bildoptimierer/Burosch_universaltestbild_avec-3840x2160.jpg";
    private const string PHOTO_FILE = @"..\..\..\..\demo_daten\Burosch_universaltestbild_avec-3840x2160.jpg";

    private const string LINK_URL = "https://github.com/piedoom/TumblrSharp/";

    private const string VIDEO_FILE = @"..\..\..\..\demo_daten\SampleVideo_360x240_30mb.mp4";

    [TestMethod]
    public async Task AudioPost_Url()
    {
        using TumblrClient tumblrClient = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, Settings.AccessToken);

        PostData postData = PostData.CreateAudio(AUDIO_URL, "Audio: Testbeispiel", ["audio", "test"]);

        var result = await tumblrClient.CreatePostAsync(BLOGNAME, postData);

        Assert.IsNotNull(result, "Post creation failed, result is null.");
    }

    [TestMethod]
    public async Task AudioPost_File()
    {
        using TumblrClient tumblrClient = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, Settings.AccessToken);

        using var fileStream = File.OpenRead(AUDIO_FILE);

        var audioFile = new BinaryFile(fileStream, "file", "audio/mpeg");

        PostData postData = PostData.CreateAudio(audioFile, "Audio: Testbeispiel", ["audio", "test"]);

        var result = await tumblrClient.CreatePostAsync(BLOGNAME, postData);

        Assert.IsNotNull(result, "Post creation failed, result is null.");
        Assert.IsTrue(result.PostId > 0, "Post creation failed, PostId is not greater than 0.");

        await tumblrClient.DeletePostAsync(BLOGNAME, result.PostId);
    }

    [TestMethod]
    public async Task PhotoPost_File()
    {
        using TumblrClient tumblrClient = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, Settings.AccessToken);

        using var fileStream = File.OpenRead(PHOTO_FILE);

        var bildFile = new BinaryFile(fileStream);

        PostData postData = PostData.CreatePhoto(bildFile, "Bild: Testbeispiel", LINK_URL, ["bild", "test", "testbild"]);

        var result = await tumblrClient.CreatePostAsync(BLOGNAME, postData);

        Assert.IsNotNull(result, "Post creation failed, result is null.");
    }

    [TestMethod]
    public async Task PhotoPost_Link()
    {
        using TumblrClient tumblrClient = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, Settings.AccessToken);

        PostData postData = PostData.CreatePhoto(PHOTO_URL, "Bild: Testbeispiel", LINK_URL, ["bild", "test", "testbild"]);

        var result = await tumblrClient.CreatePostAsync(BLOGNAME, postData);

        Assert.IsNotNull(result, "Post creation failed, result is null.");
    }

    [TestMethod]
    public async Task LinkPost_Url()
    {
        using TumblrClient tumblrClient = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, Settings.AccessToken);

        PostData postData = PostData.CreateLink(LINK_URL, "Link: Testbeispiel", "dies ist ein Beispiel für ein LinkPost", PHOTO_URL, "dies ist ein Auszug", "Ulf (Cataurus) Prill",  ["Link", "test"]);

        var result = await tumblrClient.CreatePostAsync(BLOGNAME, postData);

        Assert.IsNotNull(result, "Post creation failed, result is null.");
    }

    [TestMethod]
    public async Task VideoPost_File()
    {
        using TumblrClient tumblrClient = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, Settings.AccessToken);

        using var fileStream = File.OpenRead(VIDEO_FILE);

        var videoFile = new BinaryFile(fileStream, "file", "video/mp4");

        PostData postData = PostData.CreateVideo(videoFile, "Send Video: Testbeispiel", ["Video", "test"]);

        var result = await tumblrClient.CreatePostAsync(BLOGNAME, postData);

        Assert.IsNotNull(result, "Post creation failed, result is null.");
    }

    [TestMethod]
    public async Task VideoPost_Url()
    {
        using TumblrClient tumblrClient = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, Settings.AccessToken);

        string emmbedVideo = "<iframe width = \"560\" height=\"315\" src=\"https://www.youtube.com/embed/hFd1Kqkh2-k?si=cdRjCNfUzjXZnypY\" title=\"YouTube video player\" frameborder=\"0\" allow=\"accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share\" referrerpolicy=\"strict-origin-when-cross-origin\" allowfullscreen></iframe>";
        
        PostData postData = PostData.CreateVideo(emmbedVideo, "Send embed Video: Testbeispiel", ["Video", "test"]);

        var result = await tumblrClient.CreatePostAsync(BLOGNAME, postData);

        Assert.IsNotNull(result, "Post creation failed, result is null.");
    }

    [TestMethod]
    public async Task ChatPost()
    {
        using TumblrClient tumblrClient = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, Settings.AccessToken);

        PostData postData = PostData.CreateChat("Es gibt eine komische Sache, bei AudioFiles....", "Chatbeispiel: Audio", ["Chat", "test"]);

        var result = await tumblrClient.CreatePostAsync(BLOGNAME, postData);

        Assert.IsNotNull(result, "Post creation failed, result is null.");
    }

    [TestMethod]
    public async Task QuotePost()
    {
        using TumblrClient tumblrClient = new TumblrClientFactory().Create<TumblrClient>(Settings.consumerKey, Settings.consumerSecret, Settings.AccessToken);

        PostData postData = PostData.CreateQuote("Dies ist ein schlauer Satz", "aus den unergründlichen Weisheiten", ["Quote", "test"]);

        var result = await tumblrClient.CreatePostAsync(BLOGNAME, postData);

        Assert.IsNotNull(result, "Post creation failed, result is null.");
    }

}
