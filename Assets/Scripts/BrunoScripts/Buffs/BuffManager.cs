using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BuffManager : MonoBehaviour
{
    public float containersToDisplay;
    public Color[] colors;
    [SerializeField] private GameObject buffPanel;
    [SerializeField] private GameObject[] buffContainer;
    [SerializeField] private GameObject containerParent;
    [SerializeField] private GameObject containerPrefab;
    [SerializeField] private Buff[] buff;
    [SerializeField] private List<Buff> equipedBuffs;

    public static BuffManager Instance;
    [SerializeField] private static bool buffOnGround;
    
    //Unique Buffs Vars====================================
    public bool hasExtraCardBuff=false;

    public bool amplificacionRiesgosa=false;
    public float prevDamage;

    public bool inhibidorDeNanobots = false;

    //ACTIONS=============================================
    public static Action onBossDefetead;

    //Recover buffs
    [SerializeField]private GeneralDataHolder dataHolder;
    private void OnEnable()
    {
        PlayerStatus.onPlayerDeath += HandleDeath;
        BuffManager.onBossDefetead += ShowPanel;
    }
    private void OnDisable()
    {
        PlayerStatus.onPlayerDeath -= HandleDeath;
        BuffManager.onBossDefetead -= ShowPanel;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) onBossDefetead?.Invoke();

        if (Input.GetKeyDown(KeyCode.O))
        {
            PlayerSingleton.Instance.playerCurrentHP -= 10;
            Debug.LogWarning("-10 de salud por parte de " + gameObject.name);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            PlayerSingleton.Instance.playerCurrentHP += 30;
            Debug.LogWarning("+30 de salud por parte de " + gameObject.name);
        }

        HandleAmplificacionRiesgosa();
        HandleInhibidorDeNanoBots();
    }
    private void Awake()
    {
        Instance = this;       
    }
    private void Start()
    {
        amplificacionRiesgosa = false;
        hasExtraCardBuff = false;
        inhibidorDeNanobots = false;

        LoadSelectedBuffs(false, null);
        buffPanel.SetActive(false);

        prevDamage = PlayerSingleton.Instance.playerDamage;
    }
  
    public void SpawnContainers()
    {
        buffContainer = new GameObject[(int)containersToDisplay];
        for(int i = 0; i < buffContainer.Length; i++)
        {
            GameObject spawn = Instantiate(containerPrefab);
            spawn.transform.parent = containerParent.transform;
            buffContainer[i] = spawn;
        }
    }
    public void ShowPanel()
    {
        if (containerParent == null) Debug.LogError("Falta asignar el objeto padre de los contenedores");

        UIManager.Instance.CursorController("unblock");
        SpawnContainers();
        ResetBuffDisplay();
        SetBuffInformation();

        //Le agregue esto porque el mouse no salia owo - Santiago
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        buffPanel.SetActive(true);
        buffPanel.GetComponent<CanvasGroup>().alpha = 0;
        buffPanel.GetComponent<CanvasGroup>().interactable = true;
        Sequence showPanel = DOTween.Sequence();
        showPanel.Append(buffPanel.GetComponent<CanvasGroup>().DOFade(1, 3));
        showPanel.AppendInterval(-1.5f);

        foreach(GameObject container in buffContainer)
        {
            showPanel.AppendInterval(-0.5f);
            showPanel.Append(container.GetComponent<RectTransform>().DORotate(Vector3.zero, 1, RotateMode.Fast));
            showPanel.Join(container.GetComponent<RectTransform>().DOScale(1, 1).SetEase(Ease.OutBack));           
        }
    }    
    public void HideCards(GameObject selectedCard)
    {
        buffPanel.GetComponent<CanvasGroup>().interactable = false;
        UIManager.Instance.CursorController("block");
        Sequence hidePanel = DOTween.Sequence();
        foreach(GameObject container in buffContainer)
        {
            if (container != selectedCard)
            {
                hidePanel.Join(container.GetComponent<RectTransform>().DORotate(new Vector3(0, 90, 0), 1, RotateMode.Fast));
                hidePanel.Join(container.GetComponent<RectTransform>().DOScale(0, 1).SetEase(Ease.InBack));
            }
        }

        //Le agregue esto para desactivar el mouse owo - Santiago
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        hidePanel.Append(buffPanel.GetComponent<CanvasGroup>().DOFade(0, 1));
        hidePanel.OnComplete(HidePanel);
    }
    public void HidePanel()
    {
        buffPanel.SetActive(false);
        foreach(GameObject container in buffContainer)
        {
            Destroy(container);
        }
    }
    public void SetBuffInformation()
    {
        foreach(GameObject container in buffContainer)
        {
            int maxTries = 500;

            for(int i = 0; i < maxTries; i++)
            {
                int randomIndex = (int)UnityEngine.Random.Range(0, buff.Length);
                if (buff[randomIndex].displayed == false)
                {
                    i = maxTries;
                    buff[randomIndex].displayed = true;
                    container.GetComponent<BuffContainer>().assignedBuff=buff[randomIndex];
                    container.GetComponent<BuffContainer>().ChangeText(GetColor(buff[randomIndex].rarity));
                }
            }          
        }
    }
    public Color GetColor(BuffRarity rarity)
    {
        if (rarity == BuffRarity.common) return colors[0];
        else if (rarity == BuffRarity.rare) return colors[1];
        else if (rarity == BuffRarity.unique) return colors[2];
        else return colors[0];
    }
    public void HandleDeath()
    {
        if (equipedBuffs.Count > 0)
        {
            if (buffOnGround == false)
            {
                Debug.Log("Tus buffs estan en el lugar de tu muerte UwU");
                foreach (Buff buff in equipedBuffs)
                {
                    buff.active = false;
                }
                buffOnGround = true;
                dataHolder.spawnDataRecoveryObject=true; 
            }
        }
    }
    public void RecoverBuffs()
    {
        Debug.Log("Recuperaste tus buffs :)");
        foreach (Buff buff in equipedBuffs)
        {
            buff.active = true;
            buff.ApplyBuff();
        }
        buffOnGround = false;
        dataHolder.spawnDataRecoveryObject = false;
    }
    public void ResetBuffDisplay()
    {
        //Debug.Log("Reseting display");
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
                    if(_buff.active) _buff.ApplyBuff();
                }

            }
        }
        else
        {
            equipedBuffs.Add(buffToLoad);
        }
        
    }
    
    public void UniqueBuffs(UniqueID id)
    {
        switch (id)
        {
            case UniqueID.extraCard:
                hasExtraCardBuff = true;
                if (hasExtraCardBuff) containersToDisplay = 4;
                else containersToDisplay = 3;
                break;

            case UniqueID.amplificacionRiesgosa:
                amplificacionRiesgosa = true;
                
              break;

            case UniqueID.inhibidorDeNanobots:
                inhibidorDeNanobots = true;
                break;
        }
    }
    private void HandleAmplificacionRiesgosa()
    {
        if (amplificacionRiesgosa == false) return;

        if (PlayerSingleton.Instance.playerCurrentHP < PlayerSingleton.Instance.playerMaxHP * .2f)
        {
            PlayerSingleton.Instance.playerDamage = prevDamage + (DefaultPlayerVars.defaultDamage * 1.6f);
        }
        else
        {
            PlayerSingleton.Instance.playerDamage = prevDamage;
        }
    }
    private void HandleInhibidorDeNanoBots()
    {
        if (inhibidorDeNanobots == false) return;

        if (PlayerSingleton.Instance.playerCurrentHP < PlayerSingleton.Instance.playerMaxHP)
        {
            PlayerSingleton.Instance.playerCurrentHP += .2f * Time.deltaTime;
        }

    }
    [ContextMenu("ResetAll uses")]
    public void ResetBuffUses()
    {
        foreach (Buff _buff in buff)
        {
            _buff.picked = false;
            _buff.active = true;
            _buff.displayed = false;
        }
    }
}
