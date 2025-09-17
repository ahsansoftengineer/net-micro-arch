| Exchange Type | Routing Logic  | `Key` Required | `Headers` Used | Use Case               |
| ------------- | -------------- | -------------- | -------------- | ---------------------- |
| Direct        | Exact match    | ✅ Yes          | ❌ No           | Targeted routing       |
| Topic         | Wildcard match | ✅ Yes          | ❌ No           | Logging, user activity |
| Fanout        | Broadcast      | ❌ Ignored      | ❌ No           | Global notifications   |
| Headers       | Header match   | ❌ No           | ✅ Yes          | Advanced filtering     |
### Direct
- Routing key must match exactly
- Use case: Targeted messaging between services (e.g., order -> inventory)
#### Pub
```c#
rabbit.Pubs(new RabbitMQParam
{
  payload = new { Event = "OrderCreated", OrderId = 123 },
  route = new()
  {
    Exchange = "ex.direct",
    Queue = "q.order",
    Key = "order.created",
    Typez = ExchangeType.Direct
  }
});

```
#### Sub
```c#
rabbit.Subs<dynamic>(new RabbitMQParam
{
  route = new()
  {
    Exchange = "ex.direct",
    Queue = "q.order",
    Key = "order.created",
    Typez = ExchangeType.Direct
  }
}, msg => Console.WriteLine($"[DIRECT] Received: {JsonSerializer.Serialize(msg)}"));

```

### Fanout
- Broadcasts to all bound queues, routing key is ignored.
- Use case: System-wide announcements (e.g., UserRegistered)
#### Pub
```c#
rabbit.Pubs(new RabbitMQParam
{
  payload = new { Event = "UserRegistered", Email = "test@example.com" },
  route = new()
  {
    Exchange = "ex.fanout",
    Typez = ExchangeType.Fanout
    // Key is ignored
  }
});

```
#### Sub
```c#
rabbit.Subs<dynamic>(new RabbitMQParam
{
  route = new()
  {
    Exchange = "ex.fanout",
    Queue = "q.email.welcome", // every subscriber should use a unique queue
    Typez = ExchangeType.Fanout
  }
}, msg => Console.WriteLine($"[FANOUT] Received: {JsonSerializer.Serialize(msg)}"));

```

### Topic
- Supports wildcard routing with:
- - \* = exactly one word
- - \# = zero or more words
- Use case: Logging, user events by scope
#### Pub
```c#
rabbit.Pubs(new RabbitMQParam
{
  payload = new { UserId = 42, Action = "Login" },
  route = new()
  {
    Exchange = "ex.topic",
    Queue = "q.user.activity",
    Key = "user.42.login",
    Typez = ExchangeType.Topic
  }
});

```
#### Sub
```c#
rabbit.Subs<dynamic>(new RabbitMQParam
{
  route = new()
  {
    Exchange = "ex.topic",
    Queue = "q.user.activity",
    Key = "user.*.login",
    Typez = ExchangeType.Topic
  }
}, msg => Console.WriteLine($"[TOPIC] Received: {JsonSerializer.Serialize(msg)}"));

```

### Headers
- Routing via headers instead of routing key
- Use case: Complex routing logic (e.g., event type, company, region)
#### Pub
```c#
rabbit.Pubs(new RabbitMQParam
{
  payload = new { Event = "DataExported", Format = "CSV" },
  route = new()
  {
    Exchange = "ex.headers",
    Queue = "q.data.export", // optional, used for queue declaration
    Typez = ExchangeType.Headers
  },
  options = new()
  {
    Headers = new Dictionary<string, object>
    {
      { "x-match", "all" },  // or "any"
      { "event", "data.export" },
      { "format", "csv" }
    }
  }
});

```
#### Sub
```c#
rabbit.Subs<dynamic>(new RabbitMQParam
{
  route = new()
  {
    Exchange = "ex.headers",
    Queue = "q.data.export",
    Typez = ExchangeType.Headers
  },
  options = new()
  {
    Headers = new Dictionary<string, object>
    {
      { "x-match", "all" },
      { "event", "data.export" },
      { "format", "csv" }
    }
  }
}, msg => Console.WriteLine($"[HEADERS] Received: {JsonSerializer.Serialize(msg)}"));

```
