using UnityEngine;
using Model;
[CreateAssetMenu(fileName = "StartButtonAction", menuName = "CommonButtonActions")]
public class RestartButtonAction : CommonButtonAction
{
    public override void Execute()
    {
        GameManager.Instance.RestartGame();
    }
}