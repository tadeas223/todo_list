```mermaid
classDiagram

    class Provider

    class Provider {
        - implementation of IDBConnection        

        +ProvideDBConnection(): IDBConnection
        +ProvideProjectRepository(): IProjectRepository
        +ProvideBoardRepository(): IBoardRepository
        +ProvideCalendarRepository(): ICalendarRepository
        +ProvideTaskRepository(): ITaskRepository
    }

```