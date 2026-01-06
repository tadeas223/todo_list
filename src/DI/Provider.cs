using Data;
using Data.Repository;
using Domain;
using Domain.Repository;

namespace DI;

public class Provider
{
    private IDBConnection dbConnection = new OracleDBConnection();

    private static Provider? instance;
    public static Provider Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new Provider();
            }
            return instance;
        }
    }    

    private Provider() {}

    public IDBConnection ProvideDBConnection()
    {
        return dbConnection;        
    }

    public IProjectRepository ProvideProjectRepository()
    {
        return new OracleDBProjectRepository(dbConnection);
    }

    public IBoardRepository ProvideBoardRepository()
    {
        return new OracleDBBoardRepository(dbConnection);
    }
    
    public ICalendarRepository ProvideCalendarRepository()
    {
        return new OracleDBCalendarRepository(dbConnection);
    }
   
    public ITodoTaskRepository ProvideTodoTaskRepository()
    {
        return new OracleDBTodoTaskRepository(dbConnection);
    }

    public IniConfigurationRepository ProvideConfigurationRepository(string path)
    {
        return new IniConfigurationRepository(path);
    }

    public IProgressReportGen ProvideProgressReportGen()
    {
        return new OracleDBProgressReportGen();
    } 
    
    public IKanbanReportGen ProvideKanbanReportGen()
    {
        return new OracleDBKanbanReportGen();
    } 
}