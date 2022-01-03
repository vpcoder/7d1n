
namespace Engine.Data.Stories
{

    public class CharParametersProxy : FileStoryProxyBase<ParametersStoryData>
    {

        public override string DirName => "Chars";
        public override string FileName => "Parameters";

        public override ParametersStoryData CreateDefault()
        {
            return new Parameters().CreateData();
        }

    }

}
