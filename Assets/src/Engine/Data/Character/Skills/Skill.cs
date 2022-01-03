using System;
using UnityEngine;

namespace Engine.Data
{

    [Serializable]
    public class Skill : ISkill
    {

        public string ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Sprite Sprite
        {
            get
            {
                return SkillFactory.Instance.GetSprite(ID);
            }
        }
    }

}
