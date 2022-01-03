using System;

namespace com.baensi.sdon.db.dao.generator
{

    public interface IGenerator
    {

        int Id { get; }

        int Next { get; }

        void Update();

    }

}
