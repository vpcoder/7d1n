using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Sdon.I18N.Model;
using System.IO;

namespace UnityEditor.Sdon.I18N {

	public class DictionaryView  {

		private static Color SPLITTER_COLOR = new Color(0f,0f,0.2f);

		private const int BUTTON_SIZE = 21;

		private DatasTableListener datasListener = new DatasTableListener();
		private List<Dictionary>   dictionaries = new List<Dictionary>(); // Файл словаря

		private string[] dictionariesTitles = new string[] { };
        private string[] langsTitles        = new string[]{ };
		private string newDictionaryName;
		private int currentTab  = 0;
        private int prevTab     = 0;
        private int currentLang = 0;

		private List<Dictionary> tmpDictionaries = new List<Dictionary>();
		private bool isChangedStatus = false;

		private Vector2 scrollPos;

		public DictionaryView() : base() {

			ReloadDictionaries();

		}

		public void AddItemSelectListener(IItemSelectListener itemSelectListener) {
			datasListener.AddItemSelectListener(itemSelectListener);
		}

		private void LoadDictionariesList(ref List<Dictionary> dictionaries) {

			dictionaries.Clear();
			dictionaries = new List<Dictionary>();
			
			foreach(string xmlFile in Directory.GetFiles(Engine.I18N.Dictionary.EDITOR.I18N_DIRECTORY,"*.xml")) {
				dictionaries.Add(XMLDictionaryService.getInstance().ReadDictionary(xmlFile));
			}

		}

		private void CheckChangedData() {

			if (dictionaries.Count != tmpDictionaries.Count) {
				ReloadDictionaries();
				return;
			}

			if (dictionaries.Count == 0 || tmpDictionaries.Count == 0) {
				return;
			}

			if (!dictionaries[currentTab].Equals(tmpDictionaries[currentTab])) {
				isChangedStatus = true;
				return;
			}

			isChangedStatus = false;

		}

		private void ReloadDictionaries() {

			LoadDictionariesList(ref this.dictionaries);
			LoadDictionariesList(ref this.tmpDictionaries); // загружаем временный список для сравнения изменений

			isChangedStatus = false;

			RefreshTabs();
            RefreshLangsTitles();
		}

		private void RefreshTabs() {

			List<string> dictionariesTitlesList = new List<string>();
			foreach (Dictionary dictionary in dictionaries) {
				dictionariesTitlesList.Add(dictionary.Name);
			}
			dictionariesTitles = dictionariesTitlesList.ToArray();

			if (dictionariesTitles.Length == 0) {
				dictionariesTitles = new string[] { "Пусто" };
			}

		}


        private void RefreshLangsTitles() {
            
            List<string> langsTitlesList = new List<string>();

			if (currentTab >= dictionaries.Count || currentTab<0 || dictionaries[currentTab].Langs.Count == 0) {
				langsTitles = new string[] { "Пусто" };
				return;
			}

            foreach (Lang lang in dictionaries[currentTab].Langs) {
                langsTitlesList.Add(lang.Language);
            }

            langsTitles = langsTitlesList.ToArray();

        }

		public void OnDrawView() {

			CheckChangedData();

			scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

			EditorGUILayout.BeginVertical();

				if (currentTab >= 0 && dictionaries.Count > 0) {
					DrawDictionaryToolsPanel(dictionaries[currentTab]);
				}

				EditorGUILayout.BeginHorizontal();
				
					GUILayout.Label("Словари:",GUILayout.Width(64));
					GUILayout.Space(16);
					currentTab = GUILayout.Toolbar(currentTab,dictionariesTitles);

                    if (prevTab != currentTab) {
                        RefreshLangsTitles();
                    }

                    prevTab = currentTab;
					GUILayout.Space(16);

					if (currentTab >= dictionaries.Count) {
						currentTab=dictionaries.Count-1;
					}

					if (currentTab >= 0 && dictionaries.Count > 0) {

						DrawEditPanel(dictionaries[currentTab]);
					}

				EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                DrawLanguagePanel();
                EditorGUILayout.EndHorizontal();

			EditorGUILayout.EndVertical();

				EditorGUILayout.Separator();
				Tables.Splitter(SPLITTER_COLOR,2);
				EditorGUILayout.Separator();

				if (dictionaries.Count > 0 && currentTab >= 0) {
					datasListener.DrawDataView(dictionaries[currentTab].Langs[currentLang].Datas);
				}

			EditorGUILayout.EndScrollView();
			
		}

