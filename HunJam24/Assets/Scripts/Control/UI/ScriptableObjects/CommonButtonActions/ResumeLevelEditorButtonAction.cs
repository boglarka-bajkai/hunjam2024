using UnityEngine;
using Model;
[CreateAssetMenu(fileName = "ResumeLevelEditorButtonAction", menuName = "CommonButtonActions/ResumeLevelEditor")]
public class ResumeLevelEditorButtonAction : CommonButtonAction
{
    public override void Execute()
    {
        GameManager.Instance.EditLevel(false);
    }
}