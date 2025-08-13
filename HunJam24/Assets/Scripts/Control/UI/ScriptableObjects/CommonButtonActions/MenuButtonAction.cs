using UnityEngine;
using Model;
[CreateAssetMenu(fileName = "MenuButtonAction", menuName = "CommonButtonActions/Menu")]
public class MenuButtonAction : CommonButtonAction
{
    public override void Execute()
    {
        GameManager.Instance.MainMenu();
    }
}