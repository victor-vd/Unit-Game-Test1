using UnityEngine;
using UnityEngine.UIElements;

public class MainView : MonoBehaviour
{
    [SerializeField]
    VisualTreeAsset m_ListEntryTemplate;

    void OnEnable()
    {
        // The UXML is already instantiated by the UIDocument component
        var uiDocument = GetComponent<UIDocument>();

        // Initialize the upgrade list controller
        var upgradeListController = new UpgradeListController();
        upgradeListController.InitializeUpgradeList(uiDocument.rootVisualElement, m_ListEntryTemplate);
    }
}