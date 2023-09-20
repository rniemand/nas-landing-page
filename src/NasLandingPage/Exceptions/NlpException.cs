using System.Runtime.Serialization;

namespace NasLandingPage.Exceptions;

[Serializable]
public class NlpException : Exception
{
  public NlpException(string message)
    : base(message)
  { }

  public NlpException(string message, Exception inner)
    : base(message, inner)
  { }

  protected NlpException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    if (info == null)
      throw new ArgumentNullException(nameof(info));
  }
}
