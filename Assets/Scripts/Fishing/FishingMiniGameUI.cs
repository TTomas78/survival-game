using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingMiniGameUI : MonoBehaviour
{
    [SerializeField] Transform parentUI;

    public void ShowGameUI() {
        parentUI.gameObject.SetActive(true);
    }

    public void HideGameUI() {
        parentUI.gameObject.SetActive(false);
    }

}
