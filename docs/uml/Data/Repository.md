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
    class OracleDBTaskRepository
    
    OracleDBProjectRepository --() Domain.Repository.IProjectRepository
    OracleDBBoardRepository --() Domain.Repository.IBoardRepository
    OracleDBCalendarRepository --() Domain.Repository.ICalendarRepository
    OracleDBTaskRepository --() Domain.Repository.IOracleRepository
    
    OracleDBProjectRepository --> OracleDBConnection: depends (DI)
    OracleDBBoardRepository --> OracleDBConnection: depends (DI)
    OracleDBCalendarRepository --> OracleDBConnection: depends (DI)
    OracleDBTaskRepository --> OracleDBConnection: depends (DI)

    class OracleDBProjectRepository {
        +OracleDBProjectRepository
    }

    class OracleDBBoardRepository
    class OracleDBCalendarRepository
    class OracleDBTaskRepository

```