using DevConsole;

new NlpDevConsole()
  //.HelloWorld()
  //.TestGetCredentials()
  //.TestGitHubClient()
  //.TestProjectInfoProvider()
  .SyncRepoDefaultBranches()
  .DoNothing();

Console.WriteLine("Fin.");
