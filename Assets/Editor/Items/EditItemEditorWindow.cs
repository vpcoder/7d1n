using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Engine;
using Engine.Data;
using Engine.Data.Factories;
using GitIntegration.Items.Data;
using Rotorz.ReorderableList;
using UnityEditor;
using UnityEngine;

namespace GitIntegration.Items
{

    public class EditItemEditorWindow : EditorWindow
    {

        private Vector2 scroll;
        
        // Базовые параметры
        private GroupType groupType;
        private long id;
        private long stackSize;
        private string txtName;
        private bool isStaticWeight;
        private string txtWeightValue;
        private PartListAdapter parts = new PartListAdapter();
        private ToolAdapter tool = new ToolAdapter();
        
        // Параметры оружия
        private WeaponType weaponType;
        private int damage;
        private float damageRadius;
        private float maxDistance;
        private float aimRadius;
        private int useAP;
        
        // Оружие ближнего боя
        private bool canThrow;
        private int throwAP;
        private int throwDamage;
        private float throwDistance;
        private float throwAimRadius;
        private string throwBulletObject;
        private string throwSound;
        private string throwHitSound;
        private string throwMissSound;

        public ItemRow Item { get; set; }

        public void Load(ItemRow rowItem)
        {
            Item = rowItem;
            var item = ItemFactory.Instance.Get(rowItem.ID);

            id = rowItem.ID;
            groupType = item.Type;
            stackSize = item.StackSize;
            txtName = item.Name?.Substring(0, item.Name.Length - 5); // -"_name"
            
            var staticWeight = rowItem.Element.GetAttribute("StaticWeight");
            isStaticWeight = !string.IsNullOrWhiteSpace(staticWeight) && bool.Parse(staticWeight);
            txtWeightValue = rowItem.Element.GetAttribute("Weight") ?? item.Weight + "mlg";
            
            if (item.Parts != null)
                parts.SetData(item.Parts.ToList());
            
            if(Sets.IsNotEmpty(item.ToolType))
                tool.SetData(item.ToolType);
            else
                tool.SetData(new HashSet<ToolType>());
            
            var weapon = item as Weapon;
            if (weapon != null)
            {
                weaponType = weapon.WeaponType;
                damage = weapon.Damage;
                maxDistance = weapon.MaxDistance;
                aimRadius = weapon.AimRadius;
                useAP = weapon.UseAP;
            }

            var edged = item as EdgedWeapon;
            if (edged != null)
            {
                damageRadius = edged.DamageRadius;
                canThrow = edged.CanThrow;
                throwAP = edged.ThrowAP;
                throwDamage = edged.ThrowDamage;
                throwDistance = edged.ThrowDistance;
                throwAimRadius = edged.ThrowAimRadius;
                throwBulletObject = edged.ThrowBulletObject;
                throwSound = edged.ThrowSound;
                throwHitSound = edged.ThrowHitSound;
                throwMissSound = edged.ThrowMissSound;
            }
        }
        
        private void OnGUI()
        {
            scroll = GUILayout.BeginScrollView(scroll);
            BaseItemBlock();
            
            switch (groupType)
            {
                case GroupType.WeaponEdged:
                    WeaponItemBlock();
                    WeaponEdgedItemBlock();
                    break;
                case GroupType.WeaponFirearms:
                    WeaponItemBlock();
                    break;
                case GroupType.WeaponGrenade:
                    WeaponItemBlock();
                    break;
            }
            
            GUILayout.EndScrollView();
            
            GUILayout.Space(30);
            if (GUILayout.Button("Сохранить | Save"))
            {
                Save(Item);
                Item.Document.Save(Item.FileName);
            }
        }
        
        private void Save(ItemRow item)
        {
            var element = item.Element;
            SaveBaseItemInfo(element, item.Document);

            switch (groupType)
            {
                case GroupType.WeaponEdged:
                    SaveBaseWeaponInfo(element, item.Document);
                    SaveEdgedWeaponInfo(element, item.Document);
                    break;
                case GroupType.WeaponFirearms:
                    SaveBaseWeaponInfo(element, item.Document);
                    break;
                case GroupType.WeaponGrenade:
                    SaveBaseWeaponInfo(element, item.Document);
                    break;
            }
        }

