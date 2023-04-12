using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using OpenAi;
using TMPro;
using UnityEditor;

public class TextReplaceCall : MonoBehaviour
{
    public OpenAiTextReplace openAiTextReplace;

    public void UpdateText()
    {
        openAiTextReplace.ReplaceText();
    }
}
