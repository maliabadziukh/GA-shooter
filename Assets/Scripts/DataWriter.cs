using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class DataWriter : MonoBehaviour
{
    public List<string> headers = new() { "Generation N", "health", "damage", "speed", "bullet speed", "reload speed" };
    private string filePath;


    void Start()
    {
        string timeStamp = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        filePath = "EvolutionData/data_" + timeStamp + ".csv";
        WriteHeaders();
    }

    // Update is called once per frame
    void WriteHeaders()
    {
        StringBuilder sb = new();
        foreach (string header in headers)
        {
            sb.Append(header);
            sb.Append(",");
        }
        sb.AppendLine();
        File.WriteAllText(filePath, sb.ToString());
        Debug.Log("Headers written to: ");
    }

    public void WriteRow(int generationNumber, List<float> avgDNA)
    {
        StringBuilder sb = new();
        sb.Append(generationNumber.ToString());
        sb.Append(",");
        foreach (float gene in avgDNA)
        {
            sb.Append(gene.ToString());
            sb.Append(",");
        }
        sb.AppendLine();
        File.AppendAllText(filePath, sb.ToString());
    }


}


