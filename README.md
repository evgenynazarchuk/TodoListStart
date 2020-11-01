# TodoListStart
# TodoListStart.Application
Базовое приложение TodoListStart.Application реализующие функционал по управлению списками, управление заметками в списке  
На данном этапе архитектура не сильно имеет значение  
Приложение написано для реализации интеграционных автотестов  
В будущем перепишу на CQRS + MediatR  

# TodoListStart.IntegrationTests 
# Концепция интеграционных автотестов
Класс TestBase запускает приложение TodoListStart.IntegrationTests  
В классе реализованы сущности Facade, Data, Date  
Facade создаёт http запросы к приложению  
Data создаёт данные и зависимые сущности  
Date устанавливает текущие время для приложения  

# TodoListStart.UnitTests
юнит тест с моками..... доделаю, нормально, чесн слово =)

# JmeterTestPlan
Тест планы для Jmeter  
В процессе...  

# PostmanTestPlan
Тест планы для Postman
В процессе...  
