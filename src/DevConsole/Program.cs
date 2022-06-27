using DevConsole;

new NlpDevConsole()
  //.TestGetCredentials()
  //.TestProjectSync()
  //.FollowLink()
  //.GetAllLinks()
  //.GetLinkCategories()
  .UpdateFollowed(1)
  .DoNothing();

Console.WriteLine("Fin.");
