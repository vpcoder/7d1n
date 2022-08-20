
namespace Engine.Data
{

    ///<summary>
    ///
    /// Группы предметов и обьектов, на которые делятся любые предметы в мире
    /// ---
    /// The groups of objects and objects into which all objects in the world are divided
    /// 
    ///</summary>
    public enum GroupType
    {

        Item           , // Обычный предмет, без особых свойств и возможностей
        Resource       , // Рессурсы - неделимые предметы
        
        Used           , // расходники, используемый предмет
        Blueprint      , // Чертежи
        MedKit         , // аптечки, используемый предмет
        Food           , // еда, используемый предмет

        ClothHead      , // Элемент одежды, можно одеть на голову
        ClothBody      , // Элемент одежды, можно одеть на тело
        ClothHand      , // Элемент одежды, можно одеть на руки
        ClothLegs      , // Элемент одежды, можно одеть на ноги
        ClothBoot      , // Элемент одежды, можно одеть на ступни
        
        WeaponFirearms , // Огнестрел
        WeaponEdged    , // Холодное оружие
        WeaponGrenade  , // Гранаты

        Ammo           , // Патроны и снаряды

        LocationObject , // Объект на локации, любых габаритов, обычно это то что игрок разбирает

    };

}

