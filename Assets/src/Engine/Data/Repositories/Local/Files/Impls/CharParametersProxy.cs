
namespace Engine.Data.Repositories
{

    public class CharParametersProxy : FileRepositoryProxyBase<ParametersRepositoryData>
    {

        public override string DirName => "Chars";
        public override string FileName => "Parameters";

        public override ParametersRepositoryData CreateDefault()
        {
            return new Parameters().CreateData();
        }

    }

}
