# Redis

## 1. Install Redis

Install the following application in your system to proceed with Redis:

* **Redis-x64-5.0.14.1**

On the installation process make sure to check the(add the redis to environmental variable) otherwise the redis server wont start
After installation, Redis will run locally on the default port **6379**.

---

## 2. Verify Redis Server

Use the below command to check whether the Redis server is running:

```bash
redis-cli ping
```

### Expected Result

```text
PONG
```

If you receive `PONG`, Redis is running successfully.

---

## 3. Monitor Redis Cache

To monitor all Redis commands in real time, use:

```bash
redis-cli MONITOR
```

This command displays every operation executed on Redis and is useful for debugging.

⚠️ **Note:** Avoid using `MONITOR` in production environments as it is very verbose.

---

## 4. Redis Cache Clear Commands

| Command     | Scope            | Impact | Result                                             |
| ----------- | ---------------- | ------ | -------------------------------------------------- |
| `FLUSHDB`   | Current database | Medium | Deletes keys in the selected database only         |
| `FLUSHALL`  | Entire server    | High   | Deletes all keys across all 16 (default) databases |
| `DEL [key]` | Single key       | Low    | Deletes only the specified key(s)                  |

### Examples

```bash
DEL user:101
```

```bash
FLUSHDB
```

```bash
FLUSHALL
```

---

## 5. BullMQ Overview

**BullMQ** is a Redis-based queue system used to handle background jobs such as:

* Email sending
* Notifications
* File processing
* Scheduled or delayed tasks

BullMQ runs on **Node.js** and uses Redis as its backend.

---

## 6. Implementing BullMQ with React.js

> ⚠️ **Important:** BullMQ cannot run directly inside React.js (frontend).

### Correct Architecture

* **React.js** → Frontend (triggers API requests)
* **Node.js** → Backend (BullMQ producer & worker)
* **Redis** → Queue storage

React communicates with BullMQ through backend APIs.

---

## 7. BullMQ Reference (Node.js)

Use the following guide to implement BullMQ in Node.js:

* BullMQ for Node.js:
  [https://hadoan.medium.com/bullmq-for-beginners-a-friendly-practical-guide-with-typescript-examples-eb8064bef1c4](https://hadoan.medium.com/bullmq-for-beginners-a-friendly-practical-guide-with-typescript-examples-eb8064bef1c4)

---

## 8. Summary

* Redis is used for fast in-memory caching
* `redis-cli ping` checks server health
* `MONITOR` helps debug cache activity
* `FLUSHDB`, `FLUSHALL`, and `DEL` manage cache cleanup
* BullMQ handles background jobs using Redis
* React.js interacts with BullMQ via a Node.js backend

---
