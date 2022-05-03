using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BuffManager : MonoBehaviour
{
    [SerializeField] private GameObject buffPanel;
    [SerializeField] private GameObject[] buffContainer;
    [SerializeField] private Buff[] buff;
    [SerializeField] private List<Buff> equipedBuffs;
    public static BuffManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        LoadSelectedBuffs(false, null);
        ResetBuffDisplay();
        SetBuffInformation();
        buffPanel.SetActive(false);
        ShowPanel();       
    }
    public void ShowPanel()
    {
        buffPanel.SetActive(true);
        buffPanel.GetComponent<CanvasGroup>().alpha = 0;
        Sequence showPanel = DOTween.Sequence();
        showPanel.Append(buffPanel.GetComponent<CanvasGroup>().DOFade(1, 3));
        showPanel.AppendInterval(-1.5f);

        showPanel.Append(buffContainer[0].GetComponent<RectTransform>().DORotate(Vector3.zero, 1, RotateMode.Fast));
        showPanel.Join(buffContainer[0].GetComponent<RectTransform>().DOScale(1, 1).SetEase(Ease.OutBack));
        showPanel.AppendInterval(-0.5f);

        showPanel.Append(buffContainer[1].GetComponent<RectTransform>().DORotate(Vector3.zero, 1, RotateMode.Fast));
        showPanel.Join(buffContainer[1].GetComponent<RectTransform>().DOScale(1, 1).SetEase(Ease.OutBack));
        showPanel.AppendInterval(-0.5f);

        showPanel.Append(buffContainer[2].GetComponent<RectTransform>().DORotate(Vector3.zero, 1, RotateMode.Fast));
        showPanel.Join(buffContainer[2].GetComponent<RectTransform>().DOScale(1, 1).SetEase(Ease.OutBack));

    }

    [ContextMenu("ResetAll uses")]
    public void ResetBuffUses()
    {
        foreach(Buff _buff in buff)
        {
            _buff.picked = false;
            _buff.displayed = false;
        }
    }
    public void SetBuffInformation()
    {
        foreach(GameObject container in buffContainer)
        {
            int maxTries = 500;

            for(int i = 0; i < maxTries; i++)
            {
                int randomIndex = (int)Random.Range(0, buff.Length);
                Debug.Log(randomIndex);
                if (buff[randomIndex].displayed == false)
                {
                    i = maxTries;
                    buff[randomIndex].displayed = true;
                    container.GetComponent<BuffContainer>().assignedBuff=buff[randomIndex];
                    container.GetComponent<BuffContainer>().ChangeText();
                }
            }          
        }
    }
    public void ResetBuffDisplay()
    {
        Debug.Log("Reseting display");
        foreach(Buff _buff in buff)
        {
            if(_buff.picked==false) _buff.displayed = false;
        }
    }
    
    public void LoadSelectedBuffs(bool specific,Buff buffToLoad)
    {
        if (specific == false)
        {
            foreach (Buff _buff in buff)
            {
                if (_buff.picked) equipedBuffs.Add(_buff);
            }
        }
        else
        {
            equipedBuffs.Add(buffToLoad);
        }
        
    }
    public void HandleInstantBuff(PlayerVars var,float multiplier)
    {
        switch (var)
        {
            case PlayerVars.maxHealth:
                PlayerSingleton.Instance.playerMaxHP *= multiplier;
                break;
            case PlayerVars.maxStamina:
                PlayerSingleton.Instance.playerMaxStamina*= multiplier;
                break;
            case PlayerVars.healingAmount:
                PlayerSingleton.Instance.playerHealAmount *= multiplier;
                break;
            case PlayerVars.damage:
                PlayerSingleton.Instance.playerDamage *= multiplier;
                break;
            case PlayerVars.defense:
                PlayerSingleton.Instance.playerDefense *= multiplier;
                break;
            case PlayerVars.runStaminaCost:
                PlayerSingleton.Instance.playerRunStaminaCost *= multiplier;
                break;
            case PlayerVars.staminaRegenValue:
                PlayerSingleton.Instance.playerStaminaRegen *= multiplier;
                break;
            case PlayerVars.statusResistance:
                PlayerSingleton.Instance.playerStatusResistance *= multiplier;
                break;
            case PlayerVars.healingCharges:
                PlayerSingleton.Instance.playerMaxHealingCharges *= multiplier;
                break;
        }
    }

}
