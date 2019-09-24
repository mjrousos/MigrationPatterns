using System.Reflection;
using System.Runtime.Loader;

namespace AppDomains
{
    class SampleAssemblyLoadContext : AssemblyLoadContext
    {
        private AssemblyDependencyResolver resolver;

        public SampleAssemblyLoadContext(string name) : base(name, isCollectible: true)
        {
            // An AssemblyDependencyResolver will use the .deps.json file to determine
            // assembly's paths. Custom resolver logic could be used here, too.
            resolver = new AssemblyDependencyResolver(typeof(SampleAssemblyLoadContext).Assembly.Location);
        }

        // This method is called when an assembly needs to be implicitly loaded
        // (as a dependency of another assembly, for example)
        protected override Assembly Load(AssemblyName assemblyName)
        {
            var assemblyPath = resolver.ResolveAssemblyToPath(assemblyName);

            // Null indicates that the assembly can't be or shouldn't be loaded into the
            // assembly load context and it will be loaded into the default context, instead.
            return (assemblyPath != null) ? LoadFromAssemblyPath(assemblyPath) : null;
        }
    }
}
