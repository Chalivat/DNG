using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoriqueSlot : MonoBehaviour
{
    public PlayerCoup coup;

    public Text damage_txt;
    public Image Background;

    public GameObject description;

    LigneHighlight_Script ligneHighlight_Script;

    private void Start()
    {
        ligneHighlight_Script = GameObject.FindObjectOfType<LigneHighlight_Script>();
    }

    public void UpdateSlot()
    {
        Background.color = coup.cardPlaced.contourColor;
        damage_txt.text = coup.cardPlaced.damage.ToString();
    }

    public void PointerEnter()
    {
        ligneHighlight_Script.HighLightCase((int)coup.caseFill.pos.x, (int)coup.caseFill.pos.y, coup.caseFill.boardNumber, ligneHighlight_Script.highlight_Color);

        if (coup.cardPlaced.asDescription)
        {
            description.GetComponentInChildren<Text>().text = coup.cardPlaced.description;
            description.GetComponent<RectTransform>().localPosition = new Vector3(105f, 0f, 0f);
        }
    }

    public void PointerExit()
    {
        ligneHighlight_Script.HighLightCase((int)coup.caseFill.pos.x, (int)coup.caseFill.pos.y, coup.caseFill.boardNumber, ligneHighlight_Script.highlight_Color);
        ligneHighlight_Script.ClearAllCase();
        description.GetComponent<RectTransform>().localPosition = new Vector3(-60f, 0f, 0f);
    }
}
