
namespace Engine.Data
{

    public interface ICraftableItem : IItem
    {
        long Level { get; set; }
        string Author { get; set; }
    }

}
