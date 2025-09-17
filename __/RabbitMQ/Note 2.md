| RabbitMQ Concept            | Real-World Example in HCM                                              | Purpose / Role                                                   |
| --------------------------- | ---------------------------------------------------------------------- | ---------------------------------------------------------------- |
| **Producer**                | `AttendanceService`, `EmployeeService`, `LeaveService`                 | Publish events like `employee.added`, `leave.approved`           |
| **Consumer**                | `PayrollService`, `NotificationService`, `AnalyticsService`            | Consume data changes to update pay, send alerts, or analyze      |
| **Exchange**                | `hcm.topic`, `hcm.direct`, `hcm.events`                                | Routes HCM-related messages across services                      |
| **Exchange Type**           | `topic`, `fanout`, `direct`                                            | Used for flexible routing, broadcasting, or exact delivery       |
| **Routing Key**             | `employee.added`, `attendance.marked`, `leave.cancelled.manager`       | Identifies message type and department context                   |
| **Queue**                   | `payroll.queue`, `notification.queue`, `hr.analytics.queue`            | Stores messages for each consuming service                       |
| **Binding**                 | `employee.*` → `payroll.queue`<br>`leave.*` → `notification.queue`     | Connects routing patterns to interested services                 |
| **Message**                 | `{ "event": "attendance.marked", "empId": 123, "date": "2025-06-19" }` | Actual event payload                                             |
| **Channel**                 | Logical path over TCP used by services like `EmployeeService`          | Efficient communication                                          |
| **Acknowledgment**          | Manual `ack` in `PayrollService`, autoAck in `AnalyticsService`        | Ensures reliable processing and fault recovery                   |
| **Dead Letter Queue (DLQ)** | `notification.dlq` or `payroll.dlq`                                    | Captures unprocessed or failed messages                          |
| **Prefetch Count**          | `basic.qos(1)` in `PayrollService`                                     | Prevents overloading consumers — processes one message at a time |
| **Durable Queue**           | `payroll.queue`, `employee.record.queue`                               | Keeps critical HR/payroll messages safe during broker restarts   |
| **Persistent Message**      | All core events marked persistent (e.g., `employee.added`)             | Stored to disk for guaranteed delivery                           |
| **Publisher Confirms**      | Used by `AttendanceService` when marking punch-ins                     | Ensures the message is reliably stored in RabbitMQ               |
