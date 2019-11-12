
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Highwynn
{

    public class ComicBookText : MonoBehaviour
    {
        [TextArea(10, 15)]
        public string outputMessage;
        public float typeDelay = 0.125f;
        public Color highlightColour;
        public TMP_Text outputBox;
        // private bool isTyping = false;
        private Coroutine typeRoutine;
        public float timer = 0;
        public float activator;

        void Start()
        {
            typeRoutine = StartCoroutine(TypeText());
            // isTyping = true;
        }
         

        IEnumerator TypeText()
        {
         //   yield return new WaitForSeconds(1.5f);
            outputBox.text = "";
            foreach (char c in outputMessage)
            {
                if (c == '[')
                {
                    outputBox.text += "<#" + ColorUtility.ToHtmlStringRGB(highlightColour) + ">";
                }
                else if (c == ']')
                {
                    outputBox.text += "</color>";
                }
                else
                {
                    outputBox.text += c;
                }
                yield return new WaitForSeconds(typeDelay);
            }
            // isTyping = false;
        }
     
    }

}

