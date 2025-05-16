using UnityEngine;
using Model;
[CreateAssetMenu(fileName = "RestartButtonAction", menuName = "CommonButtonActions/Restart")]
public class RestartButtonAction : CommonButtonAction
{
    public override void Execute()
    {
        GameManager.Instance.RestartGame();
    }
}