using BepInEx;
using RogueLibsCore;

namespace Itemized
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency(RogueLibs.GUID, RogueLibs.CompiledVersion)]
    public sealed class ItemizedPlugin : BaseUnityPlugin
    {
        public const string PluginGuid = @"abbysssal.streetsofrogue.itemized";
        public const string PluginName = "Itemized";
        public const string PluginVersion = "1.0.0";

        public new static BepInEx.Logging.ManualLogSource Logger = null!;
        public static void Log(object message) => Logger.LogWarning(message);

        private static RoguePatcher patcher = null!;
        public static RoguePatcher GetPatcher<T>()
        {
            patcher.TypeWithPatches = typeof(T);
            return patcher;
        }

        public void Awake()
        {
            Logger = base.Logger;
            patcher = new RoguePatcher(this);
            RogueLibs.LoadFromAssembly();
            patcher.TypeWithPatches = GetType();

        }

}
}
