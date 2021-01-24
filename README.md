# Описание FoxfordHack
____
Это библиотека, которая позволяет автоматизировать работу с foxford.ru 

С помощью неё вы сможете выгрузить ответы на курс(-ы) в БД, загрузить ответы на курс(-ы), активировать курсы по промокоду.
____
## Оглавление

1. [Как работает библиотека?](#Как-работает-библиотека-и-что-потребуется-для-её-использования)
2. [Настройки перед использованием](#Настройки-перед-использованием)
3. Примеры применения
   1. [Получить все доступные курсы](#Получить-все-доступные-курсы)
   2. [Получить все занятия курса](#Получить-все-занятия-курса)
4. [Информация по Лицензии](https://github.com/bezlla/FoxfordHack/master/LICENSE)

## Как работает библиотека и что потребуется для её использования

В библиотеке используется WebApi, EF .NET Core. Библиотека только взаимодействует с сервером foxford.ru 
Для работы с ней потребуется Cookie пользователя и XCSRFToken.
____
## Настройки перед использованием

Прежде всего необходимо убедиться, что в корневой директории присутствует файл appsettings.json
```json
{
 "ConnectionStrings":
	{	
	  "Default":"data source=juri\\TestServer;Initial Catalog=FoxfordHack_8;Integrated Security=True;"
	}
}
```
Необходимо задать наименование сервера (data source) и самой БД (Initial Catalog).
Взаимодействие с БД происходит посредством EF .Net Core(3.1) 

[Список допустимых СУБД](https://docs.microsoft.com/ru-ru/ef/core/providers/?tabs=dotnet-core-cli)
____
[:arrow_up:Оглавление](#Оглавление)
## Примеры применения
  1. #### Получить все доступные курсы
```C#
using FoxfordHack.Services.WebApi.Query;
using FoxfordHack.Models.ModelParsingToJson.Course;
using System;
using System.Linq;
using System.Collections.Generic;

var webApiCourse = new CourseWebService("<my_cookie>","<my_xcsrftoken>");
List<Course> courses = await webApi.GetAllActiveCourses();
```
  2. #### Получить все занятия курса
```C#
using FoxfordHack.Services.WebApi.Query;
using FoxfordHack.Models.ModelParsingToJson.Lesson;
using System;
using System.Linq;
using System.Collections.Generic;

var webApiLesson = new FoxfordLessonsWebService("<my_cookie>","<my_xcsrftoken>");
List<Lesson> courses = await webApi.GetAllLessonsInCourse(int 228);
```

