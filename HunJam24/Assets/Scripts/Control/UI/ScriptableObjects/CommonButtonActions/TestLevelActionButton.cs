using UnityEngine;
using Model;
[CreateAssetMenu(fileName = "TestLevelButtonAction", menuName = "CommonButtonActions/TestLevel")]
public class TestLevelActionButton : CommonButtonAction
{
    public override void Execute()
    {
        GameManager.Instance.TestLevel();
    }
}