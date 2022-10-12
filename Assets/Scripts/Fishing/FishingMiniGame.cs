using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FishingMiniGame : MonoBehaviour
{
    [Header("Fishing Area")]
    [SerializeField] Transform topBounds;
    [SerializeField] Transform bottomBounds;
    [SerializeField] RodLine rodLine;
    
    [Header("Fish Settings")]
    [SerializeField] Transform fish;
    [SerializeField] float fishSize = 0.2f;
    [SerializeField] float smoothMotion = 3f; // How fast the fish moves
    [SerializeField] float fishTimeRandomizer = 3f; // How much the fish moves around
    float fishPosition;
    float fishSpeed;
    float fishTimer;
    float fishTargetPosition;

    [Header("Hook Settings")]
    [SerializeField] Transform hook;
    [SerializeField] float hookSize = .2f; // How big the hook is
    [SerializeField] float hookSpeed = .1f; // How fast the hook moves
    [SerializeField] float hookGravity = .05f; // How fast the hook falls
    float hookPosition;
    float hookPullVelovity;

    [Header("Progress Bar settings")]
    [SerializeField] Slider progressBar;
    [SerializeField] float hookPower = 1f; // How fast the hook moves
    [SerializeField] float pregressBarDecay = .1f; // How fast the progress bar decays
    float catchPregress;

    [Header("Fishing area settings")]
    [SerializeField] Transform fishingArea; 
    [SerializeField] Transform playerPosition; 

    [Header("UI")]
    [SerializeField] FishingMiniGameUI fishingMiniGameUI;

    [Header("Rewards")]
    [SerializeField] Item[] fishRewards;
    [SerializeField] Item[] junkRewards;
    [SerializeField] float junkChance = 0.5f;


    bool isFishing = false;

    // Start is called before the first frame update
    void Start()
    {
        fishPosition = Random.Range(bottomBounds.position.y, topBounds.position.y);
        fishTargetPosition = Random.Range(bottomBounds.position.y, topBounds.position.y);
        fishTimer = Random.Range(0f, fishTimeRandomizer);
        fishSpeed = Random.Range(0f, 1f);
        hookPosition = bottomBounds.position.y;
        catchPregress = 0f;
    }

    private void MoveFish() {
        fishTimer += Time.deltaTime;
        if (fishTimer >= fishTimeRandomizer) {
            // Reset the timer
            fishTimer = 0f;
            // Randomize the target position
            fishTargetPosition = Random.Range(
                bottomBounds.position.y + fishSize / 2,
                topBounds.position.y - fishSize / 2
            );
        }
        // Move the fish towards the target position
        fishPosition = Mathf.Lerp(fishPosition, fishTargetPosition, smoothMotion * Time.deltaTime);
        // Set the fish position
        fish.position = new Vector3(fish.position.x, fishPosition, fish.position.z);
    }
    
    private void MoveHook() {
        if(Input.GetMouseButton(0)) {
            // increase our pull velocity
            hookPullVelovity += hookSpeed * Time.deltaTime;
        }
        // decrease our pull velocity
        hookPullVelovity -= hookGravity * Time.deltaTime;
        
        // Move the hook
        hookPosition += hookPullVelovity;
        if(hookPosition - hookSize / 2 <= 0 && hookPullVelovity < 0) {
            // We hit the bottom
            hookPullVelovity = 0;
        }
        if(hookPosition + hookSize / 2 >= 1 && hookPullVelovity > 0) {
            // We hit the top
            hookPullVelovity = 0;
        }

        // Clamp the hook position
        hookPosition = Mathf.Clamp(hookPosition, hookSize / 2, 1 - hookSize / 2);
        // Set the hook's position
        hook.position = Vector3.Lerp(bottomBounds.position, topBounds.position, hookPosition);
    }

    private void CheckProgress() {


        // Clamp the progress
        catchPregress = Mathf.Clamp(catchPregress, 0, 1);
        // Set the progress bar's value
        progressBar.value = catchPregress;

        if(catchPregress >= 1 && isFishing) {
            // We caught the fish
            Debug.Log("Caught the fish!");
            CatchFish();
        }



        float _distance = Mathf.Abs(fish.position.y - hook.position.y );
        float _sumEdges = fishSize + hookSize;
        if(_distance <= _sumEdges) {
            // We are close enough to catch the fish
            catchPregress += hookPower * Time.deltaTime;
        } else {
            // We are not close enough to catch the fish
            catchPregress -= pregressBarDecay * Time.deltaTime;
        }
        
    }
    

    private void FixedUpdate()
    {
        MoveFish();
        MoveHook();
        CheckProgress();
    }

    public void StartGame() {
        isFishing = true;
         // set ui position close to the player
        Vector3 playerOffset = new Vector3(-1f, 1.5f, 0);
        fishingMiniGameUI.transform.position = playerPosition.position + playerOffset;

        // Reset the progress bar
        catchPregress = 0f;
        progressBar.value = catchPregress;
        
        // start drawing the rod line
        rodLine.StartDrawLine();

        // enable UI
        fishingMiniGameUI.ShowGameUI();

    }

    public void EndGame() {
        isFishing = false;
        // stop drawing the rod line
        rodLine.StopDrawLine();

        // disable UI
        fishingMiniGameUI.HideGameUI();
    }

    public IEnumerator StartGameCoroutine() {
        yield return new WaitForSeconds(1f);
        StartGame();
    }

    public void CatchFish() {
        // random change to get junk
        if(Random.value <= junkChance) {
            CatchJunk();
            return; 
        }
        // get a random fish
        Item _fish = fishRewards[Random.Range(0, fishRewards.Length)];
        // add the fish to the inventory
        InventoryManager.instance.Add(_fish);
        // end the game
        EndGame();
    }

    public void CatchJunk() {
        // get a random junk
        Item _junk = junkRewards[Random.Range(0, junkRewards.Length)];
        // add the junk to the inventory
        InventoryManager.instance.Add(_junk);
        // end the game
        EndGame();
    }

}
