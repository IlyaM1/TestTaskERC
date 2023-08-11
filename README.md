Тестовое задание для компании ООО "ЕРЦ"

Установка (в терминале):
```
git clone https://github.com/IlyaM1/TestTaskERC.git
```

Затем нужно запустить WebApiServer
```
cd TestTaskERC
cd AccountWebAPI
dotnet run dev-certs https --trust --urls=https://localhost:7280/
```
После его запуска можно "попробовать" его, например по ссылкам:
https://localhost:7280/
https://localhost:7280/api/accounts

Затем нужно запустить второй сервер:
*Запустить второй терминал*
```
cd YOUR_PATH_TO_TEST_TASK/TestTaskERC/AccountManagingServer
dotnet run dev-certs https --trust --urls=https://localhost:5075/
```
После его запуска можно пользоваться сайтом:
Переходим на корневой route (https://localhost:5075/), тут отображаются все лицевые счета, отсюда мы можем сделать 2 перехода:
1. Перейти на подробную информацию по лицевому счёту (нужно нажать на его номер (например на 11111)) - на этой странице работает кнопка "Удалить", но не работает пока кнопка "Редактировать" (не успел доделать)
2. Перейти на страницу создание ЛС (по кнопке снизу), здесь нужно заполнить поля и нажать кнопку отправить (если ввести невалидные данные то аккаунт не создастся)

К сожалению, из-за недостатка времени я не успел сделать эти возможности:
•	Поиск и фильтрация по параметрам (планировал делать endpoint /filter с json в теле, который обозначает какой фильтр нужен, обрабывать это на сервере и отправлять на фронт)
•	Редактирование данных ЛС (не успел доделать страничку по маршруту "/{id}/edit", но она легко делается по аналогии с созданием нового ЛС (возможность обновить любое кол-во полей у любого аккаунта, если мы знаем id реализовано на WebApi сервере))
•	Валидация вводимых данных - она присутствует, но не успел доделать более явное обращение к пользователю с указанием на ошибку которую он допустил при создании аккаунта, а также при невалидных данных, если Web Api сервер выдал ошибку 400, показывается, что аккаунт создан успешно (хотя на самом деле в базе данных он не создаётся, в чём можно удостовериться пеорейдя на корневую страницу)

Задание было интересным, но жаль, что не успел сделать всё, что хотел