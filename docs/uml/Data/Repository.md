```mermaid
---
title: implementations for domain.repository interfaces
---
classDiagram
direction TB

    class OracleDBConnection

    class OracleDBProjectRepository
    class OracleDBBoardRepository
    class OracleDBCalendarRepository
    class OracleDBTodoTaskRepository
    
    OracleDBProjectRepository --() Domain.Repository.IProjectRepository
    OracleDBBoardRepository --() Domain.Repository.IBoardRepository
    OracleDBCalendarRepository --() Domain.Repository.ICalendarRepository
    OracleDBTodoTaskRepository --() Domain.Repository.IOracleRepository
    
    OracleDBProjectRepository --> OracleDBConnection: depends (DI)
    OracleDBBoardRepository --> OracleDBConnection: depends (DI)
    OracleDBCalendarRepository --> OracleDBConnection: depends (DI)
    OracleDBTodoTaskRepository --> OracleDBConnection: depends (DI)

    class OracleDBProjectRepository {
        +OracleDBProjectRepository
    }

    class OracleDBBoardRepository
    class OracleDBCalendarRepository
    class OracleDBTodoTaskRepository

```