        private void DrawLanguagePanel(){
            
            GUILayout.Label("Язык:",GUILayout.Width(64),GUILayout.Height(21));

            currentLang = EditorGUILayout.Popup(currentLang, langsTitles, GUILayout.Width(96));

            if (GUILayout.Button(IconsFactory.Instance.GetIcon(Icons.Remove),GUILayout.Width(BUTTON_SIZE),GUILayout.Height(BUTTON_SIZE))) {

            }

            if (GUILayout.Button(IconsFactory.Instance.GetIcon(Icons.Add),GUILayout.Width(BUTTON_SIZE),GUILayout.Height(BUTTON_SIZE))) {

            }

        }

		private void DrawDictionaryToolsPanel(Dictionary dictionary) {

			EditorGUILayout.BeginHorizontal();

				if (!isChangedStatus) {
					GUILayout.Label("Изменений не обнаружено");
				} else {
					GUILayout.Label("Есть несохранённые изменения в текущем словаре");
				}

				GUILayout.FlexibleSpace();

				if (GUILayout.Button(IconsFactory.Instance.GetIcon(isChangedStatus? Icons.Save : Icons.SaveOff), GUILayout.Width(32), GUILayout.Height(24))) {

					if (!isChangedStatus) {
						return;
					}

						try {

							string filename = Engine.I18N.Dictionary.EDITOR.I18N_DIRECTORY + dictionary.Name + ".xml";
							XMLDictionaryService.getInstance().WriteDictionary(dictionary, filename);

							ReloadDictionaries();

						} catch (Exception) {

							

						}
					
					isChangedStatus = false;

				}

				if (GUILayout.Button(IconsFactory.Instance.GetIcon(Icons.Refresh), GUILayout.Width(32), GUILayout.Height(24))) {

					if (!isChangedStatus ||  EditorUtility.DisplayDialog("Обновление", "Вы не сохранили изменения в словарях, если продолжить обновление, все не сохранённые изменения будут потеряны. Вы действительно хотите продолжить?", "Да", "Нет")) {
						ReloadDictionaries();
					}

				}

				if (GUILayout.Button(IconsFactory.Instance.GetIcon(Icons.Edit), GUILayout.Width(32), GUILayout.Height(24))) {
				
					string filename = Directory.GetCurrentDirectory()+Engine.I18N.Dictionary.EDITOR.I18N_DIRECTORY+dictionary.Name + ".xml";
					System.Diagnostics.Process.Start(filename);

				}

			EditorGUILayout.EndHorizontal();

		}

		private void DrawEditPanel(Dictionary dictionary) {

			string filename = Engine.I18N.Dictionary.EDITOR.I18N_DIRECTORY+dictionary.Name+".xml";
			string dictionaryName = dictionary.Name;
			dictionary.Name = EditorGUILayout.TextField(dictionary.Name,GUILayout.Width(128),GUILayout.Height(21));
			
			if (dictionary.Name != dictionaryName) {

				try {

					Debug.LogWarning("move!");
					string newName = Engine.I18N.Dictionary.EDITOR.I18N_DIRECTORY+dictionary.Name+".xml";
					File.Move(filename,newName);
					RefreshTabs();

				} catch (Exception e) {

					Debug.LogWarning(e);
					dictionary.Name = dictionaryName;

				}

				dictionaryName = dictionary.Name;
			}

			if (GUILayout.Button("+ Словарь",GUILayout.Width(76),GUILayout.Height(BUTTON_SIZE))) {

				string newName;
				int index = 0;

				while (File.Exists(newName=Engine.I18N.Dictionary.EDITOR.I18N_DIRECTORY+"dictionary"+(index++).ToString()+".xml")) { }
				File.AppendAllText(newName,"<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<data>\r\n\t<lang name=\"ru\">\r\n\t</lang>\r\n</data>");
				dictionaries.Add(XMLDictionaryService.getInstance().ReadDictionary(newName));
				RefreshTabs();
			}

			if (GUILayout.Button("Удалить",GUILayout.Width(76),GUILayout.Height(BUTTON_SIZE))) {
				
				if(EditorUtility.DisplayDialog("Удаление словаря", "Вы действительно хотите удалить словарь '"+dictionary.Name+"' и связанный с ним файл?", "Да", "Нет")) {

					if (File.Exists(filename)) {
						File.Delete(filename);
						dictionaries.Remove(dictionary);

						if (currentTab >= dictionaries.Count) {
							currentTab=dictionaries.Count-1;
						}

						RefreshTabs();
					}
					
				}

			}

		}

	}

}
