# ASMR-Slicing

[**Завантажити APK**](https://drive.google.com/file/d/15x2MXmtNm--Ke7BuL1O3ob9HBJal_Nc6/view?usp=sharing)

[**Відео геймплея**](https://youtube.com/shorts/Ci4uhaaah9w)

![Gameplay](https://github.com/dancewithmoon/ASMR-Slicing/assets/91838410/79f5660f-7fbe-4e22-b6a8-aac30b954e9e)

### Алгоритм реалізації:
**1. Спроєктовано базову архітектуру**
- Створено стейт-машину з основними станами гри: Bootstrap, Load Level, Game Loop
- Створено основні сервіси: Assets, Instantiate Service, Static Data Service, Game Factory, Scene Loader

**2. Прибрано ніж та предмет, що розрізається, зі сцени. Тепер їх інстанційовано за допомогою фабрики**
- Створено LevelStaticData (Scriptable Object), який містить інформацію про положення ножа та предмета на сцені, а також відповідні префаби
- Додано код створення ножа та предмета до GameFactory

**3. Реалізовано рух ножа**
- Створено відповідні InputService (для едітора та для телефона)
- Створено скрипт KnifeMovement, який рухає ніж вниз при виклику методу Move()
- Створено скрипт KnifeInput, який пов'яюзує між собою InputService та KnifeMovement
- Створено скрипт Table, який зупиняє рух ножа при колізії зі столом

**4. Реалізовано рух розрізаємого предмета за допомогою скрипту SliceMovement**

**5. Реалізовано розрізання предмета**
- Для розрізання використано ассет [Mesh Slicer](https://assetstore.unity.com/packages/tools/modeling/mesh-slicer-59618)
- Створено віпдовідні скрипти: KnifeSlicing, BzSliceable

**6. Реалізовано відкидання та падіння предмета після розрізання**
- Створено скрипт SliceThrowable

**7. Зв'язано основну логіку**
- Для зв'язування логіки між собою використано стейт GameLoop, який підписується на відповідні івенти компонентів ножа та розрізаємого предмета

**8. Додано логіку скручування розрізаної частини предмета**
- Використано ассет [Deform](https://assetstore.unity.com/packages/tools/modeling/deform-148425)
- Створено скрипти KnifeDeforming та SliceDeformable

**9. Додано сервіси UI**
- Створено UIFactory, UIStaticDataService
- Створено ScreenService, який відповідає за перемикання екранів

**10. Додано нові ігрові стани**
- WaitForActionState - відповідає за очікування user input, перш ніж починати рухати розрізаємий предмет
- LevelCompletedState - відповідає за ті події, що відбуваються після завершення рівня
- Додано відповідний UI

**11. Додано кінцеву точку предмета та ігровий HUD**
- Додано кінцеву точку предмета, після досягнення якої гра переходить у LevelCompletedState
- Створено progress bar

**12. Додано різну швидкість руху для ножа: для звичайного руху та для розрізання**

**13. Додано лінію розрізу**
- Візуалізовано лінію розрізу за допомогою LineRenderer
- Точки для LineRenderer визначаються за допомогою рейкастів

> На виконання завдання витрачено: 20 год 24 хв
