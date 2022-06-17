
namespace Engine.Data
{

    /// <summary>
    /// Тип инструмента
    /// </summary>
    public enum ToolType : int
    {
        /// <summary>
        /// Не инструмент
        /// </summary>
        None = 0x00,

        
        /// <summary>
        ///     Плоская отвёртка
        ///     ---
        ///     
        /// </summary>
        FlatheadScrewdriver,
        
        /// <summary>
        ///     Крестовая отвёртка
        ///     ---
        ///     
        /// </summary>
        PhillipsScrewdriver,
        
        /// <summary>
        ///     Молоток
        ///     ---
        ///     
        /// </summary>
        Hammer,
        
        /// <summary>
        ///     Пила
        ///     ---
        /// 
        /// </summary>
        Saw,
        
        /// <summary>
        ///     Линейка-угол
        ///     ---
        /// 
        /// </summary>
        Line,
        
        /// <summary>
        ///     Лопата
        ///     ---
        ///     
        /// </summary>
        Shovel,
        
        /// <summary>
        ///     Кусачки
        ///     ---
        /// 
        /// </summary>
        Clippers,
        
        /// <summary>
        ///     Гаечный ключ типа А
        ///     ---
        ///     
        /// </summary>
        WrenchTypeA,
        
        /// <summary>
        ///     Гаечный ключ типа Б
        ///     ---
        ///     
        /// </summary>
        WrenchTypeB,
        
        /// <summary>
        ///     Гаечный ключ типа В
        ///     ---
        ///     
        /// </summary>
        WrenchTypeC,
        
        /// <summary>
        ///     Плоскогубцы
        ///     ---
        ///     
        /// </summary>
        Pliers,
        
        /// <summary>
        ///     Кирка
        ///     ---
        ///     
        /// </summary>
        Pickaxe,

        /// <summary>
        ///     Топор
        ///     ---
        ///     
        /// </summary>
        Axe,
        
        /// <summary>
        ///     Нож
        ///     ---
        /// 
        /// </summary>
        Knife,
        
        /// <summary>
        ///     Лом/Фомка
        ///     ---
        /// 
        /// </summary>
        Crowbar,
        
    }

}
