```mermaid
classDiagram

    class Provider

    class Provider {
        - instance$: Provider
        - implementation of IDBConnection
        
        - Provider() 
        
        + Instance$: Provider

        +ProvideDBConnection() IDBConnection
        +ProvideProjectRepository() IProjectRepository
        +ProvideBoardRepository() IBoardRepository
        +ProvideCalendarRepository() ICalendarRepository
        +ProvideTodoTaskRepository() ITodoTaskRepository
    }

```