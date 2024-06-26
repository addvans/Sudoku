# Судоку
Базовая игра судоку с реализованным функционалом из всех модулей тестового задания:
* Основная игровая механика реализована: кликнув на слот на поле, игрок может записать в него число с помощью клавиатуры в нижней части экрана;
* Поле рандомно генерируется методами обмена строк, столбцов и транспонирования таблицы;
* В меню доступен выбор сложности генерации: поддерживаются значения от 1 до 81. Число - количество пустых клеток;
* Реализовано сохранение: все значения игрового поля сохраняются в одномерный массив в файл **Desk.json**. Файл хранится в [Application.persistentDataPath](https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html);
* Загрузка значений из файла Desk.json предусмотрена отдельной кнопкой. Если файла нет, кнопка недоступна;
* Реализована проверка решений: алгоритм проверяет таблицу на наличие дубликатов или пустых слотов. При их наличии всплывёт предупреждающее окно во время проверки решения;
* Подсветка выбранного слота: при нажатии на слот подсвечиваются его столбец, ряд и блок 3х3. Сам выбранный блок подсвечивается цветом другого оттенка;
* Выделение ошибок: при вводе значения игра сразу проверяет таблицу на наличие дубликатов, и подсвечивает их красным цветом;
* Режим пометок: пометки оставляются коричневым курсивным шрифтом при включённом режиме пометок. Пометки сохраняются, числа в пометках не дублируются, а при вводе «реального» значения в слот пометки удаляются.

Игра разделена на несколько классов:
* GameManager - основной класс, Контроллер игры. Связывает работу UI и остальных классов, проверяет поле на решённость, включает/выключает меню, контроллирует ввод значений в слоты, и т.д. Присутствует на сцене на объекте **Desk**
* SudokuGenerator - Модель игры, отвечающая за генерацию поля. Реализован статичными методами, поэтому на сцене не присутствует;
* SaveManager - класс, работающий с .json файлом. Сохраняет, загружает значения, проверяет наличие сохранённого файла;
* DeskPainter - класс, отвечающий за работу с цветами. Содержит статичные методы для окраски полей и их значений. 
