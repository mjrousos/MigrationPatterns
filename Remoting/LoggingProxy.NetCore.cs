using Remoting.Models;
using Serilog;
using Serilog.Core;
using System.Reflection;

namespace Remoting
{
    public class LoggingProxy<T> : DispatchProxy where T : class
    {
        private T _target;
        private Logger _logger;

        private void Initialize(T target)
        {
            _target = target;

            // Setup the Serilog logger
            _logger = new LoggerConfiguration()
                .Destructure.ByTransforming<Product>(p => new { p.Name, p.Price })
                .WriteTo.Console().CreateLogger();
            _logger.Information("New DispatchProxy logging decorator created for object of type {TypeName}", typeof(T).FullName);
        }

        public static T Decorate(T target)
        {
            var proxy = Create<T, LoggingProxy<T>>()
                as LoggingProxy<T>;

            proxy.Initialize(target);

            return proxy as T;
        }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            _logger.Information("Calling method {TypeName}.{MethodName} with arguments {@Arguments}", targetMethod.DeclaringType.Name, targetMethod.Name, args);

            var result = targetMethod.Invoke(_target, args);

            _logger.Information("Method {TypeName}.{MethodName} returned {@ReturnValue}", targetMethod.DeclaringType.Name, targetMethod.Name, result);

            return result;
        }
    }
}