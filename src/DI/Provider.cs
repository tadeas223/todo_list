using Data.Repository;
using Domain.Repository;

namespace DI;

public class Provider
{
    private IDBConnection dbConnection = new OracleDBConnection();

    private Provider? instance;
    public Provider Instance
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

    }

    public IBoardRepository ProvideBoardRepository()
    {

    }
    
    public ICalendarRepository ProvideCalendarRepository()
    {

    }
   
    public ITodoTaskRepository ProvideTodoTaskRepository()
    {

    }
}