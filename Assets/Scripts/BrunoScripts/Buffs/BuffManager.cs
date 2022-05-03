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
    [SerializeField] private static bool buffOnGround;
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) HandleDeath();
        if (Input.GetKeyDown(KeyCode.O)) RecoverBuffs();
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

    public void HandleDeath()
    {
        if (equipedBuffs.Count > 0)
        {
            if (buffOnGround==false)
            {
                Debug.Log("Tus buffs estan en el lugar de tu muerte UwU");
                foreach (Buff buff in equipedBuffs)
                {
                    buff.oppositeMultiplier =-1;
                    buff.ApplyBuff();
                    buff.oppositeMultiplier = 1;
                }
                buffOnGround = true;
                SpawnBuffRecoveryObject();
            }else if (buffOnGround == true)
            {
                Debug.Log("Oh no, perdiste tus buffs para siempre :(");
                foreach (Buff buff in equipedBuffs)
                {
                    buff.picked = false;
                    equipedBuffs = null;
                }
                buffOnGround = false;
            }
            
        }
    }
    public void SpawnBuffRecoveryObject()
    {
        //AQUI DEBERIA IR LA LOGICA PARA QUE EL OBJETO APAREZCA EN EL LUGAR DE LA MUERTE
    }
    public void RecoverBuffs()
    {
        Debug.Log("Recuperaste tus buffs :)");
        foreach (Buff buff in equipedBuffs)
        {
            buff.ApplyBuff();
        }
        buffOnGround = false;
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
                if (_buff.picked)
                {
                    equipedBuffs.Add(_buff);
                    _buff.ApplyBuff();
                }

            }
        }
        else
        {
            equipedBuffs.Add(buffToLoad);
        }
        
    }
}
