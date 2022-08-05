using IL2CPPBase.Misc;
using Steamworks;
using UnityEngine;

namespace IL2CPPBase.ModuleSystem.Visuals
{
    public class Gui : Module
    {
        public Gui() : base("GUI", "Visuals", KeyCode.Insert){}

        public static readonly Dictionary<int, Rect> WindowPos = new();

        public override void OnGUI()
        {
            var rainbow = Color.HSVToRGB(Mathf.PingPong(Time.time * 0.30f, 1), 1, 1);
            for (var i = 0; i < ModuleManager.Categories.Count; i++)
            {
                var moduleCount = CountModules(i);

                if (WindowPos.Count != ModuleManager.Categories.Count)
                {
                    WindowPos.Add(i, new Rect(120 + (i * 300), 300, 250, 90 * moduleCount));
                }
                
                if (moduleCount == 0) continue;

                GUI.contentColor = rainbow;
                GUI.backgroundColor = Color.black;
                WindowPos[i] = GUI.Window(i, WindowPos[i], (GUI.WindowFunction)CategoryWindowFunction, $"<b>{ModuleManager.Categories[i]}</b>");
            }
        }

        private static int CountModules(int i)
        {
            return ModuleManager.Modules.Count(module => module.Category == ModuleManager.Categories[i] && module.Name != "GUI");
        }
        
        private static void CategoryWindowFunction(int windowId)
        {
            var i = 0;
            foreach (var module in ModuleManager.Modules.Where(module => module.Category == ModuleManager.Categories[windowId]))
            {
                if (module.Name == "GUI") continue;
                
                if (module.Enabled)
                {
                    GUI.backgroundColor = Color.gray;
                    if (GUI.Button(new Rect(30, (60 * i) + 30, 200, 50), module.Name))
                    {
                        module.Toggle();
                    }
                }
                else
                {
                    GUI.backgroundColor = Color.black;
                    if (GUI.Button(new Rect(30, (60 * i) + 30, 200, 50), module.Name))
                    {
                        module.Toggle();
                    }
                }
                i++;
            }
            GUI.DragWindow();
        }
    }
}