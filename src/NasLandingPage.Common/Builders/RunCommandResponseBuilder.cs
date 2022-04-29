using NasLandingPage.Common.Models.Requests;
using NasLandingPage.Common.Models.Responses;

namespace NasLandingPage.Common.Builders;

public class RunCommandResponseBuilder
{
  private readonly RunCommandResponse _response;
  private readonly DateTime _startTime;
  private readonly List<string> _messages;

  public RunCommandResponseBuilder()
  {
    _response = new RunCommandResponse();
    _startTime = DateTime.Now;
    _messages = new List<string>();
  }

  public RunCommandResponseBuilder(string command)
    : this()
  {
    _response.Command = command;
  }

  public RunCommandResponseBuilder(string command, string arguments)
    : this(command)
  {
    _response.Arguments = arguments;
  }

  public RunCommandResponseBuilder(RunCommandRequest request)
    : this(request.Command, request.Arguments)
  { }

  public RunCommandResponseBuilder WithSuccess(bool success)
  {
    _response.Success = success;
    return this;
  }

  public RunCommandResponseBuilder WithMessage(string message)
  {
    _messages.Add(message);
    return this;
  }

  public RunCommandResponse Failed() =>
    // TODO: [RunCommandResponseBuilder.Failed] (TESTS) Add tests
    WithSuccess(false).Build();

  public RunCommandResponse Failed(string message) =>
    // TODO: [RunCommandResponseBuilder.Failed] (TESTS) Add tests
    WithSuccess(false)
      .WithMessage(message)
      .Build();

  public RunCommandResponse Build()
  {
    // TODO: [RunCommandResponseBuilder.Build] (TESTS) Add tests
    _response.RunningTime = DateTime.Now - _startTime;
    _response.Messages = _messages.ToArray();
    return _response;
  }
}
