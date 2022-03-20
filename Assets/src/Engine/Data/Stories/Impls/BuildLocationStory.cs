using Engine.DB;
using System;

namespace Engine.Data.Stories
{

    public class BuildLocationStory : StoryBase<BuildLocation, SqliteBuildLocationStoryProxy>
    {

        private static readonly Lazy<BuildLocationStory> instance = new Lazy<BuildLocationStory>(() => new BuildLocationStory());
        public static BuildLocationStory Instance { get { return instance.Value; } }

    }

}
