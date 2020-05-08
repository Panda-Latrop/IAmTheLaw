using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DialogCloudStruct
{
    public SpriteRenderer cloud, triangle;
    public Transform arrow;
    public TMPro.TMP_Text text;
}


public class DialogCloudView : MonoBehaviour
{
    [SerializeField]
    protected DialogCloudStruct cloudStruct;
    protected bool hided;

    public void SetText(string _text)
    {
        cloudStruct.text.SetText(_text);
    }

    public void Hide(bool _hide)
    {
        if (hided != _hide)
        {
            cloudStruct.text.enabled = cloudStruct.triangle.enabled = cloudStruct.cloud.enabled = !_hide;
            hided = _hide;
        }
    }

}
