using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextConverter : MonoBehaviour
{
    [SerializeField]private TMP_InputField _inputField;
    [SerializeField]private SimpleHelvetica SHText;
   
    public void UpdateText()
    {
        if (_inputField && SHText)
        {
            SHText.Text = _inputField.text;
            SHText.GenerateText();
        }
    }
}
