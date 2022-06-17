using System;
using System.Collections.Generic;
using System.Xml;
using UnityEditor;

namespace Engine.Data.Factories.Xml
{

    /// <summary>
    /// Загрузчик фабрики предметов
    /// ---
    /// 
    /// </summary>
    public class XmlFactoryLoaderItem : XmlFactoryLoaderBase<IItem>
    {

        public XmlFactoryLoaderItem()
        {
            FileNames = new[] {
                "Data/items_resources_data",
                "Data/items_interiors_data",
                "Data/items_tools_data",
                "Data/items_cloths_data",
                "Data/items_weapons_data",
                "Data/items_foods_data",
            };
        }

        protected override IItem ReadItem()
        {
            GroupType type = Enm<GroupType>("Type");
            IItem item;

            switch(type)
            {
                case GroupType.Resource:
                    var resource = new Resource();
                    ReadResource(resource);
                    item = resource;
                    break;
                case GroupType.ClothHead:
                case GroupType.ClothBody:
                case GroupType.ClothHand:
                case GroupType.ClothLegs:
                case GroupType.ClothBoot:
                    var cloth = new Cloth();
                    ReadBaseItem(cloth);
                    ReadCloth(cloth);
                    item = cloth;
                    break;
                case GroupType.WeaponGrenade:
                    var grenade = new GrenadeWeapon();
                    ReadBaseItem(grenade);
                    ReadBaseWeapon(grenade);
                    ReadGrenadeWeapon(grenade);
                    item = grenade;
                    break;
                case GroupType.WeaponFirearms:
                    var firearms = new FirearmsWeapon();
                    ReadBaseItem(firearms);
                    ReadBaseWeapon(firearms);
                    ReadFirearmsWeapon(firearms);
                    item = firearms;
                    break;
                case GroupType.WeaponEdged:
                    var edged = new EdgedWeapon();
                    ReadBaseItem(edged);
                    ReadBaseWeapon(edged);
                    ReadEdgedWeapon(edged);
                    item = edged;
                    break;
                case GroupType.Used:
                case GroupType.Food:
                case GroupType.MedKit:
                    var used = new Used();
                    ReadBaseItem(used);
                    ReadUsed(used);
                    item = used;
                    break;
                case GroupType.Item:
                case GroupType.Ammo:
                case GroupType.LocationObject:
                    item = new Item();
                    ReadBaseItem(item);
                    break;
                default:
                    throw new NotSupportedException();
            }

            return item;
        }

        private void ReadWeightValues(string weight, out long baseWeight, out long mul)
        {
            if (weight.EndsWith("kg"))
            {
                mul = 1000000; // 1000 * 1000
                baseWeight = long.Parse(weight.Substring(0, weight.Length - 2).Trim());
                return;
            }
            if (weight.EndsWith("mlg"))
            {
                mul = 1;
                baseWeight = long.Parse(weight.Substring(0, weight.Length - 3).Trim());
                return;
            }
            if (weight.EndsWith("g"))
            {
                mul = 1000;
                baseWeight = long.Parse(weight.Substring(0, weight.Length - 1).Trim());
                return;
            }

            throw new NotSupportedException("weight value not supported '" + weight + "'!");
        }

        private long ReadWeight(string weight)
        {
            long baseWeight;
            long mulWeight;
            ReadWeightValues(weight.ToLower(), out baseWeight, out mulWeight);
            return baseWeight * mulWeight;
        }

        private void ReadBaseItem(IItem item)
        {
            item.ID          = Lng("ID");
            item.Type        = Enm<GroupType>("Type");
            item.Name        = Str("Name");
            item.Description = Str("Description");
            item.Count       = 0;
            item.StackSize   = Lng("StackSize");
            item.ToolType    = EnmSplit<ToolType>("Tool");

            List<Part> parts = new List<Part>();
            foreach (XmlElement part in Current.GetElementsByTagName("Part"))
            {
                var resourceID           = Lng(part, "ResID");
                var resourceCount        = Lng(part, "ResCount");
                var difficulty           = Lng(part, "Difficulty");
                var neededTools  = EnmSplit<ToolType>(part, "NeededTools");
                parts.Add
                (
                    new Part()
                    {
                        ResourceID = resourceID,
                        ResourceCount = resourceCount,
                        Difficulty = difficulty,
                        NeededTools = neededTools,
                    }
                );
            }
            item.Parts = parts;
        }

        private void ReadUsed(IUsed used)
        {
            used.UseAction    = UsedItemActionsFactory.Instance.Get(Str("Action"));
            used.Weight       = ReadWeight(Str("Weight"));
            used.StaticWeight = Bol("StaticWeight"); 
            used.UseSoundType = Str("UseSoundType"); 
        }

        private void ReadCloth(ICloth cloth)
        {
            cloth.Protection = Int("Protection");
        }

        private void ReadResource(IResource resource)
        {
            resource.ID          = Lng("ID");
            resource.StackSize   = Lng("StackSize");

            var weight      = Str("Weight");
            resource.Weight      = ReadWeight(weight);

            resource.Name        = Str("Name");
            resource.Description = Str("Description");
        }

        private void ReadBaseWeapon(IWeapon weapon)
        {
            weapon.WeaponType  = Enm<WeaponType>("WeaponType");
            weapon.Damage      = Int("Damage");
            weapon.MaxDistance = Flt("MaxDistance");
            weapon.AimRadius   = Flt("AimRadius");
            weapon.UseAP       = Int("UseAP");
        }

        private void ReadFirearmsWeapon(IFirearmsWeapon weapon)
        {
            weapon.AmmoID          = Lng("AmmoID");
            weapon.AmmoStackSize   = Lng("AmmoStackSize");
            weapon.AmmoCount       = 0L;
            weapon.Penetration     = Byt("Penetration");
            weapon.ReloadAP        = Int("ReloadAP");

            weapon.AmmoEffectType  = Str("AmmoEffectType");
            weapon.ShootEffectType = Str("ShootEffectType");

            weapon.ShootSoundType   = Str("ShootSoundType");
            weapon.ReloadSoundType  = Str("ReloadSoundType");
            weapon.JammingSoundType = Str("JammingSoundType");
        }

        private void ReadGrenadeWeapon(IGrenadeWeapon weapon)
        {
            weapon.Radius            = Flt("Radius");

            weapon.GrenadeEffectType = Str("GrenadeEffectType");
            weapon.ExplodeEffectType = Str("ExplodeEffectType");

            weapon.ExplodeSoundType  = Str("ExplodeSoundType");
            weapon.ThrowSoundType    = Str("ThrowSoundType");
        }

        private void ReadEdgedWeapon(IEdgedWeapon weapon)
        {
            weapon.CanThrow          = Bol("CanThrow");
            weapon.ThrowAP           = Int("ThrowAP");
            weapon.ThrowDamage       = Lng("ThrowDamage");
            weapon.ThrowDistance     = Flt("ThrowDistance");
            weapon.ThrowAimRadius    = Flt("ThrowAimRadius");

            weapon.ThrowEffectType   = Str("ThrowEffectType");
            weapon.ThrowSoundType    = Str("ThrowSoundType");

            weapon.ThrowInSoundType  = Str("ThrowInSoundType");
            weapon.ThrowOutSoundType = Str("ThrowOutSoundType");
        }

    }

}
