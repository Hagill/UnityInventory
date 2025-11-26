using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    [SerializeField] private GameObject playerPrefab;
    public Character player { get; private set; }

    [Header("UI")]
    [SerializeField] private UIMainMenu uiMainMenu;
    [SerializeField] private UIStatus uiStatus;
    [SerializeField] private UIInventory uiInventory;

    [Header("경험치 추가 버튼")]
    [SerializeField] private Button expAddBtn;
    [SerializeField] private Button expManyAddBtn;
    [SerializeField] private Button levelUpBtn;

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

        expAddBtn.onClick.AddListener(OnAddExp);
        expManyAddBtn.onClick.AddListener(OnAddManyExp);
        levelUpBtn.onClick.AddListener(OnLevelUp);
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

            player.OnCharacterStatusChanged += HandleCharacterStatusChanged;

            player.SetData("정리코");
        }
        else
        {
            Debug.LogError("Player Prefab이 GameManager에 연결되지 않았음");
        }
    }

    private void HandleCharacterStatusChanged()
    {
        Debug.Log("player의 상태 UI 갱신");
        UpdateAllUI();
    }

    public void UpdateAllUI()
    {
        if(player == null)
        {
            Debug.LogError("Player 인스턴스 없음");
        }
        
        if (uiMainMenu != null)
        {
            uiMainMenu.SetCharacterInfo(player);
        }
        else { Debug.LogError("UIMainMenu 없음"); }

        if (uiStatus != null)
        {
            uiStatus.SetCharacterStats(player);
        }
        else { Debug.LogError("uiStatus 없음"); }

        if (uiInventory != null)
        {
            uiInventory.UpdateInventoryUI(player.Inventory);
        }
        else { Debug.LogError("uiInventory 없음"); }
    }

    public void OnAddExp()
    {
        if (player != null)
        {
            player.AddExp(1);
        }
    }

    public void OnAddManyExp()
    {
        if (player != null)
        {
            player.AddExp(10);
        }
    }

    public void OnLevelUp()
    {
        if (player != null)
        {
            player.LevelUp();
        }
    }

    private void OnDestroy()
    {
        if (player != null)
        {
            player.OnCharacterStatusChanged -= HandleCharacterStatusChanged;
        }
    }
}
