
namespace Engine.Data {

	/// <summary>
	/// 
	/// Особый навык персонажа, которым он может обладать
	/// ---
	/// A character's special skill that he can possess
	/// 
	/// </summary>
	public interface ISkill
    {
		
	    /// <summary>
	    ///		Идентификатор навыка.
	    ///		Уникальное обозначение навыка, однозначно определяющее его среди всех навыков
	    ///		---
	    /// 	Skill identifier.
	    /// 	Unique identifier of the skill, uniquely identifying it among all skills
	    /// </summary>
		string ID          { get; }
	    
	    /// <summary>
	    ///		Локализованное сообщение описывающее название навыка
	    ///		---
	    ///		Localized message describing the name of the skill
	    /// </summary>
		string Name        { get; }
	    
	    /// <summary>
	    ///		Локализованное сообщение описывающее подробную информацию о навыке
	    ///		---
	    ///		Localized message describing detailed information about the skill
	    /// </summary>
		string Description { get; }

    }
	
}
