# BuildYourMoped
3D moped assembly simulator built in Unity. Inspired by My Summer Car — disassemble, repair and reassemble your moped part by part.

# AssembleYourMoped (Собери свой мопед) 🛠️

3D-симулятор сборки мопеда на Unity, вдохновлённый My Summer Car.

## О проекте
Игрок получает разобранный мопед и должен собрать его из отдельных 
деталей: двигатель, рама, карбюратор, проводка и т.д. 
Каждая деталь имеет физику, точки крепления.

## Скриншоты / GIF
![Главное меню](Screenshots/Main.jpg)
![Гараж](Screenshots/Garage.jpg)
![Геймплей](Screenshots/Gameplay.jpg)
![Геймплей1](Screenshots/Gameplay1.jpg)
![Динамическая смена дня и ночи](Screenshots/World.gif)
![Гараж](Screenshots/garage1.gif)
![Заправка](Screenshots/gasline.gif)
![Покраска](Screenshots/paint.gif)
![Установка детали](Screenshots/detail.gif)
![Прикручивание детали](Screenshots/screw.gif)
![Езда](Screenshots/ride.gif)


## Реализованные механики
- Система крепления деталей (болты, точки соединения)
- Система покраски деталей (рама, крылья и тп)
- Система смены дня и ночи
- Физика деталей и взаимодействие через Rigidbody
- Перетаскивание объектов
- UI: подсказки, чек-лист сборки

## Технологии
- Unity 2022 LTS
- C#
- [URP / HDRP / Built-in] Render Pipeline
- [DOTween / Cinemachine / другие ассеты]

## Управление
| Действие | Клавиша |
|----------|---------|
| Движение | WASD |
| Взаимодействие | ЛКМ |
| Инструменты | ПКМ |
| Поворот детали | Колесо |

## Как запустить
1. Клонировать репозиторий
2. Открыть проект в Unity [версия]
3. Открыть сцену `Assets/Scenes/MainScene.unity`
4. Нажать Play

## Статус проекта
🚧 В разработке.