# Meeting Scheduler API

Backend system to schedule meetings for multiple users without conflicts

# Technologies Used
- ASP.NET Core (.NET 9)
- Clean Architecture
- CQRS
- AutoMapper
- FluentValidation
- xUnit
- FakeItEasy

## Setup

```bash
git clone https://github.com/sashanazarchuk/MeetingSchedulerApp.git
cd MeetingSchedulerApp
cd MeetingScheduler.API
dotnet run
```

After running the app, the API will be available at:
```
http://localhost:5146/swagger  
```

# Run Tests
```
cd MeetingSchedulerApp
cd MeetingScheduler.Tests
dotnet test
```

# Known limitations
- Data is stored in-memory and lost on application restart 
- Business hours are fixed to 9:00–17:00 UTC; no configuration for different hours or timezones.
- Algorithm only finds earliest available slot within given day/time window; does not support multi-day meetings
- Input validation does not take into account all possible errors






