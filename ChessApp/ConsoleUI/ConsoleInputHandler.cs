using Chess;



namespace ConsoleUI
{
    static class ConsoleInputHandler
    {
        public static bool TryParseMove(string input, out Vector2 from, out Vector2 to)
        {
            //нейронка
            /*  TODO: расширить TryParseMove
            добавить просмотр куда может походить фигура
            
            */

            from = new Vector2(0, 0);
            to = new Vector2(0, 0);

            input = input.Replace(" ", "");

            // Проверяем базовую длину (например, "e2e4" — это 4 символа)
            if (string.IsNullOrWhiteSpace(input) || input.Length != 4)
                return false;

            input = input.ToLower();

            // Извлекаем символы
            char startFile = input[0]; // 'e'
            char startRank = input[1]; // '2'
            char endFile = input[2];   // 'e'
            char endRank = input[3];   // '4'

            // Валидация: проверяем, что буквы от a до h, а цифры от 1 до 8
            if (startFile < 'a' || startFile > 'h' || endFile < 'a' || endFile > 'h') return false;
            if (startRank < '1' || startRank > '8' || endRank < '1' || endRank > '8') return false;

            // Конвертируем шахматные координаты в индексы (0-7)
            // 'a' станет 0, 'b' станет 1 и т.д.
            int fromX = startFile - 'a';
            int toX = endFile - 'a';

            // Шахматная доска в массиве обычно перевернута: 
            // '1' строка — это самый низ доски (индекс 7 или 0, в зависимости от вашей логики)
            // Предположим, что 0 — это 8-я горизонталь, а 7 — это 1-я горизонталь:
            int fromY = (startRank - '1');
            int toY = (endRank - '1');

            from = new Vector2(fromX, fromY);
            to = new Vector2(toX, toY);

            return true;
        }
    }
}