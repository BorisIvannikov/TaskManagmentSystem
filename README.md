Task Managment System представляет собой корпоративное решение для обмена задачами между сотрудниками.
Вы наблюдаете базовую реализацию данного решения. Для получения расширенной версии, предложите мне хороший оффер.
У задачи есть тема, описание, дата время, приоритет, инициатор(тот кто создал задачу) и риципиент(тот кому назначена задача), показатель выполнения задачи(выполнена или нет).
У пользователя есть профайл (логин пароль должность) он используется для авторизации и создания пользователя, есть роль, имя, должность.
У пользователей есть четыре роли: Админ, Босс, начальник отдела(директор), обычный рабочий. админ имеет доступ ко всем функциям приложения, создание удаление
пользователей и отделов. админ может давать задачи всем, может проссматривать все задачи. Босс может может давать задачи всем сотрудникам компании. 
Начальник отдела может давать задачи другим начальникам отдела, работникам своего отдела, админу. Рабочий может давать задачи только рабочим своего отдела, и админу.
Каждый пользователь может просматривать входящие и отправленные задачи, и выполнять входящие задачи.
Реализованные фичи: просмотр созданных задач. просмотр принятых задач. Проверка авторизации на всех страницах. если вы авторизированы, то при добавлении обратной связи, 
ваше имя автоматически вводится в поле для имени просмотр всех пользователей
