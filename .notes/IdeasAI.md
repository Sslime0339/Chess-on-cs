Связать enum с типом наследника через атрибут можно так:

```csharp
// 1. Атрибут, привязывающий enum-значение к классу

[AttributeUsage(AttributeTargets.Class)]
public class KindAttribute : Attribute
{
    public AnimalKind Kind { get; }
    public KindAttribute(AnimalKind kind) => Kind = kind;
}

// 2. Enum

public enum AnimalKind
{
    Dog,
    Cat,
    Bird
}

// 3. Базовый класс — читает атрибут через рефлексию

public abstract class Base
{
    public AnimalKind Kind { get; }

    protected Base()
    {
        var attr = GetType()
            .GetCustomAttribute<KindAttribute>();

        Kind = attr?.Kind
            ?? throw new InvalidOperationException(
                $"{GetType().Name} не помечен [Kind]");
    }
}

// 4. Наследники — каждый помечен своим enum-значением

[Kind(AnimalKind.Dog)]
public class Dog : Base { }

[Kind(AnimalKind.Cat)]
public class Cat : Base { }

[Kind(AnimalKind.Bird)]
public class Bird : Base { }
```

Использование:

```csharp
Base animal = new Cat();
Console.WriteLine(animal.Kind); // Cat
```

**Плюс** — нельзя забыть передать значение, оно «привязано» к типу декларативно.

**Минус** — рефлексия в конструкторе. На горячих путях это может быть заметно. Если создаёте много объектов, можно закэшировать словарь `Type → AnimalKind` в статическом поле базового класса:

```csharp
public abstract class Base
{
    private static readonly Dictionary<Type, AnimalKind> _cache = new();

    public AnimalKind Kind { get; }

    protected Base()
    {
        Kind = GetKind(GetType());
    }

    private static AnimalKind GetKind(Type type)
    {
        if (_cache.TryGetValue(type, out var kind))
            return kind;

        var attr = type.GetCustomAttribute<KindAttribute>();
        kind = attr?.Kind
            ?? throw new InvalidOperationException(
                $"{type.Name} не помечен [Kind]");

        _cache[type] = kind;
        return kind;
    }
}
```

Так рефлексия отрабатывает один раз на тип, дальше всё берётся из словаря.