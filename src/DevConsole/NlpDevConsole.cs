using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NasLandingPage.Common.Extensions;
using NasLandingPage.Common.Models.Requests;
using NasLandingPage.Common.Models.Responses;
using NasLandingPage.Common.Providers;
using NasLandingPage.Common.Services;
using NLog.Extensions.Logging;

namespace DevConsole;

public class NlpDevConsole
{
  private readonly IServiceProvider _services;

  public NlpDevConsole()
  {
    _services = BuildServiceContainer();
  }

  public NlpDevConsole DoNothing()
  {
    return this;
  }
  
  public NlpDevConsole TestGetCredentials()
  {
    var credentialsService = _services.GetRequiredService<ICredentialsService>();
    var credentials = credentialsService.GetCredentials("github");
    Console.WriteLine($"{credentials.Username}|{credentials.Password}");
    return this;
  }
  
  public NlpDevConsole TestProjectSync()
  {
    var projectsService = _services.GetRequiredService<IProjectsService>();

    var commandResponse = projectsService.SyncProject(new RunCommandRequest
    {
      Command = "SyncProject",
      Arguments = "DnsUpdater"
    }).GetAwaiter().GetResult();


    Console.WriteLine(commandResponse.Success);
    return this;
  }

  public NlpDevConsole TestImages()
  {
    AddLink();

    var linkProvider = _services.GetRequiredService<IUserLinkProvider>();
    var userLink = linkProvider.GetById("9c610095fc9b4fcb8108351b7ff8d93b").GetAwaiter().GetResult();
    if (userLink is null)
      throw new Exception("Wrong link ID");

    var imageProvider = _services.GetRequiredService<ILinkImageProvider>();
    var imagePath = imageProvider.ResolveImagePath(userLink.Image);


    Console.WriteLine();
    return this;
  }

  private static IServiceProvider BuildServiceContainer()
  {
    var services = new ServiceCollection();

    var config = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", true, true)
      .Build();

    services
      // Configuration
      .AddSingleton<IConfiguration>(config)
      .AddNasLandingPage()

      // Logging
      .AddLogging(loggingBuilder =>
      {
        // configure Logging with NLog
        loggingBuilder.ClearProviders();
        loggingBuilder.SetMinimumLevel(LogLevel.Trace);
        loggingBuilder.AddNLog(config);
      });

    return services.BuildServiceProvider();
  }

  private void AddLink()
  {
    /*
     * <div class="wrapper">
    <div class="links-wrapper">
      <h1>Common</h1>
      <div class="links-new">
        <div class="link">
          <a href="http://192.168.0.60:5002/" target="_blank">
            <img src="./images/my-home.png" />
          </a>
        </div>
        <div class="link">
          <a href="https://niemandr.duckdns.org/" target="_blank">
            <img src="./images/hass.png" />
          </a>
        </div>
        <div class="link">
          <a href="http://192.168.0.60:3000/" target="_blank">
            <img src="./images/grafana.png" />
          </a>
        </div>
        <div class="link">
          <a href="http://192.168.0.60:9001/" target="_blank">
            <img src="./images/sonarqube.png" />
          </a>
        </div>
        <div class="link">
          <a href="http://192.168.0.60:32400/web/" target="_blank">
            <img src="./images/plex.png" />
          </a>
        </div>
        <div class="link">
          <a href="http://192.168.0.60:5080/login/" target="_blank">
            <img src="./images/sabnzbd.png" />
          </a>
        </div>
        <div class="link">
          <a href="http://192.168.0.60:5989/sonarr/" target="_blank">
            <img src="./images/sonarr.png" />
          </a>
        </div>
        <div class="link">
          <a href="http://192.168.0.60:5878/radarr/" target="_blank">
            <img src="./images/radarr.png" />
          </a>
        </div>
        <div class="link">
          <a href="http://192.168.0.60/" target="_blank">
            <img src="./images/unraid.png" />
          </a>
        </div>
        <div class="link">
          <a href="https://niemandr.atlassian.net/" target="_blank">
            <img src="./images/confluence.jpg" />
          </a>
        </div>
      </div>

      <h1>Misc.</h1>
      <div class="links-new">
        <div class="link">
          <a href="https://alexa.amazon.ca/spa/index.html#smart-home" target="_blank">
            <img src="./images/alexa.jpg" />
          </a>
        </div>
        <div class="link">
          <a href="http://192.168.0.60:6080/vnc.html?resize=remote&host=192.168.0.60&port=6080&autoconnect=1" target="_blank">
            <img src="./images/krusader.png" />
          </a>
        </div>
        <div class="link">
          <a href="http://192.168.0.60:7810/" target="_blank">
            <img src="./images/crashplan.png" />
          </a>
        </div>
        <div class="link">
            <a href="http://192.168.0.60:7807/" target="_blank">
            <img src="./images/jdownloader.jpg" />
          </a>
        </div>
        <div class="link">
          <a href="https://192.168.0.169:8443/" target="_blank">
            <img src="./images/unifi.png" />
          </a>
        </div>
        <div class="link">
          <a href="https://192.168.0.60:18043/" target="_blank">
            <img src="./images/omada.png" />
          </a>
        </div>
        <div class="link">
          <a href="http://192.168.0.241/" target="_blank">
            <img src="./images/octoprint.png" />
          </a>
        </div>
     */

    _services.GetRequiredService<IUserLinkProvider>().AddLink(new UserLink
    {
      Name = "SonoffRF",
      Url = "http://192.168.0.201/",
      Image = "sonoff-rf.jpg"
    });

    Console.WriteLine();
  }
}
