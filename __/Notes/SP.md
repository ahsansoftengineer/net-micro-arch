| Method                            | Relative Speed | Tracking Overhead | Use Case                     |
| --------------------------------- | -------------- | ----------------- | ---------------------------- |
| Stored Procedure                  | ⭐️ Fastest     | ❌ None            | Heavy queries, existing SPs  |
| Raw SQL (`FromSqlRaw`)            | ⭐️ Very Fast   | ❌ Optional        | Complex or optimized queries |
| `AsNoTracking().FirstOrDefault()` | ✅ Fast         | ❌ None            | Read-only, single item       |
| `FirstOrDefault()`                | ⚠️ Medium      | ✅ Yes             | Read/update entity           |
| `ToList()` with no filters        | ❌ Slowest      | ✅ Yes             | Pagination or export needed  |
