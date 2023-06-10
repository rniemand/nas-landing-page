using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace NasLandingPage.Auth;

class NlpControllerFeatureProvider : ControllerFeatureProvider
{
  protected override bool IsController(TypeInfo typeInfo)
  {
    if (!typeInfo.IsClass)
      return false;

    if (typeInfo.IsAbstract)
      return false;

    if (typeInfo.DeclaringType is not null)
      return false;

    if (typeInfo.ContainsGenericParameters)
      return false;

    if (typeInfo.IsDefined(typeof(NonControllerAttribute)))
      return false;

    if (!typeInfo.Name.EndsWith("Controller") &&
        !typeInfo.IsDefined(typeof(ControllerAttribute)))
      return false;

    return true;
  }
}
