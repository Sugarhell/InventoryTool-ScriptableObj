using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(InventoryDatabase), true)]
public class inventoryDatabaseEditor : Editor {

    InventoryDatabase _target;

    public void OnEnable()
    {
        _target = (InventoryDatabase)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorUtility.SetDirty(_target);

        GUILayout.BeginVertical(); //1 Vertical

        EditorGUILayout.HelpBox("Create the items for the inventory database!", MessageType.Info);

        GUILayout.Space(10);

        if (GUILayout.Button("Create Item")){
            _target.itemDatabase.Add(CreateItem());
        }

        GUILayout.Space(10);

        EditorGUILayout.TextArea("", GUI.skin.horizontalSlider);
        GUILayout.Space(20);

        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.MiddleLeft;
        style.normal.textColor = Color.black;
        style.fontStyle = FontStyle.Bold;
        style.fontSize = 18;

        GUILayout.BeginVertical(EditorStyles.helpBox); //Open 2 Vertical
        GUILayout.Label("ITEMS", style);

        GUILayout.Space(5);

        ShowItems();
        
        GUILayout.EndVertical(); //Close 2 Vertical
        GUILayout.EndVertical(); // Close 1 Vertical
        
        
        serializedObject.ApplyModifiedProperties();
        
    }

    public Items CreateItem()
    {
        Items itemtmp = CreateInstance<Items>();
        string directory = Application.dataPath + "/Items";
        if (!System.IO.Directory.Exists(directory))
        {
            System.IO.Directory.CreateDirectory(directory);
        }
        AssetDatabase.CreateAsset(itemtmp, string.Format("Assets/{0}/{1}_{2}.asset", "Items", "Item", 1 + (System.IO.Directory.GetFiles(directory).Length / 2)));
        AssetDatabase.SaveAssets();
        return itemtmp;
    }

    public void RemoveItem(Items item)
    {
        _target.itemDatabase.Remove(item);

        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(item));
        
    }

    public Items DuplicateItem(Items item)
    {
        Items tmp = CreateInstance<Items>();
        tmp.Name = item.Name;
        tmp.hashID = item.hashID;
        tmp.itemIcon = item.itemIcon;

        return tmp;
    }

    public void SaveItem(Items item)
    {

    }

    public void ShowItems()
    {
        //Debug.Log(_target.itemDatabase);
        if (_target.itemDatabase == null) return;
        
        foreach (var c in _target.itemDatabase.ToArray())
        {
            GUILayout.BeginVertical("Box");
            GUILayout.Space(5);
            Editor tade = ItemEditor.CreateEditor(c);
            GUILayout.BeginHorizontal();
            GUIStyle style = new GUIStyle(GUI.skin.label);
            
            style.normal.textColor = Color.black;
            style.fontStyle = FontStyle.Bold;
            style.fontSize = 18;
            style.alignment = TextAnchor.MiddleCenter;

            EditorGUILayout.LabelField(c.Name, style, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            EditorGUILayout.TextArea("", GUI.skin.horizontalSlider);

            tade.OnInspectorGUI();
            //EditorGUILayout.HelpBox("Inventory Controls", MessageType.Info);
            GUILayout.BeginHorizontal();
            var style1 = new GUIStyle(GUI.skin.button);
            style1.normal.textColor = Color.red;
            style1.active.textColor = Color.green;
            //style.fontSize = 18;

            if (GUILayout.Button("Duplicate"))
            {
                _target.itemDatabase.Add(DuplicateItem(c));
            }

            if (GUILayout.Button("Remove Item", style1))
            {
                RemoveItem(c);
            }

            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.Space(10);
        }
    }
}
#endif
