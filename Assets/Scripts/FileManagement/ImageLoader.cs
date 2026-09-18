using RavingBots.EdgePadding;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;


public class ImageLoader
{
    private OVRLipSyncContextTextureFlip flip;

    public void LoadFlip(OVRLipSyncContextTextureFlip ovrflip)
    {
        flip = ovrflip;
    }

    public async Task LoadMyImage(string path, string filename, int number)
    {
        Texture2D newTexture = new(2, 2, TextureFormat.RGBA32, false);
        byte[] imageData;
        try
        {
            imageData = await File.ReadAllBytesAsync(Path.Combine(path, $"{filename}.png"));
        }
        catch
        {
            imageData = await File.ReadAllBytesAsync(Path.Combine(path, $"{filename}.PNG"));
        }
        newTexture.LoadImage(imageData);
        newTexture.filterMode = FilterMode.Trilinear;

        if (PlayerPrefs.GetInt("IsHQ") == 0)
        {
            newTexture.Compress(true);
        }

        newTexture.name = filename;
        newTexture.Apply();

        newTexture = newTexture.Clone();
        newTexture.ApplyPadding(PaddingMode.Simple, 0, 10);

        flip.Textures[number] = newTexture;
    }

    public async Task LoadMyImage(string path, string filename, Material mat)
    {
        Texture2D newTexture = new(2, 2, TextureFormat.RGBA32, false);
        byte[] imageData;
        try
        {
            imageData = await File.ReadAllBytesAsync(Path.Combine(path, $"{filename}.png"));
        }
        catch
        {
            imageData = await File.ReadAllBytesAsync(Path.Combine(path, $"{filename}.PNG"));
        }
        newTexture.LoadImage(imageData);
        newTexture.filterMode = FilterMode.Trilinear;

        if (PlayerPrefs.GetInt("IsHQ") == 0)
        {
            newTexture.Compress(true);
        }

        newTexture.name = filename;
        newTexture.Apply();

        newTexture = newTexture.Clone();
        newTexture.ApplyPadding(PaddingMode.Simple, 0, 10);

        mat.mainTexture = newTexture;
    }
}