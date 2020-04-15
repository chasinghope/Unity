using UnityEngine.UI;

public class MInfoPanel : UIBase
{
    // Start is called before the first frame update
    private void Start()
    {
        GetControl<Button>("CloseBtn").onClick.AddListener(CloseBtn_click);
    }

    private void CloseBtn_click()
    {
        Hide();
    }

}
