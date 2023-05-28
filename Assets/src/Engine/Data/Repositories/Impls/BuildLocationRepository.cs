using Engine.DB;
using System;

namespace Engine.Data.Repositories
{

    public class BuildLocationRepository : RepositoryBase<BuildLocation, SqliteBuildLocationRepositoryProxy>
    {

        private static readonly Lazy<BuildLocationRepository> instance = new Lazy<BuildLocationRepository>(() => new BuildLocationRepository());
        public static BuildLocationRepository Instance { get { return instance.Value; } }

    }

}
