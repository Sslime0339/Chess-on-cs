Для реализации проверки на шах при хождении фигур лучше всего использовать метод «мнимого хода» (микро-симуляции).

Суть метода: вы временно делаете ход на доске, проверяете, находится ли ваш король под ударом, и если да — отменяете ход и запрещаете его.
Вот подробный разбор того, как распределить эту логику по вашим классам.

------------------------------
## 1. Добавление базовой логики в класс Piece
Каждая фигура должна уметь генерировать два списка ходов:

   1. Псевдолегальные ходы — ходы, которые фигура может сделать по своим правилам (например, слон по диагонали), не задумываясь о шахе.
   2. Легальные ходы — только те ходы, которые не подставляют своего короля под шах.
```
public abstract class Piece
{
    // ... ваши текущие поля (Color, Position и т.д.)

    // Возвращает ходы без учёта шаха
    public abstract List<Square> GetPseudoLegalMoves(Chessboard board);

    // Возвращает только безопасные ходы
    public List<Square> GetLegalMoves(Chessboard board, ChessGame game)
    {
        var legalMoves = new List<Square>();
        var pseudoMoves = GetPseudoLegalMoves(board);

        foreach (var targetSquare in pseudoMoves)
        {
            // Симулируем ход и проверяем безопасность
            if (!game.WouldLeaveKingInCheck(this, targetSquare))
            {
                legalMoves.Add(targetSquare);
            }
        }

        return legalMoves;
    }
}
```

------------------------------
## 2. Реализация проверки шаха в ChessGame (или Chessboard)
Вам понадобятся два метода в классе ChessGame (или Chessboard, в зависимости от того, где у вас хранится состояние игры):

   1. IsInCheck(Color kingColor) — проверяет, атакован ли король прямо сейчас. Для этого нужно найти короля нужного цвета и посмотреть, может ли хоть одна фигура соперника сделать псевдолегальный ход на клетку короля.
   2. WouldLeaveKingInCheck(Piece piece, Square target) — та самая микро-симуляция.
```
public class ChessGame
{
    private Chessboard _board;

    // Проверка: находится ли король под шахом прямо сейчас
    public bool IsInCheck(Color kingColor)
    {
        Square kingSquare = _board.FindKing(kingColor);
        Color opponentColor = kingColor == Color.White ? Color.Black : Color.White;

        // Смотрим все живые фигуры соперника
        foreach (var piece in _board.GetPieces(opponentColor))
        {
            // Если фигура врага может пойти на клетку короля
            if (piece.GetPseudoLegalMoves(_board).Contains(kingSquare))
            {
                return true; // Шах!
            }
        }
        return false;
    }

    // Симуляция хода для проверки на шах
    public bool WouldLeaveKingInCheck(Piece piece, Square targetSquare)
    {
        Square originalSquare = piece.Square;
        Piece targetPiece = targetSquare.Piece; // Может быть null, если клетка пустая

        // --- ШАГ 1: Делаем мнимый ход ---
        _board.MovePieceInternal(piece, targetSquare); 

        // --- ШАГ 2: Проверяем, есть ли шах ---
        bool inCheck = IsInCheck(piece.Color);

        // --- ШАГ 3: Возвращаем всё назад (откатываем ход) ---
        _board.MovePieceInternal(piece, originalSquare);
        if (targetPiece != null)
        {
            _board.PlacePiece(targetPiece, targetSquare); // возвращаем съеденную фигуру
        }

        return inCheck;
    }
}
```
------------------------------
## 3. Алгоритм выполнения хода игроком
Когда игрок кликает на фигуру и пытается передвинуть её на TargetSquare:

   1. Программа вызывает метод piece.GetLegalMoves(_board, this).
   2. Если TargetSquare есть в этом списке, то:
   * Фигура перемещается насовсем.
      * Ход передается следующему игроку.
   3. Если TargetSquare нет в списке, ход блокируется (выводится ошибка или фигура возвращается на место).

------------------------------
## Важные нюансы для оптимизации и расширения

* Избегайте бесконечной рекурсии! Метод IsInCheck должен вызывать у вражеских фигур именно GetPseudoLegalMoves (псевдолегальные ходы). Если он случайно вызовет GetLegalMoves, игра уйдет в бесконечный цикл (проверка шаха вызовет проверку шаха).
* Специальные ходы (Рокировка и Взятие на проходе):
* Для рокировки нельзя симулировать только конечную точку. Король не должен быть под шахом на стартовой клетке, на промежуточной клетке и на конечной.
   * При симуляции взятия на проходе (en passant) пешка соперника удаляется не с целевой клетки, а с соседней. Учтите это при откате мнимого хода.

Какую структуру для хранения координат вы используете в Square (например, X и Y или индексы)? От этого зависит, как лучше написать метод MovePieceInternal. Хотите разобрать этот момент?

