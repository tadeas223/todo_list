```mermaid
---
title: repository interfaces
---

classDiagram

    class IDBConnection
    class IProjectRepository
    class IBoardRepository
    class ICalendarRepository
    class ITodoTaskRepository

    note "insert methods act like update if the model already exists"

    class IDBConnection {
        +«get» Connected
        +Connect(username: string, password: string, datasource: string)
        +Disconnect()
        +ExecuteQuery(string sq, params (string, object)[] parameters): DataTable;
        +ExecuteNonQuery(string sq, params (string, object)[] parameters): int;
    }

    class IProjectRepository {
        +Insert(project: Project)
        +Update(project: Project)
        +Delete(project: Project)

        +SelectAll() Set~Project~
        +SelectByName(name: string) Project?
        +SelectById(id: int) Project?
    }
    
    class IBoardRepository {
        +Insert(board: Board)
        +Update(board: Board)
        +Delete(board: Board)

        +SelectAll() Set~Board~
        +SelectByName(name: string) Board?
        +SelectById(id: int) Board?

        +SelectTodoTasks(board: Board) HashSet<TodoTask>
    }
    
    class ICalendarRepository {
        +Insert(calendar: Calendar)
        +InsertTask(calendar: Calendar, task: Task)

        +Update(calendar: Calendar)
        +Delete(calendar: Calendar)

        +SelectAll() Set~Calendar~
        +SelectByName(name: string) Calendar?
        +SelectById(id: int) Calendar?

        +SelectAllCalendarTasks(calendar: Calendar) Set~TodoTask~
        +SelectCalendarTasksByDate(calendar: Calendar, date: DateTime) Set~TodoTask~
    }
    
    class ITodoTaskRepository {
        +Insert(task: TodoTask)
        +Update(task: TodoTask)
        +Delete(task: TodoTask)

        +SelectAll() Set~TodoTask~
        +SelectByName(name: string) TodoTask?
        +SelectById(id: int) TodoTask?
        +SelectByState(state: TaskState) Set~TodoTask~
        +SelectByFinishDate(date: Date) Set~TodoTask~
    }

```