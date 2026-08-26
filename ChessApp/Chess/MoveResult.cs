



namespace Chess
{
    enum MoveResult
    {
        GameOver,
        IllegalMove, 
        Success,
        PromotionRequired,
        Check,
        Checkmate,
        Stalemate,
        Draw

    }
}

/*

    // Игра завершена
    GameOver,

// Ход невозможен (фигура так не ходит, мешают другие фигуры, или король под шахом)
    IllegalMove,   

    // Обычный успешный ход
    Success,       
    
    PromotionRequired (Рекомендуется) — «требуется превращение». Превращение для пешки

    // Ход успешный, и вражескому королю объявлен шах
    Check,         

    // Мат! Игра завершена победой текущего игрока
    Checkmate,     

    // Пат (нет доступных ходов) — ничья
    Stalemate,     

    // Ничья по другим причинам (трикратное повторение, правило 50 ходов, нехватка фигур)
    Draw
*/