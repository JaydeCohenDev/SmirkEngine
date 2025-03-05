namespace SmirkEngine.AssetHandling;

public static class Asset
{
    public static T Load<T>(string path) where T : IAsset
    {
        var asset = Activator.CreateInstance<T>();
        if (!asset.LoadFromFile(path))
        {
            throw new Exception($"Failed to load asset from path {path}");
        }
        return asset;
    }
}