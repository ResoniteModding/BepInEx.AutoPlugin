using BepInEx;
using BepInEx.Preloader.Core.Patching;

[BepInAutoPlugin]
public partial class PluginInGlobalNamespace { }

namespace Plugin
{
    [PatcherAutoPlugin(id: "my id", name: "my name", version: "my version")]
    public partial class MyPluginWithOverrides { }

    // [PatcherAutoPlugin]
    public partial class MyPatcherPlugin { }

    namespace Nested
    {
        [BepInAutoPlugin]
        public partial class NestedPlugin
        {
            public void Load()
            {
                Console.WriteLine($"Plugin {NAME} version {VERSION} is loaded!");
            }
        }
    }
}
