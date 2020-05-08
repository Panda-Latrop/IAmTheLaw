using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BrideStruct
{
    public SpriteRenderer spriteBride, spriteDarkness;
    public TMPro.TMP_Text textBride;
}
public class BrideView : MonoBehaviour
{
    [SerializeField]
    protected BrideStruct bride;
    protected string text;
    public bool IsHide { get {return !bride.spriteBride.enabled; } }
    public void SetText(TextAsset _text,bool _imprison,int _size)
    {
        XMLLoader.LoadBribe(_text, ref text, _imprison, _size);
        bride.textBride.SetText(text);
    }

    public void Hide()
    {
        bride.spriteBride.enabled = false;
        bride.spriteDarkness.enabled = false;
        bride.textBride.enabled = false;
    }
    public void Unhide()
    {
        bride.spriteBride.enabled = true;
        bride.spriteDarkness.enabled = true;
        bride.textBride.enabled = true;
    }
}
