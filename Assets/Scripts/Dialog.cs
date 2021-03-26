using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Dialog
{
    public string Name;
    public string Text;

    public Dialog()
    {

    }

    public Dialog(string name, string text)
    {
        this.Name = name;
        this.Text = text;
    }

    public Dialog(string text)
    {
        this.Text = text;
    }
}
