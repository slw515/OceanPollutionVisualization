using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListButton : MonoBehaviour
{

    [SerializeField]
    private Text myText;
    private string myTextString;

    [SerializeField]
    private ButtonListControl buttonControl;

    public void SetText(string textString) 
    {
        myTextString = textString;
        myText.text = textString;
    }

    public void OnClick() 
    {
        Debug.Log(myText.text);
    }
}
