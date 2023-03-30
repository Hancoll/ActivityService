# EventsService

## Инициализация

Необходимо добавить submodule'и

```
git submodule init
git submodule update
```

## Запуск

Для начала нужно запустить docker контейнеры

```
docker compose up
```

Открытие страницы swagger

```
http://localhost:8080/swagger/index.html
```

Для доступа к API нужно ввести JWT в окне swagger'а

## Получение токена для авторизации

### Запрос

```
POST http://localhost:8081/auth/register
```

### Пример тела запроса

```js
{
    "Nickname": "mynick",
    "Password": "12345"
}
```

### Ответ

```js
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJiOTE3YTQxMS01Yjk2LTRlMzgtODVkYS05ZmUxNjkyN2UzYzEiLCJ1bmlxdWVfbmFtZSI6InNkamZrbCIsImp0aSI6ImQyNTBmYmExLTZlZGItNGE3OS1iYjRjLTUyZGRhOTI1MjM4ZSIsImV4cCI6MTY3OTAzNzMzNywiaXNzIjoiSXNzdWVyIiwiYXVkIjoiQXVkaWVuY2UifQ.740R3550HSAP1hXVvunnuH9RamsnxCWASa4FCfsdMEw
```

## RabbitMQ

Открытие RabbitMQ 
```
http://localhost:15672
```
