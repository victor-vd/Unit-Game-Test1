using UnityEngine.UIElements;

public class UpgradeListEntryController
{
    Label m_NameLabel;

    // This function retrieves a reference to the 
    // upgrade name label inside the UI element.
    public void SetVisualElement(VisualElement visualElement)
    {
        m_NameLabel = visualElement.Q<Label>("upgrade-name");
    }

    // This function receives the upgrade whose name this list 
    // element is supposed to display. Since the elements list 
    // in a `ListView` are pooled and reused, it's necessary to 
    // have a `Set` function to change which upgrade's data to display.
    public void SetUpgradeData(UpgradeData upgradeData)
    {
        m_NameLabel.text = upgradeData.Name;
    }
}
