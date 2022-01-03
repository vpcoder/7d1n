using System;
using System.Linq;
using System.Collections.Generic;
using NLog;

namespace com.baensi.sdon.db.dao
{

	// Autogen Factory
	/// Date: 31.12.2019 15:45:20
	public class DaoFactory
	{

		#region Singleton

		private static DaoFactory _instance = new DaoFactory();

		public static DaoFactory Instance
		{
			get
			{
				return _instance;
			}
		}

		#endregion

		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		#region Properties

			// Autogen Build Repository
			/// <summary>
			/// Репозиторий для таблицы 'build', для работы с сущностью Build
			/// </summary>
			public BuildRepository Build { get; }

			// Autogen BuildEnemy Repository
			/// <summary>
			/// Репозиторий для таблицы 'build_enemy', для работы с сущностью BuildEnemy
			/// </summary>
			public BuildEnemyRepository BuildEnemy { get; }

			// Autogen BuildLoot Repository
			/// <summary>
			/// Репозиторий для таблицы 'build_loot', для работы с сущностью BuildLoot
			/// </summary>
			public BuildLootRepository BuildLoot { get; }

			// Autogen Camp Repository
			/// <summary>
			/// Репозиторий для таблицы 'camp', для работы с сущностью Camp
			/// </summary>
			public CampRepository Camp { get; }

			// Autogen CampBuild Repository
			/// <summary>
			/// Репозиторий для таблицы 'camp_build', для работы с сущностью CampBuild
			/// </summary>
			public CampBuildRepository CampBuild { get; }

			// Autogen CampLoot Repository
			/// <summary>
			/// Репозиторий для таблицы 'camp_loot', для работы с сущностью CampLoot
			/// </summary>
			public CampLootRepository CampLoot { get; }

			// Autogen Group Repository
			/// <summary>
			/// Репозиторий для таблицы 'group', для работы с сущностью Group
			/// </summary>
			public GroupRepository Group { get; }

			// Autogen GroupBuild Repository
			/// <summary>
			/// Репозиторий для таблицы 'group_build', для работы с сущностью GroupBuild
			/// </summary>
			public GroupBuildRepository GroupBuild { get; }

			// Autogen GroupLoot Repository
			/// <summary>
			/// Репозиторий для таблицы 'group_loot', для работы с сущностью GroupLoot
			/// </summary>
			public GroupLootRepository GroupLoot { get; }

			// Autogen GroupMember Repository
			/// <summary>
			/// Репозиторий для таблицы 'group_member', для работы с сущностью GroupMember
			/// </summary>
			public GroupMemberRepository GroupMember { get; }

			// Autogen User Repository
			/// <summary>
			/// Репозиторий для таблицы 'user', для работы с сущностью User
			/// </summary>
			public UserRepository User { get; }

			// Autogen UserBag Repository
			/// <summary>
			/// Репозиторий для таблицы 'user_bag', для работы с сущностью UserBag
			/// </summary>
			public UserBagRepository UserBag { get; }

			// Autogen UserDevice Repository
			/// <summary>
			/// Репозиторий для таблицы 'user_device', для работы с сущностью UserDevice
			/// </summary>
			public UserDeviceRepository UserDevice { get; }

			// Autogen UserEquip Repository
			/// <summary>
			/// Репозиторий для таблицы 'user_equip', для работы с сущностью UserEquip
			/// </summary>
			public UserEquipRepository UserEquip { get; }

			// Autogen UserExp Repository
			/// <summary>
			/// Репозиторий для таблицы 'user_exp', для работы с сущностью UserExp
			/// </summary>
			public UserExpRepository UserExp { get; }

			// Autogen UserItem Repository
			/// <summary>
			/// Репозиторий для таблицы 'user_item', для работы с сущностью UserItem
			/// </summary>
			public UserItemRepository UserItem { get; }

			// Autogen UserSkill Repository
			/// <summary>
			/// Репозиторий для таблицы 'user_skill', для работы с сущностью UserSkill
			/// </summary>
			public UserSkillRepository UserSkill { get; }

			// Autogen UserState Repository
			/// <summary>
			/// Репозиторий для таблицы 'user_state', для работы с сущностью UserState
			/// </summary>
			public UserStateRepository UserState { get; }
		#endregion

		public DaoFactory()
		{
			this.Build = new BuildRepository();
			this.BuildEnemy = new BuildEnemyRepository();
			this.BuildLoot = new BuildLootRepository();
			this.Camp = new CampRepository();
			this.CampBuild = new CampBuildRepository();
			this.CampLoot = new CampLootRepository();
			this.Group = new GroupRepository();
			this.GroupBuild = new GroupBuildRepository();
			this.GroupLoot = new GroupLootRepository();
			this.GroupMember = new GroupMemberRepository();
			this.User = new UserRepository();
			this.UserBag = new UserBagRepository();
			this.UserDevice = new UserDeviceRepository();
			this.UserEquip = new UserEquipRepository();
			this.UserExp = new UserExpRepository();
			this.UserItem = new UserItemRepository();
			this.UserSkill = new UserSkillRepository();
			this.UserState = new UserStateRepository();
		}

	}

}

