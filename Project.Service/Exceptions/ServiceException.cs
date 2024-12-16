using System;

namespace Project.Service.Exceptions;

public class ServiceException : Exception
{
  public ServiceException(string message) : base(message)
  {

  }
}
