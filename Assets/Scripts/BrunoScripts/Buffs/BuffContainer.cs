using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuffContainer : MonoBehaviour
{
    [SerializeField] private Text titleText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Image iconSprite;

    public Buff assignedBuff;

    public void ChangeText(Color _color)
    {
        titleText.text = assignedBuff.buffName;
        descriptionText.text = assignedBuff.description;
        iconSprite.sprite = assignedBuff.icon;
        GetComponent<Image>().color = _color;
    }
    public void PickBuff()
    {
        assignedBuff.picked = true;
        BuffManager.Instance.ResetBuffDisplay();
        BuffManager.Instance.LoadSelectedBuffs(true, assignedBuff);
        assignedBuff.ApplyBuff();
        BuffManager.Instance.HideCards(this.gameObject);
    }  
}