        public void SaveBaseItemInfo(XmlElement element, XmlDocument doc)
        {
            element.InnerXml = "";
            
            element.SetAttribute("ID", id.ToString());
            element.SetAttribute("Name", txtName + "_name");
            element.SetAttribute("Description", txtName + "_desc");
            element.SetAttribute("Type", groupType.ToString());

            if (isStaticWeight && groupType != GroupType.Resource)
            {
                element.SetAttribute("StaticWeight", "true");
                element.SetAttribute("Weight", txtWeightValue);
            }
            if (groupType == GroupType.Resource)
            {
                element.SetAttribute("Weight", txtWeightValue);
            }
            
            element.SetAttribute("StackSize", GroupsWithoutStacks.Contains(groupType) ? "1" : stackSize.ToString());

            if (tool.IsNotEmpty)
            {
                var tools = string.Join(";", tool.Tools.Select(tool => tool.ToString()));
                element.SetAttribute("Tool", tools);
            }
            
            if (!parts.IsEmpty && groupType != GroupType.Resource)
            {
                foreach (var part in parts.Data)
                {
                    var partElement = doc.CreateElement("Part");
                    partElement.SetAttribute("ResID", part.ResourceID.ToString());
                    partElement.SetAttribute("ResCount", part.ResourceCount.ToString());
                    partElement.SetAttribute("Difficulty", part.Difficulty.ToString());
                    if (Sets.IsNotEmpty(part.NeededTools))
                    {
                        var tools = string.Join(";", part.NeededTools.Select(tool => tool.ToString()));
                        partElement.SetAttribute("NeededTools", tools);
                    }
                    element.AppendChild(partElement);
                }
            }
        }

        private void SaveBaseWeaponInfo(XmlElement element, XmlDocument doc)
        {
            element.SetAttribute("WeaponType", weaponType.ToString());
            element.SetAttribute("Damage", damage.ToString());
            element.SetAttribute("MaxDistance", maxDistance.ToString());
            element.SetAttribute("AimRadius", aimRadius.ToString());
            element.SetAttribute("UseAP", useAP.ToString());
        }
        
        private void SaveEdgedWeaponInfo(XmlElement element, XmlDocument doc)
        {
            element.SetAttribute("DamageRadius", damageRadius.ToString());
            element.SetAttribute("CanThrow", canThrow.ToString());
            if (canThrow)
            {
                element.SetAttribute("ThrowAP", throwAP.ToString());
                element.SetAttribute("ThrowDamage", throwDamage.ToString());
                element.SetAttribute("ThrowDistance", throwDistance.ToString());
                element.SetAttribute("ThrowAimRadius", throwAimRadius.ToString());
                element.SetAttribute("ThrowBulletObject", throwBulletObject);
                element.SetAttribute("ThrowSound", throwSound);
                element.SetAttribute("ThrowHitSound", throwHitSound);
                element.SetAttribute("ThrowMissSound", throwMissSound);
            }
        }

        private bool baseGroupVisible = true;
        private void BaseItemBlock()
        {
            baseGroupVisible = EditorGUILayout.Foldout(baseGroupVisible, "Базовые параметры | Base parameters");
            if (!baseGroupVisible)
                return;
            
            GUILayout.Space(16);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Тип предмета | item type:");
            groupType = (GroupType)EditorGUILayout.EnumPopup(groupType);
            GUILayout.EndHorizontal();

            long idStart = GetIdInfoByType();
            GUILayout.Label("Диапазон ИД с " + idStart + "+" + " | Id range from " + idStart + "+");
            id = EditorGUILayout.LongField("ИД предмета | item id:", id);
            if (ItemFactory.Instance.Exists(id))
                GUILayout.Label("ИД предмета уже занят | Item ID already exists", Style(Color.red));

            GUILayout.BeginHorizontal();
            var texture = SpriteFactory.Instance.Get(id)?.texture;
            if (texture != null)
            {
                GUI.DrawTexture(new Rect()
                {
                    x = 0,
                    y = 110,
                    width = 128,
                    height = 128,
                }, texture);
            }
            GUILayout.Space(140);
            GUILayout.BeginVertical();
            
            var nameText = Localization.Instance.GetUnsafe(txtName + "_name");
            var descriptionText = Localization.Instance.GetUnsafe(txtName + "_desc");
            txtName = EditorGUILayout.TextField("Название | Name", txtName);
            if(nameText != null)
                GUILayout.Label("Название | Name: " + nameText, Style(Color.green));
            if(descriptionText != null)
                GUILayout.Label("Описание | Description: " + descriptionText, Style(Color.green));
            
            if(groupType != GroupType.Resource)
                isStaticWeight = GUILayout.Toggle(isStaticWeight, "Статичный вес | Static weight");
            if(isStaticWeight || groupType == GroupType.Resource)
                txtWeightValue = EditorGUILayout.TextField("Вес | Weight", txtWeightValue);
            
            if(!GroupsWithoutStacks.Contains(groupType))
                stackSize = EditorGUILayout.LongField("Размер стека | Stack size:", stackSize);

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            if (groupType != GroupType.Resource)
            {
                GUILayout.Space(90);
                ReorderableListGUI.Title("Части | Parts");
                ReorderableListGUI.ListField(parts);
                
                GUILayout.Space(16);
                GUILayout.Label("Свойства инструмента | Tool properties:");
                var pos = GUILayoutUtility.GetLastRect();
                pos.y += pos.height;
                pos.height = 96;
                tool.DrawTools(pos);
                GUILayout.Space(96);
            }
            
            GUILayout.Space(16);
        }
        
