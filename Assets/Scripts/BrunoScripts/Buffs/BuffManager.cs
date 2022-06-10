using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BuffManager : MonoBehaviour
{
    public float containersToDisplay;
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
    public bool redTearstone=false;
    private float extraDamage;
    private float prevDamage;
    public bool sacrificeRing = false;
    //public float sacrificeRingCharges;

    //Recover buffs
    [SerializeField]private GeneralDataHolder dataHolder;
    private void OnEnable()
    {
        PlayerStatus.onPlayerDeath += HandleDeath;
    }
    private void OnDisable()
    {
        PlayerStatus.onPlayerDeath -= HandleDeath;
    }
    private void Awake()
    {
        Instance = this;       
    }
    private void Start()
    {
        LoadSelectedBuffs(false, null);
        buffPanel.SetActive(false);
        //temp
        //ShowPanel();       
    }
    private void Update()
    {
        //Testing
        //if (Input.GetKeyDown(KeyCode.P)) HandleDeath();
        //if (Input.GetKeyDown(KeyCode.O)) RecoverBuffs();
        //if (Input.GetKeyDown(KeyCode.I)) ShowPanel();

        //LowHealthDamageBuff();
        
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

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
        //CheckSacrificeRing();
        //if (sacrificeRing == true&&PlayerPrefs.GetFloat("sacrificeCharges")>0)
        //{
        //    PlayerPrefs.SetFloat("sacrificeCharges", PlayerPrefs.GetFloat("sacrificeCharges")-1);
        //    Debug.Log("Sacrifice charges= " + PlayerPrefs.GetFloat("sacrificeCharges"));
        //}
        //else
        //{
            if (equipedBuffs.Count > 0)
            {
                if (buffOnGround == false)
                {
                    Debug.Log("Tus buffs estan en el lugar de tu muerte UwU");
                    foreach (Buff buff in equipedBuffs)
                    {
                    //buff.oppositeMultiplier = -1;
                    //buff.ApplyBuff();
                    //buff.oppositeMultiplier = 1;
                    buff.active = false;
                    }
                    buffOnGround = true;
                    dataHolder.spawnDataRecoveryObject=true; 
                }
                //else if (buffOnGround == true)
                //{
                //    Debug.Log("Oh no, perdiste tus buffs para siempre :(");
                //    foreach (Buff buff in equipedBuffs)
                //    {
                //        buff.picked = false;
                //        equipedBuffs = null;
                //    }
                //    buffOnGround = false;
                //}

            }
        //}
        
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
    public void CheckSacrificeRing()
    {
        for(int i = 0; i < equipedBuffs.Count; i++)
        {
            if (equipedBuffs[i].uniqueID == UniqueID.sacrificeRing)
            {
                if(sacrificeRing&& PlayerPrefs.GetFloat("sacrificeCharges") == 0)
                {
                    equipedBuffs[i].picked = false;
                    sacrificeRing = false;
                    equipedBuffs.RemoveAt(i);
                }
            }
        }
       
    }
    public void UniqueBuffs(UniqueID id)
    {
        switch (id)
        {
            case UniqueID.extraCard:
                hasExtraCardBuff = !hasExtraCardBuff;
                if (hasExtraCardBuff) containersToDisplay = 4;
                else containersToDisplay = 3;
                break;

            case UniqueID.lowHealthMoreDamage:
                redTearstone = !redTearstone;
                prevDamage = PlayerSingleton.Instance.playerDamage;
              break;

            case UniqueID.sacrificeRing:
                sacrificeRing =true;
                if (PlayerPrefs.GetFloat("sacrificeCharges") == 0) PlayerPrefs.SetFloat("sacrificeCharges", 3);
                break;
        }
    }
    public void LowHealthDamageBuff()
    {
        if (redTearstone)
        {
            if (PlayerSingleton.Instance.playerCurrentHP <= PlayerSingleton.Instance.playerMaxHP * 0.2f)
            {
                extraDamage = DefaultPlayerVars.defaultDamage * 1.6f;
            }
            else extraDamage = 0;

        }
        else extraDamage = 0;
     
        PlayerSingleton.Instance.playerDamage = extraDamage+prevDamage;

        //if (Input.GetMouseButtonDown(0)) PlayerSingleton.Instance.playerCurrentHP -= 80f;
        //if (Input.GetMouseButtonDown(1)) PlayerSingleton.Instance.playerCurrentHP += 30f;

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
