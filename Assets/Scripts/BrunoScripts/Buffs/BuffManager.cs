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

    private void Start()
    {
        SetBuffInformation();
        ShowPanel();
        buffPanel.GetComponent<CanvasGroup>().alpha = 0;
    }
    public void ShowPanel()
    {
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
            _buff.used = false;
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
                if (buff[randomIndex].used == false)
                {
                    i = maxTries;
                    buff[randomIndex].used = true;
                    container.GetComponent<BuffContainer>().assignedBuff=buff[randomIndex];
                    container.GetComponent<BuffContainer>().ChangeText();
                }
            }

        }
    }
}