        private bool baseWeaponGroupVisible = true;
        private void WeaponItemBlock() {
            baseWeaponGroupVisible = EditorGUILayout.Foldout(baseWeaponGroupVisible, "Базовые параметры оружия | Weapon basic parameters");
            if (!baseWeaponGroupVisible)
                return;
            
            GUILayout.Space(16);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Анимация | Animation:");
            weaponType = (WeaponType)EditorGUILayout.EnumPopup(weaponType);
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Урон | Damage:");
            damage = EditorGUILayout.IntField(damage);
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Предельная дистанция | Max distance:");
            maxDistance = EditorGUILayout.FloatField(maxDistance);
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Радиус прицельной атаки | Aim attack radius:");
            aimRadius = EditorGUILayout.FloatField(aimRadius);
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Стоимость атаки ОД | Attack AP cost:");
            useAP = EditorGUILayout.IntField(useAP);
            GUILayout.EndHorizontal();
            
            GUILayout.Space(16);
        }

        
        private bool edgedWeaponGroupVisible = true;
        private void WeaponEdgedItemBlock() {
            edgedWeaponGroupVisible = EditorGUILayout.Foldout(edgedWeaponGroupVisible, "Параметры оружия ближнего боя | Melee weapons parameters");
            if (!edgedWeaponGroupVisible)
                return;
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Радиус атаки | Damage Radius:");
            damageRadius = EditorGUILayout.FloatField(damageRadius);
            GUILayout.EndHorizontal();
            
            GUILayout.Space(16);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Можно метнуть | Can throw:");
            canThrow = EditorGUILayout.Toggle(canThrow);
            GUILayout.EndHorizontal();

            if (canThrow)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Стоимость ОД для метания | Damage:");
                throwAP = EditorGUILayout.IntField(throwAP);
                GUILayout.EndHorizontal();
            
                GUILayout.BeginHorizontal();
                GUILayout.Label("Урон от метания | Throw damage:");
                throwDamage = EditorGUILayout.IntField(throwDamage);
                GUILayout.EndHorizontal();
                
                GUILayout.BeginHorizontal();
                GUILayout.Label("Дистанция метания | Throw distance:");
                throwDistance = EditorGUILayout.FloatField(throwDistance);
                GUILayout.EndHorizontal();
                
                GUILayout.BeginHorizontal();
                GUILayout.Label("Радиус прицела метания | Throw aim radius:");
                throwAimRadius = EditorGUILayout.FloatField(throwAimRadius);
                GUILayout.EndHorizontal();
                
                GUILayout.BeginHorizontal();
                GUILayout.Label("Снаряд метания | Throw bullet:");
                throwBulletObject = EditorGUILayout.TextField(throwBulletObject);
                GUILayout.EndHorizontal();
                
                GUILayout.BeginHorizontal();
                GUILayout.Label("Звук метания | Throwing sound:");
                throwSound = EditorGUILayout.TextField(throwSound);
                GUILayout.EndHorizontal();
                
                GUILayout.BeginHorizontal();
                GUILayout.Label("Звук попадания | The sound of a hit:");
                throwHitSound = EditorGUILayout.TextField(throwHitSound);
                GUILayout.EndHorizontal();
                
                GUILayout.BeginHorizontal();
                GUILayout.Label("Звук промаха | The sound of a miss:");
                throwMissSound = EditorGUILayout.TextField(throwMissSound);
                GUILayout.EndHorizontal();
            }
            
            GUILayout.Space(16);
        }
        private long GetIdInfoByType()
        {
            switch (groupType)
            {
                case GroupType.Resource:
                    return 0;
                case GroupType.WeaponFirearms:
                    return 1000;
                case GroupType.Ammo:
                    return 2000;
                case GroupType.WeaponEdged:
                    return 3000;
                case GroupType.WeaponGrenade:
                    return 4000;
                case GroupType.Item:
                    return 5000;
                case GroupType.Used:
                    return 7000;
                case GroupType.MedKit:
                    return 8000;
                case GroupType.Food:
                    return 9000;
                case GroupType.ClothBody:
                    return 10000;
                case GroupType.ClothHead:
                    return 11000;
                case GroupType.ClothHand:
                    return 12000;
                case GroupType.ClothLegs:
                    return 13000;
                case GroupType.ClothBoot:
                    return 14000;
                case GroupType.LocationObject:
                    return 100000;
                default:
                    return 20000;
            }
        }
        
        public GUIStyle Style(Color color)
        {
            var style = new GUIStyle(EditorStyles.miniLabel);
            style.normal.textColor = color;
            return style;
        }

        private static readonly GroupType[] GroupsWithoutStacks =
        {
            GroupType.ClothBody, GroupType.ClothBoot, GroupType.ClothHand, GroupType.ClothHead, GroupType.ClothLegs,
            GroupType.LocationObject, GroupType.WeaponEdged, GroupType.WeaponFirearms, GroupType.WeaponGrenade
        };

    }
    
}
