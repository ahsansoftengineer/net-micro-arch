

##  Types of Frames in RabbitMQ (AMQP 0-9-1)
| Frame Type | Purpose                             |
| ---------- | ----------------------------------- |
| Method     | Commands like publish, ack, consume |
| Header     | Metadata about the message          |
| Body       | Actual message content              |
| Heartbeat  | Keep connection alive               |
## Common Developer Use Cases
| Operation                   | Method(s) Involved                           |
| --------------------------- | -------------------------------------------- |
| Declare a queue             | `queue.declare`                              |
| Bind a queue to an exchange | `queue.bind`                                 |
| Declare an exchange         | `exchange.declare`                           |
| Start consuming             | `basic.consume`, `basic.deliver`             |
| Acknowledge a message       | `basic.ack`                                  |
| Publish a message           | `basic.publish`, `basic.return`              |
| Enable publisher confirms   | `confirm.select`, `basic.ack/nack` responses |
| Set prefetch                | `basic.qos`                                  |
### Summary Table (Class Behavior vs AMQP Methods)
| Concept               | Description                                |
| --------------------- | ------------------------------------------ |
| **Producer**          | Sends messages to an exchange              |
| **Exchange**          | Routes messages to queues                  |
| **Queue**             | Stores messages                            |
| **Consumer**          | Reads messages from a queue                |
| **Routing Key**       | Used by exchanges to route messages        |
| **Bindings**          | Rules that link exchanges to queues        |
| **ACK**               | Acknowledgment from consumer to broker     |
| **Prefetch**          | Controls message load per consumer         |
| **Dead Letter Queue** | Queue for undeliverable or failed messages |
