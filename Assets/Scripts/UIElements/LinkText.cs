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
        linkColor + "<link=https://www.flaticon.com/de/kostenlose-icons/donner>Donner Icons erstellt von Smashicons - Flaticon</link><br>" + endColor + 
        linkColor + "<link=https://kenney.nl/assets/blaster-kit>Kenny Blaster Kit</link><br>" + endColor +
        linkColor + "<link=https://kenney.nl/assets/animated-characters-1>Kenny Animated Characters 1</link><br>" + endColor +
        linkColor + "<link=https://kenney.nl/assets/animated-characters-2>Kenny Animated Characters 2</link><br>" + endColor +
        linkColor + "<link=https://www.mixamo.com>Dying, Rifle Aim Idle, Rifle Idle, Rifle Run, Silly Dancing, Two Cycle Sprint, Zombie Dying, Zombie Idle, Zombie Punch, Zombie Run, Zombie Walk</link><br>" + endColor +
        linkColor + "<link=https://assetstore.unity.com/packages/vfx/particles/particle-pack-127325>Unity Partical Pack</link><br>" + endColor + 
        linkColor + "<link=https://assetstore.unity.com/packages/3d/props/ten-power-ups-217666>Unity Power Ups</link><br>" + endColor + 
        linkColor + "<link=https://pixabay.com/music/>In-Game music from Pixabay</link><br>" + endColor +
        linkColor + "<link=https://kenney.nl/assets/interface-sounds>UI and In-Game sounds</link><br>" + endColor + 
        linkColor + "<link=https://freesound.org/people/pgi/sounds/212607/>Machine Gun 002 - single shot</link><br>" + endColor +
        linkColor + "<link=https://freesound.org/people/PNMCarrieRailfan/sounds/683311/>Pistol Gun Shot Mix</link><br>" + endColor +
        linkColor + "<link=https://freesound.org/people/Carlfnf/sounds/677839/>Sniper Shoot</link><br>" + endColor +
        linkColor + "<link=https://freesound.org/people/unfa/sounds/609587/>Grenade Explosion SFX (medium-sized, meaty, realistic)</link><br>" + endColor;
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
