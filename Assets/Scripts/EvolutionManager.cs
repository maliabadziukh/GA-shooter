using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EvolutionManager : MonoBehaviour
{
    public int enemyCount = 5;
    public float bestFitness = 0;
    public float mutationRate = 0.05f;
    public float mutationAmount = 0.1f;
    public List<float> bestDNA = new();
    public List<Specimen> previousGeneration = new();
    private DataWriter dataWriter;
    private GameController gameController;

    private System.Random random;
    private void Start()
    {
        random = new System.Random();
        dataWriter = gameObject.GetComponent<DataWriter>();
        gameController = GetComponent<GameController>();
    }
    public List<List<float>> CreateGeneration()
    {
        List<List<float>> newGeneration = new();

        if (previousGeneration.Count == 0)
        {
            print("making a random generation");
            while (newGeneration.Count < enemyCount)
            {
                newGeneration.Add(GenerateRandomDNA());
            }
        }
        else
        {
            print("adding best specimen");
            newGeneration.Add(Mutate(new(BestSpecimen().DNA)));
            PrintDNA(newGeneration[0]);

            print("adding children");
            while (newGeneration.Count < enemyCount)
            {

                newGeneration.Add(Mutate(ChildFromCrossover()));
                PrintDNA(newGeneration[newGeneration.Count - 1]);
            }

        }
        dataWriter.WriteFloatRow(dataWriter.avgFilePath, AverageDNA(newGeneration));

        dataWriter.WriteString(dataWriter.breakdownFilePath, gameController.waveIndex.ToString());
        dataWriter.WriteStringRow(dataWriter.breakdownFilePath, dataWriter.dnaHeaders);

        foreach (List<float> dna in newGeneration)
        {
            dataWriter.WriteFloatRow(dataWriter.breakdownFilePath, dna);
        }

        previousGeneration.Clear();
        return newGeneration;
    }

    public List<float> ChildFromCrossover()
    {
        print("Make child from crossover");

        List<float> childDNA = new();
        List<float> firstParentDNA = RouletteWheelParentSelect();
        List<float> secondParentDNA = RouletteWheelParentSelect();
        int midPoint = Random.Range(0, firstParentDNA.Count);

        for (int i = 0; i < firstParentDNA.Count; i++)
        {
            if (i <= midPoint)
            {
                childDNA.Add(firstParentDNA[i]);
            }
            else
            {
                childDNA.Add(secondParentDNA[i]);
            }
        }
        return NormalizeDNA(childDNA);
    }

    private List<float> RouletteWheelParentSelect()
    {
        print("Selecting parent with roulette...");
        float fitnessSum = 0f;
        foreach (Specimen specimen in previousGeneration)
        {
            fitnessSum += specimen.Fitness;
        }
        print("Total fitness: " + fitnessSum);

        float wheelPointer = Random.Range(0, fitnessSum);
        print("Wheel pointer: " + wheelPointer);

        float partialSum = 0;
        foreach (Specimen specimen in previousGeneration)
        {
            partialSum += specimen.Fitness;
            print("added fitness: " + specimen.Fitness + "; partial sum: " + partialSum);
            if (partialSum >= wheelPointer)
            {
                print("Selected parent! DNA:");
                PrintDNA(specimen.DNA);
                return new(specimen.DNA);
            }
        }

        return new(previousGeneration[previousGeneration.Count - 1].DNA);
    }

    public List<float> Mutate(List<float> DNA)
    {
        print("(EM) Mutate from:");
        PrintDNA(DNA);

        List<float> mutatedDNA = DNA;

        for (int i = 0; i < DNA.Count; i++)
        {
            if (random.NextDouble() < mutationRate)
            {
                mutatedDNA[i] += Random.Range(-mutationAmount, mutationAmount);
                if (mutatedDNA[i] < 0)
                {
                    mutatedDNA[i] = 0;
                }
            }
        }

        print("to:");
        PrintDNA(mutatedDNA);

        return NormalizeDNA(mutatedDNA);
    }

    public void LogSpecimen(List<float> DNA, float fitnessScore)
    {
        previousGeneration.Add(new Specimen(DNA, fitnessScore));
    }
    public Specimen BestSpecimen()
    {
        Specimen bestSpecimen = new(new(previousGeneration[0].DNA), previousGeneration[0].Fitness);

        foreach (Specimen specimen in previousGeneration)
        {
            if (specimen.Fitness > bestSpecimen.Fitness)
            {
                bestSpecimen = new(new(specimen.DNA), specimen.Fitness);
            }
        }
        return bestSpecimen;
    }

    private List<float> GenerateRandomDNA()
    {
        print("Generate random DNA");

        return NormalizeDNA(new() { Random.Range(1, 100), Random.Range(1, 100), Random.Range(1, 100), Random.Range(1, 100), Random.Range(1, 100) });
    }


    public List<float> NormalizeDNA(List<float> rawDNA)
    {
        print("Normalize DNA");

        float sum = 0;
        List<float> normalizedDNA = new();

        foreach (float stat in rawDNA)
        {
            sum += stat;
        }
        foreach (float stat in rawDNA)
        {
            normalizedDNA.Add(stat / sum);
        }
        return normalizedDNA;
    }

    private List<float> AverageDNA(List<List<float>> generation)
    {
        List<float> averageDNA = new();
        int statCount = generation[0].Count;

        //iterate through each stat 
        for (int i = 0; i < statCount; i++)
        {
            float sum = 0f;
            //add value from each specimen
            foreach (List<float> DNA in generation)
            {
                sum += DNA[i];
            }
            //divide by total N specimens
            float averageGene = sum / generation.Count;
            averageDNA.Add(averageGene);
        }
        return averageDNA;
    }

    public void PrintDNA(List<float> DNA)
    {
        string output = "";
        foreach (float gene in DNA)
        {
            output += gene + ", ";
        }
        print(output);
    }
}

public class Specimen
{
    public List<float> DNA;
    public float Fitness;
    public Specimen(List<float> dna, float fitness)
    {
        DNA = dna;
        Fitness = fitness;
    }
}
