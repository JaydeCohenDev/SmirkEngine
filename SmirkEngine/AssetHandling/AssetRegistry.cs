namespace SmirkEngine.AssetHandling;

public static class AssetRegistry
{
    private static readonly Dictionary<string, IAsset> _assets = [];

    public static void RegisterAsset(string path, IAsset asset)
    {
        _assets.Add(path, asset);
    }

    public static T? GetAsset<T>(string path) where T : class, IAsset
    {
        if (!_assets.TryGetValue(path, out var asset))
            return null;
        
        return (T)asset;
    }

    public static bool AssetExists(string path)
    {
        return _assets.ContainsKey(path);
    }
}