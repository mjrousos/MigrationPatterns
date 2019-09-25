using Remoting.Models;
using Serilog;
using Serilog.Core;
using System;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace Remoting
{
    public class LoggingProxy<T> : RealProxy where T: MarshalByRefObject
    {
        private readonly T _target;
        private readonly Logger _logger;

        private LoggingProxy(T target) : base(typeof(T))
        {
            _target = target;

            // Setup the Serilog logger
            _logger = new LoggerConfiguration()
                .Destructure.ByTransforming<Product>(p => new { p.Name, p.Price })
                .WriteTo.Console().CreateLogger();
            _logger.Information("New RealProxy logging decorator created for object of type {TypeName}", typeof(T).FullName);
        }

        public static T Decorate(T target) =>
            new LoggingProxy<T>(target).GetTransparentProxy() as T;

        public override IMessage Invoke(IMessage msg)
        {
            if (msg is IMethodCallMessage callMessage)
            {
                _logger.Information("Calling method {TypeName}.{MethodName} with arguments {@Arguments}", callMessage.MethodBase.DeclaringType.Name, callMessage.MethodName, callMessage.Args);

                // Cache the method's arguments locally so that out and ref args can be updated at invoke time.
                var args = callMessage.Args;
                var result = callMessage.MethodBase.Invoke(_target, args);

                _logger.Information("Method {TypeName}.{MethodName} returned {@ReturnValue}", callMessage.MethodBase.DeclaringType.Name, callMessage.MethodName, result);

                return new ReturnMessage(result, args, args.Length, callMessage.LogicalCallContext, callMessage);
            }
            else
            {
                throw new ArgumentException("Invalid message; expected IMethodCallMessage", nameof(msg));
            }
        }
    }
}