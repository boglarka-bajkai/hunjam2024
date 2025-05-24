using Gilzoide.FlexUi;
using Gilzoide.FlexUi.Yoga;
using UnityEngine;

public class PortraitLandscape : MonoBehaviour
{
    [SerializeField] private FlexLayout _flexLayout;
    void Start()
    {
        if (Screen.width > Screen.height)
        {
            _flexLayout.FlexDirection = FlexDirection.Row;
        }
        else
        {
            _flexLayout.FlexDirection = FlexDirection.Column;
        }
    }


}
