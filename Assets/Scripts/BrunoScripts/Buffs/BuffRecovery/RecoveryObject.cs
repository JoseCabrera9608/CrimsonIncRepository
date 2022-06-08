using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class RecoveryObject : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject fillBar;
    [SerializeField] private GameObject player;

    [SerializeField] private bool playerOnRange;
    [SerializeField] private float currentRecoverTime;


    // Update is called once per frame
    private void Start()
    {
        player = FindObjectOfType<PlayerStatus>().gameObject;

        cube.transform.DORotate(new Vector3(360, 360, 50), 10, RotateMode.WorldAxisAdd).SetEase(Ease.OutBack).SetLoops(-1, LoopType.Yoyo);
        cube.transform.DOMove(cube.transform.position+Vector3.up*0.2f, 10, false).SetEase(Ease.InOutBack).SetLoops(-1, LoopType.Yoyo);
    }
    void Update()
    {
        LookToPlayer();
        RecoverTime();
    }
    private void LookToPlayer()
    {
        Vector3 playerHead = player.transform.position + Vector3.up;
        canvas.transform.LookAt(playerHead);
    }

    private void RecoverTime()
    {
        if (playerOnRange) currentRecoverTime += Time.deltaTime;

        fillBar.GetComponent<Image>().fillAmount = currentRecoverTime / PlayerSingleton.Instance.playerRecoveryTime;

        if (currentRecoverTime >= PlayerSingleton.Instance.playerRecoveryTime)
        {
            BuffManager.Instance.RecoverBuffs();
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) playerOnRange = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerOnRange = false;
    }
}
