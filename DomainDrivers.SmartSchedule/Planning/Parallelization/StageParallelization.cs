namespace DomainDrivers.SmartSchedule.Planning.Parallelization;

public class StageParallelization
{
    public ParallelStagesList Of(ISet<Stage> stages)
    {
        return FindRecursively(stages, ParallelStagesList.Empty());
    }

    private ParallelStagesList FindRecursively(ISet<Stage> stages, ParallelStagesList parallelStagesList)
    {
        var found = stages
            .Where(stage => stage.HasAllDepenenciesFullfiled(parallelStagesList.AllStages))
            .ToHashSet();

        if (!found.Any())
        {
            return parallelStagesList;
        }
        
        var parallelStages = new ParallelStages(found);
        var remainingStages = stages.Except(found).ToHashSet();

        return FindRecursively(remainingStages, parallelStagesList.Add(parallelStages));
    }
}