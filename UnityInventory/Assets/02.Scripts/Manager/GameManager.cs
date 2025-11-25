using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    [SerializeField] private GameObject playerPrefab;
    public Character player { get; private set; }

    [Header("UI")]
    [SerializeField] private UIMainMenu uiMainMenu;
    [SerializeField] private UIStatus uiStatus;
    [SerializeField] private UIInventory uiInventory;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetGameData();
    }

    public void SetGameData()
    {
        if (playerPrefab != null)
        {
            GameObject playerObject = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            
            player = playerObject.GetComponent<Character>();

            if(player == null)
            {
                Debug.LogError("Player에 Character 컴포넌트가 없음");
                return;
            }

            player.SetData("정리코");

            UpdateAllUI();
        }
        else
        {
            Debug.LogError("Player Prefab이 GameManager에 연결되지 않았음");
        }
    }

    public void UpdateAllUI()
    {
        if (uiMainMenu == null)
        {
            Debug.LogError("UIMainMenu 없음");
        }
        if (uiStatus == null)
        {
            Debug.LogError("UIStatus 없음");
        }
        if(uiInventory == null)
        {
            Debug.LogError("UIInventory 없음");
        }
        uiMainMenu.SetCharacterInfo(player);
        uiStatus.SetCharacterStats(player);
        uiInventory.UpdateInventoryUI(player.Inventory);
    }
}
