using IL2CPPBase.ModuleSystem.Testing;
using IL2CPPBase.ModuleSystem.Visuals;

namespace IL2CPPBase.ModuleSystem
{
    public class ModuleManager
    {
        public static readonly List<Module> Modules = new();
        public static readonly List<string> Categories = new();
        
        public static void Initialize()
        {
            AddModule(new Gui());
            AddModule(new TestingWindow());
            AddModule(new SecondTestingWindow());
        }
        
        private static void AddModule(Module module)
        {
            Modules.Add(module);
            if (!Categories.Contains(module.Category)) Categories.Add(module.Category);
        }
    }
}