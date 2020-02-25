using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 1)]
public class Items : ScriptableObject {

    public string Name;
    public string hashID;
    public Sprite itemIcon;
    public AudioClip pickupSound;


    public void Play(AudioSource source)
    {
        if (pickupSound == null) return;
        source.clip = pickupSound;
        source.Play();
    }
}