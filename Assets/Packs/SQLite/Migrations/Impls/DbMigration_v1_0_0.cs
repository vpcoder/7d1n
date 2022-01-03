using System;

namespace Engine.DB.Migrations
{

	public class DbMigration_v1_0_0 : MigrationBase
	{
		public override Version Version { get { return new Version("1.0.0.0"); } }

		public override void DoMigration(SQLiteConnection connection)
		{

            // TODO: Добавить логику ChangeSet-ов, которая позволит проливать частичные изменения, не конфликтующие друг с другом

        }
	}

}
