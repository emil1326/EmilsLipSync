using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WizardScript : MonoBehaviour
{
    public TMP_InputField folderemplacement;
    public Toggle faceshakeTOG;
    public Toggle bodyshakeTOG;
    public Slider camerazoom;
    public Slider backgroundzoom;
    public Slider avatarzoom;
    public Slider faceshakeammount;
    public Slider bodyshakeammount;
    public Button Finished;
    public Button abort;
    public Button acceptpath;
    public TextMeshProUGUI logger;
    public TMP_InputField avatarname;
    public TMP_InputField blinkduration;
    public TMP_InputField blinkinterval;
    public Toggle randomblink;

    public GameObject background;
    public GameObject mouth;
    public GameObject face;
    public GameObject body;
    public GameObject outline;

    public Material[] allmats;

    private string[] filesinDIR;
    private string folder;
    private int imagecount;

    private void Start()
    {
        Finished.onClick.AddListener(CheckAndRun);
        abort.onClick.AddListener(Abort);
        acceptpath.onClick.AddListener(PathAcc);
        faceshakeTOG.onValueChanged.AddListener(delegate { Mod1(); });
        bodyshakeTOG.onValueChanged.AddListener(delegate { Mod2(); });

        camerazoom.onValueChanged.AddListener(PreviewChanged_cam);
        backgroundzoom.onValueChanged.AddListener(PreviewChanged_bac);
        avatarzoom.onValueChanged.AddListener(PreviewChanged_avi);
    }

    void PathAcc()
    {
        folder = folderemplacement.text;
        filesinDIR = Directory.GetFiles(folder);

        foreach (string dir in filesinDIR)
        {
            string fileName = Path.GetFileName(dir).Trim();
            if (Path.GetExtension(fileName) != ".png" && Path.GetExtension(fileName) != ".PNG")
            {
                logger.text += "Not all images are .png files." + "\n";
                break;
            }
            imagecount++;
        }

        if (imagecount != allmats.Length)
        {
            logger.text += "not the right ammount of images with .png extentions" + "\n";
        }

        string[] validNames = new string[] { "sil", "pp", "ff", "th", "dd", "kk", "ch", "ss", "nn", "rr", "aa", "e", "i", "o", "u", "face", "face-close", "body", "background" };

        foreach (string dir in filesinDIR)
        {
            string fileName = Path.GetFileNameWithoutExtension(dir);
            if (!validNames.Contains(fileName))
            {
                logger.text += "Invalid file name: " + fileName + "\n";
            }
        }

        if (logger.text == string.Empty)
        {
            Texture2D TextBackground = LoadImage(folder, "background");

            background.GetComponent<RawImage>().texture = TextBackground;

            Texture2D Textbody = LoadImage(folder, "body");

            body.GetComponent<RawImage>().texture = Textbody;

            Texture2D Textface = LoadImage(folder, "face");

            face.GetComponent<RawImage>().texture = Textface;

            Texture2D TextMouth = LoadImage(folder, "sil");

            mouth.GetComponent<RawImage>().texture = TextMouth;
        }
        else
        {
            logger.text += "Images not loaded because of an error";
        }
    }

    Texture2D LoadImage(string emplacement, string filename)
    {
        Texture2D newTexture = new(2, 2);
        byte[] imageData;
        try
        {
            imageData = File.ReadAllBytes(emplacement + "/" + filename + ".png");
        }
        catch
        {
            imageData = File.ReadAllBytes(emplacement + "/" + filename + ".PNG");
        }
        newTexture.LoadImage(imageData);

        return newTexture;
    }

    void PreviewChanged_cam(float sz)
    {
        outline.transform.localScale = new Vector3(sz, sz, sz);
    }

    void PreviewChanged_bac(float sz)
    {
        background.transform.localScale = new Vector3(sz, sz, sz);
    }

    void PreviewChanged_avi(float sz)
    {
        mouth.transform.localScale = new Vector3(sz, sz, sz);
        face.transform.localScale = new Vector3(sz, sz, sz);
        body.transform.localScale = new Vector3(sz, sz, sz);
    }

    void Abort()
    {
        this.gameObject.SetActive(false);
    }

    void Mod1()
    {
        if (faceshakeTOG.isOn)
        {
            faceshakeammount.interactable = true;
        }
        else
        {
            faceshakeammount.interactable = false;
        }
    }

    void Mod2()
    {
        if (bodyshakeTOG.isOn)
        {
            bodyshakeammount.interactable = true;
        }
        else
        {
            bodyshakeammount.interactable = false;
        }
    }

    public static void CompressFolder(string sourceFolder, string destinationArchive)
    {
        ZipFile.CreateFromDirectory(sourceFolder, destinationArchive, System.IO.Compression.CompressionLevel.Optimal, true);
    }

    private void CheckAndRun()
    {
        logger.text = string.Empty;

        //write info
        using StreamWriter writer = new(folder + "/AvatarData.txt");
        writer.WriteLine(DateTime.Now);
        writer.WriteLine(faceshakeTOG.isOn);
        writer.WriteLine(bodyshakeTOG.isOn);
        writer.WriteLine(camerazoom.value);
        writer.WriteLine(backgroundzoom.value);
        writer.WriteLine(avatarzoom.value);
        writer.WriteLine(faceshakeammount.value);
        writer.WriteLine(bodyshakeammount.value);
        writer.WriteLine(avatarname.text);
        writer.WriteLine(blinkduration.text);
        writer.WriteLine(blinkinterval.text);
        writer.WriteLine(randomblink.isOn);
        writer.Close();

        string newfolderemplacement = Directory.GetParent(folder).FullName + "/" + avatarname.text;

        Directory.Move(folder, newfolderemplacement);

        CompressFolder(newfolderemplacement, Directory.GetParent(folder).FullName + "/" + avatarname.text + ".2dAvi");

        this.gameObject.SetActive(false);
    }
}
