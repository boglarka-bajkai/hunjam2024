using UnityEngine;
using Model;
[CreateAssetMenu(fileName = "LevelEditorButtonAction", menuName = "CommonButtonActions/LevelEditor")]
public class LevelEditorButtonAction : CommonButtonAction
{
    public override void Execute()
    {
        GameManager.Instance.EditLevel();
    }
}