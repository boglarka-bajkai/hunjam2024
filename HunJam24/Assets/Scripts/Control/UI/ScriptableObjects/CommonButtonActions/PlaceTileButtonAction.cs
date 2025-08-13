using UnityEngine;
using Model;
using Model.LevelEditor;
[CreateAssetMenu(fileName = "PlaceTileButtonAction", menuName = "CommonButtonActions/PlaceTile")]
public class PlaceTileButtonAction : CommonButtonAction
{
    public override void Execute()
    {

        LevelEditor.Instance.Place();
    }
}