using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public CharaterType type;
    public ScriptableGeneratorAtlas atlas;
    public GenCharacter character;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            atlas.Generate(type, character);
        }
        if (Input.GetMouseButtonDown(1))
        {
            switch (type)
            {
                case CharaterType.prosecutor:
                    type = CharaterType.lawyer;
                    break;
                case CharaterType.lawyer:
                    type = CharaterType.civil;
                    break;
                case CharaterType.civil:
                    type = CharaterType.civil_color;
                    break;
                case CharaterType.civil_color:
                    type = CharaterType.prosecutor;
                    break;
                default:
                    break;
            }  
        }
    }
}
