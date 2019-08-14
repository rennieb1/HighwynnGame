using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Highwynn 
{

    public class TextDrawer : MonoBehaviour
    {
        public string outputMessage;
        public float typeDelay = 0.125f;
        public Color highlightColour;
        public TMP_Text outputBox;
        private bool isTyping = false;
        private Coroutine typeRoutine;

        void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.tag == "Player" && !isTyping) {
                typeRoutine = StartCoroutine(TypeText());
                isTyping = true;
            }
        }

        IEnumerator TypeText() {
            outputBox.text = "";
            foreach (char c in outputMessage) {
                if (c == '[') {
                    outputBox.text += "<#" + ColorUtility.ToHtmlStringRGB(highlightColour) + ">";
                }
                else if (c == ']') {
                    outputBox.text += "</color>";
                }
                else {
                    outputBox.text += c;
                }
                yield return new WaitForSeconds(typeDelay);
            }
            isTyping = false;
        }

        void OnTriggerExit2D(Collider2D other) {
            if (other.gameObject.tag == "Player") {
                StopCoroutine(typeRoutine);
                outputBox.text = "";
                isTyping = false;
            }
        }
    }

}

