
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
    public static void LoadDialog(TextAsset _xmlFile,ref DialogRoot _dialog)
    {
        _dialog = new DialogRoot();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(_xmlFile.text);
        XmlNodeList sides = xmlDoc.DocumentElement.SelectNodes("side");
        _dialog.sides = new List<DialogSkills>();

        for (int i = 0; i < sides.Count; i++)
        {           
            DialogSkills skills = new DialogSkills();
            skills.skills = new List<DialogSkill>();
            _dialog.sides.Add(skills);




            
            XmlNodeList skillList = sides[i].SelectNodes("skill");
            for (int j  = 0; j < skillList.Count; j++)
            {
                DialogSkill skill = new DialogSkill();
                skill.dialogs = new List<DialogNode>();
                skill.actions = new List<DialogNode>();
                _dialog.sides[i].skills.Add(skill);
              XmlNodeList nodes = skillList[j].SelectNodes("dialog");
                for (int k = 0; k < nodes.Count; k++)
                {
                    DialogNode node = new DialogNode();
                    node.text = nodes[k].InnerText;
                    if (nodes[k].Attributes["delay"] != null)
                        node.delay = Convert.ToSingle(nodes[k].Attributes["delay"].Value, System.Globalization.CultureInfo.InvariantCulture);
                    else
                        node.delay = 1.0f;
                    if (nodes[k].Attributes["opponent"].Value.Equals("left"))
                        node.side = 0;
                    if (nodes[k].Attributes["opponent"].Value.Equals("right"))
                        node.side = 1;
                    if (nodes[k].Attributes["opponent"].Value.Equals("main"))
                        node.side = 2;
                    skill.dialogs.Add(node);
                }
                XmlNode action = skillList[j].SelectSingleNode("action");
                {
                    XmlNode nodeXml = action.SelectSingleNode("accept");
                    DialogNode node = new DialogNode();
                    node.text = nodeXml.InnerText;
                    if (nodeXml.Attributes["delay"] != null)
                        node.delay = Convert.ToSingle(nodeXml.Attributes["delay"].Value, System.Globalization.CultureInfo.InvariantCulture);
                    else
                        node.delay = 1.0f;
                    if (nodeXml.Attributes["opponent"].Value.Equals("left"))
                        node.side = 0;
                    if (nodeXml.Attributes["opponent"].Value.Equals("right"))
                        node.side = 1;
                    if (nodeXml.Attributes["opponent"].Value.Equals("main"))
                        node.side = 2;
                    skill.actions.Add(node);
                }
                {
                    XmlNode nodeXml = action.SelectSingleNode("refuse");
                    DialogNode node = new DialogNode();
                    node.text = nodeXml.InnerText;
                    if (nodeXml.Attributes["delay"] != null)
                        node.delay = Convert.ToSingle(nodeXml.Attributes["delay"].Value, System.Globalization.CultureInfo.InvariantCulture);
                    else
                        node.delay = 1.0f;
                    if (nodeXml.Attributes["opponent"].Value.Equals("left"))
                        node.side = 0;
                    if (nodeXml.Attributes["opponent"].Value.Equals("right"))
                        node.side = 1;
                    if (nodeXml.Attributes["opponent"].Value.Equals("main"))
                        node.side = 2;
                    skill.actions.Add(node);
                }
                {
                    XmlNode nodeXml = action.SelectSingleNode("refuse-succses");
                    DialogNode node = new DialogNode();
                    node.text = nodeXml.InnerText;
                    if (nodeXml.Attributes["delay"] != null)
                        node.delay = Convert.ToSingle(nodeXml.Attributes["delay"].Value, System.Globalization.CultureInfo.InvariantCulture);
                    else
                        node.delay = 1.0f;
                    if (nodeXml.Attributes["opponent"].Value.Equals("left"))
                        node.side = 0;
                    if (nodeXml.Attributes["opponent"].Value.Equals("right"))
                        node.side = 1;
                    if (nodeXml.Attributes["opponent"].Value.Equals("main"))
                        node.side = 2;
                    skill.actions.Add(node);
                }
                {
                    XmlNode nodeXml = action.SelectSingleNode("refuse-failed");
                    DialogNode node = new DialogNode();
                    node.text = nodeXml.InnerText;
                    if (nodeXml.Attributes["delay"] != null)
                        node.delay = Convert.ToSingle(nodeXml.Attributes["delay"].Value, System.Globalization.CultureInfo.InvariantCulture);
                    else
                        node.delay = 1.0f;
                    if (nodeXml.Attributes["opponent"].Value.Equals("left"))
                        node.side = 0;
                    if (nodeXml.Attributes["opponent"].Value.Equals("right"))
                        node.side = 1;
                    if (nodeXml.Attributes["opponent"].Value.Equals("main"))
                        node.side = 2;

                    skill.actions.Add(node);
                }
            }
        }
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

    public static void LoadLawbook(TextAsset _xmlFile,ref List<LawbookArticle> _lawbook)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(_xmlFile.text);
        XmlNodeList articles = xmlDoc.DocumentElement.SelectNodes("article");
        _lawbook = new List<LawbookArticle>();
        for (int i = 0; i < articles.Count; i++)
        {
            LawbookArticle article = new LawbookArticle();
            article.name = articles[i].SelectSingleNode("name").InnerText;
            article.title = articles[i].SelectSingleNode("long_name").InnerText;
            article.text = articles[i].SelectSingleNode("text").InnerText;
            _lawbook.Add(article);
        }
    }
}