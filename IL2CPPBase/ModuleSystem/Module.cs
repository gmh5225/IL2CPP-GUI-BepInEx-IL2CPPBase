using UnityEngine;

namespace IL2CPPBase.ModuleSystem
{
    public class Module
    {
        public readonly string Name;
        public readonly string Category;
        public readonly KeyCode KeyBind;
        public bool Enabled;

        protected Module(string name, string category, KeyCode keyBind = KeyCode.None)
        {
            Name = name;
            Category = category;
            KeyBind = keyBind;
        }
        
        protected virtual void OnEnable() { }

        protected virtual void OnDisable() { }
        
        public virtual void OnCheatLoad() {}
        
        public virtual void OnUpdate() {}
        
        public virtual void OnGUI() {}

        public void Toggle()
        {
            Enabled = !Enabled;
            switch (Enabled)
            {
                case true:
                    OnEnable();
                    break;
                case false:
                    OnDisable();
                    break;
            }
        }
    }
}