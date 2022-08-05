using UnityEngine;
using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using IL2CPPBase.Misc;
using IL2CPPBase.ModuleSystem;

namespace IL2CPPBase
{
    [BepInPlugin("IL2CPPBase", "IL2CPPBase", "1.0.0.0")]
    public class Plugin : BasePlugin
    {
        public override void Load()
        {
            Logs.Logger = Log;
            IL2CPPBase.Initialize(this);
        }
    }

    public class IL2CPPBase : MonoBehaviour
    {
        public static Harmony Harmony = new("IL2CPPBase");
        
        public static void Initialize(Plugin plugin)
        {
            var addComponent = plugin.AddComponent<IL2CPPBase>();
            addComponent.hideFlags = HideFlags.HideAndDontSave;
            DontDestroyOnLoad(addComponent.gameObject);
        }
        
        private void Start()
        {
            ModuleManager.Initialize();
            foreach (var module in ModuleManager.Modules)
            {
                Logs.Logger.LogInfo($"{module.Name} was Initialized");
                module.OnCheatLoad();
            }
        }
        
        private void Update()
        {
            foreach (var module in ModuleManager.Modules.Where(module => Input.GetKeyDown(module.KeyBind)))
            {
                module.Toggle();
            }
                
            foreach (var module in ModuleManager.Modules.Where(m => m.Enabled))
            {
                module.OnUpdate();
            }
        }
            
        private void OnGUI()
        {
            foreach (var module in ModuleManager.Modules.Where(m => m.Enabled))
            {
                module.OnGUI();
            }
        }
    }
}