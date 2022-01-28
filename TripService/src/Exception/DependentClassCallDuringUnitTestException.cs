using System;
using System.Runtime.Serialization;

namespace TripService.Exception;

[Serializable]
public class DependentClassCallDuringUnitTestException : System.Exception
{
    public DependentClassCallDuringUnitTestException() { }

    public DependentClassCallDuringUnitTestException(string message, System.Exception innerException) : base(message, innerException) { }
    public DependentClassCallDuringUnitTestException(string message) : base(message) { }

    private DependentClassCallDuringUnitTestException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}

