
namespace Engine.Data
{

    ///<summary>
    ///
    /// Группы предметов и обьектов, на которые делятся любые предметы в мире
    /// ---
    /// The groups of objects and objects into which all objects in the world are divided
    /// 
    ///</summary>
    public enum GroupType : byte
    {

        Item            = 0x00, // Обычный предмет, без особых свойств и возможностей
        Resource        = 0x01, // Рессурсы - неделимые предметы
        
        Used            = 0x02, // расходники, используемый предмет
        MedKit          = 0x03, // аптечки, используемый предмет
        Food            = 0x04, // еда, используемый предмет

        ClothHead       = 0x10, // Элемент одежды, можно одеть на голову
        ClothBody       = 0x11, // Элемент одежды, можно одеть на тело
        ClothHand       = 0x12, // Элемент одежды, можно одеть на руки
        ClothLegs       = 0x13, // Элемент одежды, можно одеть на ноги
        ClothBoot       = 0x14, // Элемент одежды, можно одеть на ступни
        
        WeaponFirearms  = 0x20, // Огнестрел
        WeaponEdged     = 0x21, // Холодное оружие
        WeaponGrenade   = 0x22, // Гранаты

        Ammo            = 0x24, // Патроны и снаряды

        
        LocationObject  = 0xf0, // Объект на локации, любых габаритов, обычно это то что игрок разбирает

    };

}

