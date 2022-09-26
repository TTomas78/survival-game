using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Resource")]
public class ResourceData : ScriptableObject
{
    [SerializeField] int maxHealth;
    [SerializeField] int rewardAmount;
    [SerializeField] GameObject rewardPrefab;
    [SerializeField] float spawnRewardRadius;
    [SerializeField] Enums.PossibleActions requiredAction;



    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int RewardAmount { get => rewardAmount; set => rewardAmount = value; }
    public GameObject RewardPrefab { get => rewardPrefab; set => rewardPrefab = value; }
    public float SpawnRewardRadius { get => spawnRewardRadius; set => spawnRewardRadius = value; }

    public Enums.PossibleActions RequiredAction { get => requiredAction;}
}
