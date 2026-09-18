using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ListWriter : MonoBehaviour
{
    public List<string> IDs = new List<string>();
    public List<string> Names = new List<string>();
    public List<string> Paths = new List<string>();

    public void AddPacket(int id, string name, string path, GameObject LastCaller)
    {
        if (IDs.Count == 15)
        {
            IDs.Clear();
            Names.Clear();
            Paths.Clear();
        }

        IDs.Add(id.ToString());
        Names.Add(name);
        Paths.Add(path);

        if (LastCaller.GetComponent<AvatarLoaderSc>().selfID == 14)
        {
            using StreamWriter writer = new(Directory.GetParent(UnityEngine.Application.dataPath) + "/ManagedAvatar/" + "AvatarLoadData.txt");
            {
                writer.WriteLine(DateTime.Now.ToString());
                for (int i = 0; i < IDs.Count; i++)
                {
                    writer.WriteLine(IDs[i]);
                    writer.WriteLine(Names[i]);
                    writer.WriteLine(Paths[i]);
                }
                writer.WriteLine("Finished");
            }
        }
    }
}