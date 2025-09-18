### Auto Shutdown
- To Auto Shutdown PC on 07:10 PM
- Incase you forgot shutdown PC

```bash
schtasks /create /sc daily /tn "DailyShutdown" /tr "shutdown /s /f /t 0" /st 19:10
schtasks /query /tn "DailyShutdown"
```