using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AvatarLoaderSc : MonoBehaviour
{
    public Button load;
    public Button delete;
    public TextMeshProUGUI aviname;
    public Material[] imagestoload;
    public int selfID;
    public ListWriter LW;

    public GameObject model;

    public Texture[] imageslist;

    private string pathtokeep = string.Empty;

    void Start()
    {
        load.onClick.AddListener(LoadAvi);
        delete.onClick.AddListener(DeleteLine);
    }

    public void AddAviInfoToList(string path, string name, bool addthen)
    {
        pathtokeep = path;
        aviname.text = name;

        if (addthen) LoadAvi();
    }

    public async void LoadAvi()
    {
        ImageLoader IMGLoad = new();

        var flip = FindAnyObjectByType<OVRLipSyncContextTextureFlip>();
        IMGLoad.LoadFlip(flip);

        var loaders = new Task[19];

        loaders[0] = IMGLoad.LoadMyImage(pathtokeep, "sil", 0);
        loaders[1] = IMGLoad.LoadMyImage(pathtokeep, "pp", 1);
        loaders[2] = IMGLoad.LoadMyImage(pathtokeep, "ff", 2);
        loaders[3] = IMGLoad.LoadMyImage(pathtokeep, "th", 3);
        loaders[4] = IMGLoad.LoadMyImage(pathtokeep, "dd", 4);
        loaders[5] = IMGLoad.LoadMyImage(pathtokeep, "kk", 5);
        loaders[6] = IMGLoad.LoadMyImage(pathtokeep, "ch", 6);
        loaders[7] = IMGLoad.LoadMyImage(pathtokeep, "ss", 7);
        loaders[8] = IMGLoad.LoadMyImage(pathtokeep, "nn", 8);
        loaders[9] = IMGLoad.LoadMyImage(pathtokeep, "rr", 9);
        loaders[10] = IMGLoad.LoadMyImage(pathtokeep, "aa", 10);
        loaders[11] = IMGLoad.LoadMyImage(pathtokeep, "e", 11);
        loaders[12] = IMGLoad.LoadMyImage(pathtokeep, "i", 12);
        loaders[13] = IMGLoad.LoadMyImage(pathtokeep, "o", 13);
        loaders[14] = IMGLoad.LoadMyImage(pathtokeep, "u", 14);
        loaders[15] = IMGLoad.LoadMyImage(pathtokeep, "face", imagestoload[15]);
        try
        {
            loaders[16] = IMGLoad.LoadMyImage(pathtokeep, "face-close", imagestoload[16]);
        }
        catch
        {
            loaders[16] = IMGLoad.LoadMyImage(pathtokeep, "face", imagestoload[16]);
        }
        loaders[17] = IMGLoad.LoadMyImage(pathtokeep, "body", imagestoload[17]);
        loaders[18] = IMGLoad.LoadMyImage(pathtokeep, "background", imagestoload[18]);

        await Task.WhenAll(loaders);


        StreamReader reader = new(pathtokeep + "/AvatarData.txt");

        List<string> data = new();

        for (int i = 0; i < 12; i++)
        {
            data.Add(reader.ReadLine());
        }
        model.GetComponent<ModelSC>().LoadData(data);

        reader.Close();

        Resources.UnloadUnusedAssets();
    }

    void DeleteLine()
    {
        pathtokeep = string.Empty;
        aviname.text = string.Empty;

        LW.Names[selfID] = string.Empty;
        LW.Paths[selfID] = string.Empty;
    }

    public bool IsFree()
    {
        if (pathtokeep == string.Empty) return true; else return false;
    }

    private void OnDisable()
    {
        LW.AddPacket(selfID, aviname.text, pathtokeep, this.gameObject);
    }
}
