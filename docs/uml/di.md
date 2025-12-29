```mermaid
classDiagram

    class Provider

    class Provider {
        - implementation of IDBConnection        

        +provideDBConnection(): IDBConnection
        +provideProjectRepository(): IProjectRepository
        +provideBoardRepository(): IBoardRepository
        +provideCalendarRepository(): ICalendarRepository
        +provideTaskRepository(): ITaskRepository
    }

```