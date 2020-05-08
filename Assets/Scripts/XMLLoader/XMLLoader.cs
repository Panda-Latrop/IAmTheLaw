
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System;

public class XMLLoader
{

    public static string FindNumber(TextAsset _xmlFile)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(_xmlFile.text);
        XmlNode casen = xmlDoc.DocumentElement.SelectSingleNode("case");
        XmlNode number = casen.SelectSingleNode("number");
        if (number != null)
            return number.InnerText;
        else
            return null;
    }

    public static int LoadDocument(TextAsset _xmlFile, ref string _number, ref string[] _pages)
    {
        int count = 0;
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(_xmlFile.text);
        XmlNode casen = xmlDoc.DocumentElement.SelectSingleNode("case");
        XmlNode number = casen.SelectSingleNode("number");
        XmlNodeList pages = casen.SelectNodes("page");
        if (number != null)
            _number = number.InnerText;
        _pages = new string[pages.Count];
        for (int i = 0; i < pages.Count; i++)
        {
            _pages[count] = pages[i].InnerText;
            count++;
        }
        return count;
    }
    public static void LoadBribe(TextAsset _xmlFile, ref string _bribe, bool _imprison, int _size, int insertPos1 = 0, int insertPos2 = 1)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(_xmlFile.text);
        XmlNode bribe = xmlDoc.DocumentElement.SelectSingleNode("bribe");
        if (bribe != null)
        {
            XmlNodeList msg = bribe.SelectNodes("msg");
            if (msg.Count > insertPos1 && msg.Count > insertPos2)
            {
                for (int i = 0; i < msg.Count; i++)
                {
                    _bribe += msg[i].InnerText;
                    if (i == insertPos1)
                    {
                        if (_imprison)
                            _bribe += bribe.Attributes["nega"].Value;
                        else
                            _bribe += bribe.Attributes["posi"].Value;
                    }
                    if (i == insertPos2)
                    {
                        _bribe += _size;
                    }
                }
            }
            else
            {
                for (int i = 0; i < msg.Count; i++)
                {
                    _bribe += msg[i].InnerText;
                }
                if (_imprison)
                    _bribe += bribe.Attributes["nega"].Value;
                else
                    _bribe += bribe.Attributes["posi"].Value;
                _bribe += _size;
            }
        }
    }
    public static void LoadSkills(TextAsset _xmlFile, string _side, ref string[] _skillTexts)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(_xmlFile.text);
        XmlNodeList sides = xmlDoc.DocumentElement.SelectNodes("side");
        XmlNode side = null;
        for (int i = 0; i < sides.Count; i++)
        {
            if (sides[i].Attributes["side"].Value.Equals(_side))
            {
                side = sides[i];
                break;
            }
        }
        if (side != null)
        {
            XmlNodeList skills = side.SelectNodes("skill");
            if (skills.Count < _skillTexts.Length)
            {
                for (int i = 0; i < skills.Count; i++)
                {
                    _skillTexts[i] = skills[i].InnerText;
                }
                for (int i = skills.Count; i < _skillTexts.Length; i++)
                {
                    _skillTexts[i] = skills[skills.Count - 1].InnerText;
                }
            }
            else
            {
                for (int i = 0; i < _skillTexts.Length; i++)
                {
                    _skillTexts[i] = skills[i].InnerText;
                }
            }


        }
    }
    public static void LoadDialog(TextAsset _xmlFile,ref List<DialogBranch> _dialog)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(_xmlFile.text);
        XmlNodeList branches = xmlDoc.DocumentElement.SelectNodes("branch");
        _dialog = new List<DialogBranch>();
        for (int i = 0; i < branches.Count; i++)
        {
            XmlNodeList nodes = branches[i].SelectNodes("opponent");
            DialogBranch branch = new DialogBranch();
            branch.round = int.Parse(branches[i].Attributes["round"].Value);
            _dialog.Add(branch);      
            _dialog[i].nodes = new List<DialogNode>();
            for (int j  = 0; j < nodes.Count; j++)
            {
                DialogNode node = new DialogNode();
                node.text = nodes[j].InnerText;
                if (nodes[j].Attributes["delay"] != null)
                    node.delay = Convert.ToSingle(nodes[j].Attributes["delay"].Value, System.Globalization.CultureInfo.InvariantCulture);
                else
                    node.delay = 1.0f;
                if (nodes[j].Attributes["opponent"].Value.Equals("left"))
                    node.side = 0;
                if (nodes[j].Attributes["opponent"].Value.Equals("right"))
                    node.side = 1;
                _dialog[i].nodes.Add(node);
            }

            XmlNode xmlN = branches[i].SelectSingleNode("stop");
            if(xmlDoc != null)
            {
                DialogNode node = new DialogNode();
                node.text = xmlN.InnerText;
                if (xmlN.Attributes["delay"] != null)
                    node.delay = Convert.ToSingle(xmlN.Attributes["delay"].Value, System.Globalization.CultureInfo.InvariantCulture);
                else
                    node.delay = 1.0f;
                if (xmlN.Attributes["opponent"].Value.Equals("stop"))
                    node.side = 2;
                _dialog[i].stopNode = node;
            }
        }
    }
    public static void LoadDialog(TextAsset _xmlFile,ref List<XmlNodeList> _dialog)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(_xmlFile.text);
        
        XmlNodeList branches = xmlDoc.DocumentElement.SelectNodes("branch");
        _dialog = new List<XmlNodeList>(branches.Count);
        for (int i = 0; i < branches.Count; i++)
        {
            _dialog.Add(branches[i].SelectNodes("opponent"));
        }
    }
    public static int GetDialogNodeInfo(XmlNode _node, ref string _text, ref float delay)
    {
        _text = _node.InnerText;
        if (_node.Attributes["delay"] != null)
            delay = Convert.ToSingle(_node.Attributes["delay"].Value, System.Globalization.CultureInfo.InvariantCulture);
           //delay = float.Parse(,System.Globalization.NumberStyles.Float);   
        return _node.Attributes["opponent"].Value.Equals("left") ? 0 : 1;
    }

    public static string[] LoadNewspaper(TextAsset _xmlFile, int _info)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(_xmlFile.text);
        XmlNodeList texts = xmlDoc.DocumentElement.SelectNodes("text");
        string[] strs = new string[texts.Count + 1];
        for (int i = 0; i < texts.Count; i++)
        {
            strs[i + 1] = texts[i].InnerText;
        }
        strs[0] = xmlDoc.DocumentElement.SelectNodes("title")[_info].InnerText;
        return strs;
    }
}