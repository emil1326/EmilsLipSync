using SimpleFileBrowser;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class AviSelectionSc : MonoBehaviour
{
    public Button addAvi;
    public GameObject[] SaveSpot;

    private List<string> filePaths = new List<string>();
    private string executablePath;
    private string FileIUse;

    // Start is called before the first frame update
    void Start()
    {
        executablePath = Path.GetDirectoryName(UnityEngine.Application.dataPath);
        addAvi.onClick.AddListener(AddAvi);

        FileBrowser.SetFilters(true, new FileBrowser.Filter("Avatars", ".2dAvi"));
    }

    private void OnEnable()
    {
        LoadList();
    }

    void LoadList()
    {
        string path = Directory.GetParent(UnityEngine.Application.dataPath) + "/ManagedAvatar/" + "AvatarLoadData.txt";

        try
        {


            using (StreamReader sr = new(path))
            {
                string currentLine = sr.ReadLine();
                while (currentLine != "Finished")
                {
                    string newcurrentLine = sr.ReadLine();
                    if (newcurrentLine == string.Empty)
                    {
                        return;
                    }
                    else
                    {
                        int[] validNB = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
                        int nbread = int.Parse(newcurrentLine);

                        if (validNB.Contains(nbread))
                        {
                            string thename = sr.ReadLine();
                            string thepath = sr.ReadLine();
                            SaveSpot[nbread].GetComponent<AvatarLoaderSc>().AddAviInfoToList(thepath, thename, false);
                        }
                    }
                    currentLine = newcurrentLine;
                }
            }
        }
        catch 
        {
            Debug.LogError("couldnt read properly file");
        }
    }

    IEnumerator ShowLoadDialogCoroutine()
    {
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, true, null, ".2dAvi", "Load Files", "Load");

        if (FileBrowser.Success)
        {

            FileIUse = FileBrowser.Result[0];

            Directory.CreateDirectory(executablePath + "/ManagedAvatar/");

            using (ZipArchive archive = ZipFile.OpenRead(FileIUse))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    ExtractFilesRecursively(entry, executablePath + "/ManagedAvatar/");
                }
            }

        }

        // add avi with the conditions

        string fullPath = FileBrowserHelpers.GetFilename(FileBrowser.Result[0]);
        string fileName = Path.GetFileNameWithoutExtension(fullPath);

        for (int i = 0; i < 15; i++)
        {
            if (SaveSpot[i].GetComponent<AvatarLoaderSc>().IsFree())
            {
                SaveSpot[i].GetComponent<AvatarLoaderSc>().AddAviInfoToList(executablePath + "/ManagedAvatar/" + fileName, fileName, true);
                break;
            }
        }
    }


    void AddAvi()
    {
        StartCoroutine(ShowLoadDialogCoroutine());
    }

    void ExtractFilesRecursively(ZipArchiveEntry entry, string extractPath)
    {
        // Get the full path of the file
        string filePath = Path.Combine(extractPath, entry.FullName);

        if (entry.FullName.EndsWith("/"))
        {
            // If the entry is a directory, create it
            Directory.CreateDirectory(filePath);
            // Recursively extract files in the subdirectory
            foreach (ZipArchiveEntry subEntry in entry.Archive.Entries.Where(e => e.FullName.StartsWith(entry.FullName)))
            {
                ExtractFilesRecursively(subEntry, extractPath);
            }
        }
        else
        {
            // Create the directory if it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            // Extract the file
            entry.ExtractToFile(filePath, true);
            // Add the file path to the list
            filePaths.Add(filePath);
        }
    }
}
