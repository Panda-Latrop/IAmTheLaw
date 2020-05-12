using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CharaterType
{
    prosecutor,
    lawyer,
    civil,
    civil_color
}
[System.Serializable]
public struct CharaterAtlas
{
    public CharaterType type;
    public Sprite[] head,body;
}

[CreateAssetMenu(fileName = "New Atlas", menuName = "Generator/Atlas", order = 52)] // 1
public class ScriptableGeneratorAtlas : ScriptableObject
{
    public CharaterAtlas[] atlas;

    public void Generate(CharaterType _type, GenCharacter _character)
    {
        int i;
        for (i = 0; i < atlas.Length; i++)
        {
            if (atlas[i].type == _type)
            {
                _character.head.sprite = atlas[i].head[Random.Range(0, atlas[i].head.Length)];
                _character.body.sprite = atlas[i].body[Random.Range(0, atlas[i].body.Length)];
                return;
            }
        }      
    }
}
