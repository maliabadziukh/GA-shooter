using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

public class EvolutionManager : MonoBehaviour
{
    public int enemyCount = 5;
    public float bestFitness = 0;
    public float mutationRate = 0.05f;
    public List<float> bestDNA = new();
    public List<List<float>> previousGeneration = new();

    private System.Random random;
    private void Start()
    {
        random = new System.Random();
    }
    public List<List<float>> CreateGeneration()
    {
        List<List<float>> newGeneration = new();
        print("(EM) previous generation.Count == " + previousGeneration.Count);
        //make the new generation random if it's the first one
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
            newGeneration.Add(Mutate(bestDNA));
            PrintDNA(newGeneration[0]);

            print("adding children");
            while (newGeneration.Count < enemyCount)
            {

                newGeneration.Add(Mutate(ChildFromCrossover()));
                PrintDNA(newGeneration[newGeneration.Count - 1]);
            }

        }

        previousGeneration = new List<List<float>>(newGeneration);

        print("generation " + GetComponent<GameController>().waveIndex);
        foreach (List<float> DNA in newGeneration)
        {
            PrintDNA(DNA);
        }
        return newGeneration;
    }

    public List<float> ChildFromCrossover()
    {
        print("Make child from crossover");

        List<float> childDNA = new();
        List<float> firstParentDNA = previousGeneration[Random.Range(0, previousGeneration.Count)];
        List<float> secondParentDNA = previousGeneration[Random.Range(0, previousGeneration.Count)];
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
        return childDNA;
    }

    public List<float> Mutate(List<float> DNA)
    {
        print("(EM) Mutate");

        List<float> mutatedDNA = DNA;

        for (int i = 0; i < DNA.Count; i++)
        {
            if (random.NextDouble() < mutationRate)
            {
                //swaps some genes around
                if (i == DNA.Count - 1)
                {
                    mutatedDNA[i] = DNA[0];
                    mutatedDNA[0] = DNA[i];
                }
                else
                {
                    mutatedDNA[i] = DNA[i + 1];
                    mutatedDNA[i + 1] = DNA[i];
                }
            }
        }

        return mutatedDNA;

    }

    public void CheckFitness(float timeSurvived, List<float> DNA)
    {
        print("(EM) Check fitness");
        if (timeSurvived > bestFitness)
        {
            bestFitness = timeSurvived;
            bestDNA = DNA;
            print("(EM) New best! " + bestFitness);
        }
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