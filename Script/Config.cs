using UnityEngine;
using System.Collections;
using File = System.IO.File;
using System.IO;
using System.Text;
using System;

public class Config : MonoBehaviour {

    public static string filename = "config.ini";
    public static string ipAddress;
    public static string port;

    void Start()
    {
        loadFile(filename);
        // Other stuff 
    }

    void loadFile(string filename)
    {
        if (!File.Exists(filename))
        {
            File.CreateText(filename);
            return;
        }

        try
        {
            string line;
            StreamReader sReader = new StreamReader(filename, Encoding.Default);
            do
            {
                line = sReader.ReadLine();
                if (line != null)
                {
                    // Lines with # are for comments
                    if (!line.Contains("#"))
                    {
                        // Value property identified by string before the colon.
                        string[] data = line.Split(':');
                        if (data.Length == 2)
                        {
                            switch (data[0])
                            {
                                case "IP Address":
                                    Debug.Log("IP Address: " + data[1]);
                                    ipAddress = data[1].Trim();
                                    break;
                                case "Port":
                                    Debug.Log("Port: " + data[1]);
                                    port = data[1].Trim();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            while (line != null);
            sReader.Close();
            return;
        }
        catch (Exception e)
        {
        }
    }
}
