using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
[CustomEditor(typeof(Items), true)]
public class ItemEditor : Editor {

    Items _target;
    [SerializeField] private AudioSource _previewer;

    public void OnEnable()
    {
        _target = (Items)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorUtility.SetDirty(_target);
        EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Item Name");
        _target.Name = EditorGUILayout.TextField(_target.Name, GUILayout.ExpandWidth(true));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Item ID");
        _target.hashID = EditorGUILayout.TextField(_target.hashID, GUILayout.ExpandWidth(true));
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Generate Hash", GUILayout.Width(150)))
        {
            _target.hashID = md5(_target.Name);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(5);

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Item Icon");
        _target.itemIcon = (Sprite)EditorGUILayout.ObjectField(_target.itemIcon, typeof(Sprite), true, GUILayout.Width(80), GUILayout.Height(80));
        GUILayout.EndHorizontal();

        //GUILayout.BeginHorizontal();
        //EditorGUILayout.LabelField("Pickup Sound");
        //item.pickupSound = (AudioClip)EditorGUILayout.ObjectField(item.pickupSound, typeof(AudioClip), GUILayout.ExpandWidth(true));
        //GUILayout.EndHorizontal();


        GUILayout.EndVertical();

        EditorGUI.EndDisabledGroup();

        serializedObject.ApplyModifiedProperties();
    }

    public static string md5(string str)
    {
        System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
        byte[] bytes = encoding.GetBytes(str);
        var sha = new System.Security.Cryptography.MD5CryptoServiceProvider();
        return System.BitConverter.ToString(sha.ComputeHash(bytes));
    }
}

#endif