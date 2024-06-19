using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public static class TextUtils
{
    public static IEnumerator SpeechText(TMP_Text tmp, string text, float defaultDelay)
    {
        StringBuilder sb = new();
        foreach (var letter in text)
        {
            sb.Append(letter);
            tmp.SetText(sb);
            yield return new WaitForSeconds(defaultDelay);
        }
    }
}
