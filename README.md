# BepInEx.AutoPlugin

BepInEx.AutoPlugin is an incremental C# source generator that takes the following properties from your project:

```xml
<PropertyGroup>
  <PackageId>com.example.ExamplePlugin</PackageId>
  <Product>ExamplePlugin</Product>
  <Version>0.1.0</Version>
  <Authors>AuthorName, OtherAuthor</Authors>
  <RepositoryUrl>https://github.com/example/ExamplePlugin</RepositoryUrl>
</PropertyGroup>
```

And generates a partial class for your partial plugin class decorated with the `BepInAutoPluginAttribute`, decorating the generated class with the `BepInPluginAttribute` using the above properties:

```cs
[BepInExResoniteShim.ResonitePlugin(ExamplePlugin.GUID, ExamplePlugin.NAME, ExamplePlugin.VERSION, ExamplePlugin.AUTHORS, ExamplePlugin.REPOSITORY_URL)]
partial class ExamplePlugin : BaseUnityPlugin
{
    public const string GUID = "com.example.ExamplePlugin";
    public const string NAME = "ExamplePlugin";
    public const string VERSION = "0.1.0";
    public const string AUTHORS = "AuthorName, OtherAuthor";
    public const string REPOSITORY_URL = "https://github.com/example/ExamplePlugin";
}
```

A `PatcherAutoPluginAttribute` also exists for BepInEx 6 preloader patchers.

## Usage

First, ensure you have the NuGet source configured. You can either add a `NuGet.Config` file to your project root:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="resonite-modding" value="https://nuget-modding.resonite.net/v3/index.json" />
  </packageSources>
</configuration>
```

Or add this to your csproj:

```xml
<PropertyGroup>
  <RestoreAdditionalProjectSources>
    https://nuget-modding.resonite.net/v3/index.json;
  </RestoreAdditionalProjectSources>
</PropertyGroup>
```

Then add the package reference to your csproj:

```xml
<ItemGroup>
  <PackageReference Include="BepInEx.AutoPlugin" Version="2.0.*" PrivateAssets="all" />
</ItemGroup>
```

Mark your plugin class partial, and decorate it with the `BepInAutoPluginAttribute`:

```cs
[BepInAutoPlugin]
public partial class ExamplePlugin : BasePlugin
{
}
```

And you're done! You don't need to manually include the `BepInPluginAttribute`, as the generated partial class includes that.

You can also access all the properties in your code, as they are public members in your class:

```cs
[BepInAutoPlugin]
public partial class ExamplePlugin : BasePlugin
{
    public override void Load()
    {
        Logger.LogInfo($"Plugin {Name} version {Version} is loaded!");
    }
}
```

### Overriding Properties

AutoPlugin allows overriding any of the properties with the optional attribute arguments:

```cs
[BepInAutoPlugin(id: "com.example.MyOverrideId", name: "My Override Name", version: "1.2.3", authors: "Author1, Author2", link: "https://example.com")]
public partial class ExamplePlugin : BasePlugin
{
}
```

### MSBuild Configuration

AutoPlugin allows stripping version build metadata to turn `1.0.0-beta+1234567890` into `1.0.0-beta` for the `Version` property by setting the following in your csproj:

```xml
<PropertyGroup>
  <BepInAutoPluginStripBuildMetadata>true</BepInAutoPluginStripBuildMetadata>
</PropertyGroup>
```

Note that this will do nothing on BepInEx 5 as it only accepts a version number without a pre-release version or build metadata, so it strips both.
