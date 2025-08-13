using UnityEngine;
using Model;
[CreateAssetMenu(fileName = "StartButtonAction", menuName = "CommonButtonActions/Start")]
public class StartButtonAction : CommonButtonAction
{
    public override void Execute()
    {
        GameManager.Instance.LevelSetSelect();
    }
}