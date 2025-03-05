namespace SmirkEngine.AssetHandling;

public static class Asset
{
    public static T Load<T>(string path) where T : class, IAsset
    {
        if(AssetRegistry.AssetExists(path))
            return AssetRegistry.GetAsset<T>(path)!;
        
        var asset = Activator.CreateInstance<T>();
        if (!asset.LoadFromFile(path))
        {
            throw new Exception($"Failed to load asset from path {path}");
        }
        AssetRegistry.RegisterAsset(path, asset);
        return asset;
    }
}