using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using FluentResults;

namespace Troupon.Catalog.Integration.Tests
{
  public class FluentResult
  {
    [DataMember(Name = "isFailed")]
    public bool IsFailed { get; set; }

    [DataMember(Name = "isSuccess")]
    public bool IsSuccess { get; set; }

    [DataMember(Name = "reasons")]
    public Reason[] Reasons { get; set; }

    [DataMember(Name = "errors")]
    public Error[] Errors { get; set; }

    [DataMember(Name = "successes")]
    public string[] Succesess { get; set; }


  }
  // public class ReasonObject
  // {

  //   [DataMember(Name = "message")]
  //   public string Message { get; set; }

  //   [DataMember(Name = "metadata")]
  //   public object Metadata { get; set; }
  // }
  // public class ErrorObject
  // {

  //   [DataMember(Name = "reasons")]
  //   public ReasonObject[] Reasons { get; set; }
  //   [DataMember(Name = "message")]
  //   public string Message { get; set; }

  //   [DataMember(Name = "metadata")]
  //   public object Metadata { get; set; }
  // }
}
