using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LinkText : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI linkText;

    // Start is called before the first frame update
    void Start()
    {
    string linkColor = "<color=#00FF00>"; // Beispielfarbe: Grün
    string endColor = "</color>";

    linkText.text = 
        linkColor + "<link=https://www.flaticon.com/de/kostenlose-icons/muskel>Muskel Icons erstellt von Vitaly Gorbachev - Flaticon</link><br>" + endColor +
        linkColor + "<link=https://www.flaticon.com/de/kostenlose-icons/donner>Donner Icons erstellt von Smashicons - Flaticon</link><br>" + endColor;
    }

    public void OnPointerClick(PointerEventData eventData) {
        if(eventData.button == PointerEventData.InputButton.Left) {
            int index = TMP_TextUtilities.FindIntersectingLink(linkText, Input.mousePosition, null);
            if(index >-1) {
                Application.OpenURL(linkText.textInfo.linkInfo[index].GetLinkID());
            }
        }
    }

}
