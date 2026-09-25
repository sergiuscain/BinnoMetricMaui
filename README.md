# BinnoMetric

Кроссплатформенное приложение для учёта производственных записей фармацевтического предприятия. Состоит из REST API на ASP.NET Core и клиента на .NET MAUI.

Проект написан как портфолио: демонстрирует полный цикл разработки — от проектирования БД и API до MVVM-клиента с фильтрацией, пагинацией, графиками и логированием.

---

## Стек

**Backend**
- ASP.NET Core Web API
- Entity Framework Core
- MS SQL Server
- Swagger / OpenAPI

**Frontend**
- .NET MAUI (Android, iOS, Windows, macOS)
- CommunityToolkit.Mvvm
- LiveChartsCore
- HttpClient + System.Text.Json

---

## Скриншоты

**Производственные записи**

<img width="1908" height="1031" alt="image" src="https://github.com/user-attachments/assets/ec1a5181-88aa-48e3-91c9-836de48e29bd" />

**Фильтры**

<img width="1918" height="601" alt="image" src="https://github.com/user-attachments/assets/78513ee9-1170-46de-9a55-327053edfc37" />

**Пагинация**

<img width="1908" height="755" alt="image" src="https://github.com/user-attachments/assets/dbc0683b-5ee3-4f55-a07a-9ab45045b4cf" />

**Логирование**

<img width="1904" height="166" alt="image" src="https://github.com/user-attachments/assets/0dca5add-20ac-4b80-9c6f-4ff061f95546" />

**Карточка записи**

<img width="1913" height="517" alt="image" src="https://github.com/user-attachments/assets/ae333fa9-62c9-4f13-8743-99ba27b9e45b" />

**График производительности**

<img width="1919" height="823" alt="image" src="https://github.com/user-attachments/assets/c5d01de1-9a95-450c-bdb5-89f1d931db5d" />

**Продукты**

<img width="1916" height="659" alt="image" src="https://github.com/user-attachments/assets/f5fbbb9f-5aa5-4c8d-a9c6-05a6df71bb49" />

**Топ сотрудников за смену**

<img width="1919" height="909" alt="image" src="https://github.com/user-attachments/assets/62b6d932-d37b-427c-8683-907640cdb5fb" />

**Сотрудники**

<img width="1915" height="1007" alt="image" src="https://github.com/user-attachments/assets/8686fc3a-ea6a-4319-b44a-9cfa910bd9c4" />

---

## Функциональность

- Журнал производственных записей с серверной пагинацией
- Фильтрация по линии, сотруднику, продукту, серии, количеству, комментарию и диапазону дат
- Карточка записи с детальной информацией
- График выпуска по дням с зумом и скроллингом
- Рейтинг сотрудников по производству текущего продукта за смену
- Справочники продуктов и сотрудников
- Вывод ошибок и предупреждений в UI

---

## Архитектура

**Backend** — контроллеры → сервисы → EF Core. Фильтрация выполняется через `POST` с телом запроса, что позволяет передавать сложные фильтры без ограничений query-строки.

**Frontend** — MVVM на `CommunityToolkit.Mvvm`. `[ObservableProperty]` для состояния, `[RelayCommand]` для команд. Каждая страница — отдельная View + ViewModel, сервисы оборачивают HTTP-вызовы.

```
BinnoMetric/
├── BinnoMetric.Api/
│   ├── Controllers/
│   ├── Services/
│   ├── DataBase/
│   └── DTO/
└── BinnoMetricMaui/
    ├── Model/
    ├── Service/
    ├── ViewModel/
    ├── View/
    └── Resources/
```

## Планы

- Авторизация и роли
- Экспорт в Excel / PDF
- Серверная агрегация для графиков
- Кэширование данных
- Тесты

---

## Примечание

Все данные в проекте — вымышленные. Названия препаратов, ФИО сотрудников, номера серий и комментарии сгенерированы специально для демонстрации и не относятся к реальному производству. Все совпадения случайны.
