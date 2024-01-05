namespace InjectionMachineModule.Domain.Helpers;

public class TabuSearchOptimizer<TType>
{
    private readonly TType[] InitialSolution;
    public readonly int MaxIterations;
    public readonly int TabuListSize;
    public readonly Func<TType[], object, double> FitnessFunction;
    public readonly EOptimalType OptimaType;
    public event Action<TType[]>? IterationCompleted;
    public object AdditionalData { get; set; }

    private readonly Queue<TType[]> tabuList = new();

    public TabuSearchOptimizer(TType[] initialSolution, int maxIterations, int tabuListSize, Func<TType[], object, double> fitnessFunction, TabuSearchOptimizer<TType>.EOptimalType optimaType, object additionalData)
    {
        InitialSolution = initialSolution;
        MaxIterations = maxIterations;
        TabuListSize = tabuListSize;
        FitnessFunction = fitnessFunction;
        OptimaType = optimaType;
        AdditionalData = additionalData;
    }

    public TType[] Run()
    {
        TType[] currentSolution = InitialSolution;
        TType[] bestSolution = InitialSolution;
        tabuList.Enqueue(InitialSolution);

        for (int i = 0; i < MaxIterations; i++)
        {
            var neighborSolutions = TabuSearchOptimizer<TType>.GetNeighborSolutions(currentSolution);
            currentSolution = GetBestCandidateNotInTabuList(neighborSolutions);

            tabuList.Enqueue(currentSolution);
            if (tabuList.Count > TabuListSize)
            {
                tabuList.Dequeue();
            }

            if (OptimaType == EOptimalType.Maximum)
            {
                if (FitnessFunction(currentSolution, AdditionalData) > FitnessFunction(bestSolution, AdditionalData))
                {
                    bestSolution = currentSolution;
                }
            }
            else
            {
                if (FitnessFunction(currentSolution, AdditionalData) < FitnessFunction(bestSolution, AdditionalData))
                {
                    bestSolution = currentSolution;
                }
            }
            IterationCompleted?.Invoke(currentSolution);
            var fitness = FitnessFunction(currentSolution, AdditionalData);
            if (fitness == 0 && OptimaType == EOptimalType.Minimum)
            {
                return currentSolution;
            }
            Console.WriteLine($"{i}: {fitness}");
        }

        return bestSolution;
    }

    private TType[] GetBestCandidateNotInTabuList(List<TType[]> candidates)
    {
        TType[] bestCandidate = null!;
        double bestFitness;
        if (OptimaType == EOptimalType.Maximum)
        {
            bestFitness = double.MinValue;
        }
        else
        {
            bestFitness = double.MaxValue;
        }

        foreach (var candidate in candidates)
        {
            if (!tabuList.Any(x => candidate.SequenceEqual(x)))
            {
                if (OptimaType == EOptimalType.Maximum)
                {
                    if (FitnessFunction(candidate, AdditionalData) > bestFitness)
                    {
                        bestCandidate = candidate;
                        bestFitness = FitnessFunction(candidate, AdditionalData);
                    }
                }
                else
                {
                    if (FitnessFunction(candidate, AdditionalData) < bestFitness)
                    {
                        bestCandidate = candidate;
                        bestFitness = FitnessFunction(candidate, AdditionalData);
                    }
                }
            }
        }
        return bestCandidate;
    }

    private static List<TType[]> GetNeighborSolutions(TType[] currentSolution)
    {
        List<TType[]> neighborSolutions = new();
        for (int i = 0; i < currentSolution.Length; i++)
        {
            for (int j = 0; j < currentSolution.Length; j++)
            {
                if (i != j)
                {
                    var neighborSolution = (TType[])currentSolution.Clone();
                    neighborSolution[i] = currentSolution[j];
                    neighborSolution[j] = currentSolution[i];

                    neighborSolutions.Add(neighborSolution);
                }
            }
        }
        return neighborSolutions;
    }

    public enum EOptimalType
    {
        Maximum,
        Minimum
    }
}