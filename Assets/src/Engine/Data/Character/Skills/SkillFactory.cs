using System.Collections.Generic;
using UnityEngine;

namespace Engine.Data
{

    public static class SkillsDictionary
    {
        public const string SKILL = "";
    }

    public class SkillFactory
    {

        private static readonly Lazy<SkillFactory> instance = new Lazy<SkillFactory>(() => new SkillFactory());
        public static SkillFactory Instance { get { return instance.Value; } }


        private readonly IDictionary<string, ISkill> dataByID = new Dictionary<string, ISkill>();
        private readonly IDictionary<string, Sprite> spriteByID = new Dictionary<string, Sprite>();

        #region Ctor

        private SkillFactory()
        {
            AddSkill(SkillsDictionary.SKILL, "test", "test");
        }

        private void AddSkill(string id, string name, string description)
        {
            dataByID.Add(id, new Skill()
            {
                ID = id,
                Name = name,
                Description = description,
            });
        }

        #endregion

        public Sprite GetSprite(string id)
        {
            Sprite sprite = null;
            if (!spriteByID.TryGetValue(id, out sprite))
            {
                sprite = Resources.Load<Sprite>("Data/Skills/" + id);
                if(sprite == null)
                {
                    Debug.LogError("skill '" + id + "' hasn't be found!");
                    return null;
                }
                spriteByID.Add(id, sprite);
            }
            return sprite;
        }

        public ISkill Get(string id)
        {
            return dataByID[id];
        }

    }

}
