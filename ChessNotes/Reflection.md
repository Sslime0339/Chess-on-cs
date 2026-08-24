

Идея по рефлексии от которой отказался

Указать тип фигуры

Заместо передачи в конструктор использовать атрибуты

А потом (в главном классе, чтобы не писать это во всех производных) через рефлексию узнать и указать тип фигуры

**Минус**, рефлексия чуть медленней чем просто передать в конструктор

Поэтому не использую


```cs
using System;

namespace Chess
{
  [AttributeUsage(AttributeTargets.Class)]
  class SetPieceTypeAttribute : Attribute
  {
    public PieceType type { get; }
    
    public SetPieceTypeAttribute(PieceType type)
    => this.type = type;
  }
}
```

```cs
using System;
using System.Reflection;

namespace Chess
{
  abstract class Piece
  {
    public Piece(Vector2 position, PieceColor color, Chessboard board)
    {
      _position = position;
      this.color = color;
      _board = board;
      
      var attr = GetType().GetCustomAttribute<SetPieceTypeAttribute>();
      
      if (attr == null) throw new Exception("не задан тип фигуры");
      
      type = attr.type;
    }
  }
}
```

Пример

```cs
[SetPieceType(PieceType.Pawn)]
class Pawn
{
  // ...
}
```