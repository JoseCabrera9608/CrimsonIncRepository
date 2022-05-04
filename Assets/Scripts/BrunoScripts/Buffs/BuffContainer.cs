using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuffContainer : MonoBehaviour
{
    [SerializeField] private Text titleText;
    [SerializeField] private Text descriptionText;

    public Buff assignedBuff;

    public void ChangeText()
    {
        titleText.text = assignedBuff.buffName;
        descriptionText.text = assignedBuff.description;       
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
