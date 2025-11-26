using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private Image expBar;
    [SerializeField] private TextMeshProUGUI description;


    // player의 각 정보를 텍스트에 기입
    public void SetCharacterInfo(Character player)
    {
        if(player != null)
        {
            nameText.text = player.Name;
            levelText.text = $"{player.Level}";
            goldText.text = $"{player.Gold}";
            expText.text = $"{player.Exp} / {player.MaxExp}";
            expBar.fillAmount = (float)player.Exp / player.MaxExp;
        }
    }
}
