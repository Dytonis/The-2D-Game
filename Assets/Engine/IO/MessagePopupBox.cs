//   2D Game
// MessagePopupBox.cs
//   Tanner Wright, 2015
using System;
using UnityEngine;

public class MessagePopupBox
{
    public static void PopMessageBox(GameObject Canvas, GameObject prefab, GameObject tracker, float time, MessagePopupBoxInfo_TitleRightLeft info)
    {
        //TODO
    }
}

public class MessagePopupBoxInfo_TitleRightLeft
{
    private string TITLE = "";
    private string LEFTBOX = "";
    private string RIGHTBOX = "";

    public MessagePopupBoxInfo_TitleRightLeft(string title, string leftbox, string rightbox)
    {
        TITLE = title;
        LEFTBOX = leftbox;
        RIGHTBOX = rightbox;
    }

    public string getTitle()
    {
        return TITLE;
    }

    public string getLeftBox()
    {
        return LEFTBOX;
    }

    public string getRightBox()
    {
        return RIGHTBOX;
    }
}



