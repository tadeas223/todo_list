using Domain.Model;

namespace Domain;

public interface IProgressReportGen
{
    List<TaskProgressReport> Generate();
}