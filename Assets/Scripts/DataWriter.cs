using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class DataWriter : MonoBehaviour
{
    public List<string> dnaHeaders = new() { "health", "damage", "speed", "bullet speed", "reload speed" };
    public List<string> startingParamHeaders = new() { "Wave duration", "Enemy N", "Mutation rate", "Mutation Amount" };

    public string avgFilePath;
    public string breakdownFilePath;


    void Start()
    {
        GameController gameController = GetComponent<GameController>();
        EvolutionManager evolutionManager = GetComponent<EvolutionManager>();
        string timeStamp = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        avgFilePath = "EvolutionData/generations_avg" + timeStamp + ".csv";
        breakdownFilePath = "EvolutionData/generations_breakdown" + timeStamp + ".csv";

        WriteString(avgFilePath, "Tower:");
        WriteStringRow(avgFilePath, dnaHeaders);
        WriteFloatRow(avgFilePath, gameController.towerDNA);
        WriteStringRow(avgFilePath, startingParamHeaders);
        WriteFloatRow(avgFilePath, new() { gameController.waveDuration, evolutionManager.enemyCount, evolutionManager.mutationRate, evolutionManager.mutationAmount });
        WriteString(avgFilePath, "Enemy average per generation:");
        WriteStringRow(avgFilePath, dnaHeaders);


    }
    public void WriteString(string filePath, string text)
    {
        StringBuilder sb = new();
        sb.Append(text);
        sb.AppendLine();
        File.AppendAllText(filePath, sb.ToString());
    }

    public void WriteStringRow(string filePath, List<string> headers)
    {
        StringBuilder sb = new();
        foreach (string header in headers)
        {
            sb.Append(header);
            sb.Append(",");
        }
        sb.AppendLine();
        File.AppendAllText(filePath, sb.ToString());
    }

    public void WriteFloatRow(string filePath, List<float> DNA)
    {
        StringBuilder sb = new();

        foreach (float gene in DNA)
        {
            sb.Append(gene.ToString());
            sb.Append(",");
        }

        sb.AppendLine();
        File.AppendAllText(filePath, sb.ToString());
    }


}


