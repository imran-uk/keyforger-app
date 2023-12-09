# Keyforger

An experimental app for generating KeyForge decks. Created to learn and demonstrate some concepts and  patterns.

Some patterns and concepts used:
* CQRS
* Domain-driven design (sort of)
* TDD (sort of)
* Entity Framework for database access and ORM
* REST API design
* distributed architecture
* event-driven architecture

https://blog.restcase.com/4-maturity-levels-of-rest-api-design/

https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/eshoponcontainers-cqrs-ddd-microservice)


## About KeyForge and the Sets

https://en.wikipedia.org/wiki/KeyForge#Sets


## MySQL

Based on the MySQL Docker image https://hub.docker.com/r/amd64/mysql/

How to connect to MySQL

```sql
\connect imran@localhost

Creating a session to 'imran@localhost'
Fetching schema names for autocompletion... Press ^C to stop.
Your MySQL connection id is 8 (X protocol)
Server version: 8.0.30 MySQL Community Server - GPL
No default schema selected; type \use <schema> to set one.

 MySQL  localhost:33060+ ssl  SQL > show databases;
+--------------------+
| Database           |
+--------------------+
| information_schema |
| keyforger          |
| mysql              |
| performance_schema |
| sakila             |
| sys                |
| world              |
+--------------------+


MySQL  localhost:33060+ ssl  SQL > \u keyforger
Default schema set to `keyforger`.
Fetching table and column names from `keyforger` for auto-completion... Press ^C to stop.


 MySQL  localhost:33060+ ssl  keyforger  SQL > show tables;
+---------------------+
| Tables_in_keyforger |
+---------------------+
| generated_deck      |
| keyforge_deck       |
| keyforge_set        |
+---------------------+

 MySQL  localhost:33060+ ssl  keyforger  SQL > select * from generated_deck;
+--------------------------------------+---------------------+--------+
| generatedId                          | generatedDate       | deckId |
+--------------------------------------+---------------------+--------+
| 20686df8-10f5-49ac-ab84-a8e89f4e2896 | 2022-09-14 18:27:09 |      1 |
| d889fccf-7b2f-4efb-9a05-79fa96baec7f | 2022-09-14 18:26:30 |      2 |
| f16e06d4-a9dc-4b62-947f-3a24a07a6ee6 | 2022-09-14 18:27:29 |      3 |
+--------------------------------------+---------------------+--------+
```

the house table

```sql
 MySQL  localhost:33060+ ssl  keyforger  SQL > select * from house;
+---------------+
| name          |
+---------------+
| Brobnar       |
| Dis           |
| Logos         |
| Mars          |
| Sanctum       |
| Saurian       |
| Shadows       |
| StarAlliance  |
| Unfathomable  |
| Untamed       |
+---------------+

CREATE TABLE `house` (`name` varchar(255) NOT NULL,  PRIMARY KEY (`name`));

insert into house set name="Brobnar";
insert into house set name="Logos";
insert into house set name="Mars";
insert into house set name="Untamed";
insert into house set name="Shadows";
insert into house set name="Saurian";
insert into house set name="StarAlliance ";
insert into house set name="Dis";
insert into house set name="Unfathomable";
insert into house set name="Sanctum";
```

## Docker on Windows

You might run into trouble trying to get Docker running on Windows laptop.

Docker is needed for the MySQL and RabbitMQ images.

I needed to install WSL, with this we can get Linux running.

This helped:
https://learn.microsoft.com/en-gb/windows/wsl/install-manual#step-4---download-the-linux-kernel-update-package