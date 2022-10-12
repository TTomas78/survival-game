using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingArea : MonoBehaviour
{
    [SerializeField] FishingMiniGame fishingMiniGame;

    void OnMouseDown()
    {
        // start drawing line
        StartCoroutine(fishingMiniGame.StartGameCoroutine());
    }
}
