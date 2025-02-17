using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeListController
{
    // UXML template for list entries
    VisualTreeAsset m_ListEntryTemplate;

    // UI element references
    ListView m_UpgradeList;
    Label m_UpgradeDescriptionLabel;
    Label m_UpgradeNameLabel;
    VisualElement m_UpgradePortrait;

    List<UpgradeData> m_AllUpgrades;

    public void InitializeUpgradeList(VisualElement root, VisualTreeAsset listElementTemplate)
    {
        EnumerateAllUpgrades();

        // Store a reference to the template for the list entries
        m_ListEntryTemplate = listElementTemplate;

        // Store a reference to the upgrade list element
        m_UpgradeList = root.Q<ListView>("upgrade-list");

        // Store references to the selected upgrade info elements
        m_UpgradeDescriptionLabel = root.Q<Label>("upgrade-description");
        m_UpgradeNameLabel = root.Q<Label>("upgrade-name");
        m_UpgradePortrait = root.Q<VisualElement>("upgrade-portrait");

        FillUpgradeList();

        // Register to get a callback when an item is selected
        m_UpgradeList.selectionChanged += OnUpgradeSelected;
    }

    void EnumerateAllUpgrades()
    {
        m_AllUpgrades = new List<UpgradeData>();
        m_AllUpgrades.AddRange(Resources.LoadAll<UpgradeData>("Upgrades"));
    }

    void FillUpgradeList()
    {
        // Set up a make item function for a list entry
        m_UpgradeList.makeItem = () =>
        {
            // Instantiate the UXML template for the entry
            var newListEntry = m_ListEntryTemplate.Instantiate();

            // Instantiate a controller for the data
            var newListEntryLogic = new UpgradeListEntryController();

            // Assign the controller script to the visual element
            newListEntry.userData = newListEntryLogic;

            // Initialize the controller script
            newListEntryLogic.SetVisualElement(newListEntry);

            // Return the root of the instantiated visual tree
            return newListEntry;
        };

        // Set up bind function for a specific list entry
        m_UpgradeList.bindItem = (item, index) =>
        {
            (item.userData as UpgradeListEntryController)?.SetUpgradeData(m_AllUpgrades[index]);
        };

        // Set a fixed item height matching the height of the item provided in makeItem. 
        // For dynamic height, see the virtualizationMethod property.
        m_UpgradeList.fixedItemHeight = 45;

        // Set the actual item's source list/array
        m_UpgradeList.itemsSource = m_AllUpgrades;
    }

    void OnUpgradeSelected(IEnumerable<object> selectedUpgrades)
    {
        // Get the currently selected item directly from the ListView
        var selectedUpgrade = m_UpgradeList.selectedItem as UpgradeData;

        // Handle none-selection (Escape to deselect everything)
        if (selectedUpgrade == null)
        {
            // Clear
            m_UpgradeDescriptionLabel.text = "";
            m_UpgradeNameLabel.text = "";
            m_UpgradePortrait.style.backgroundImage = null;

            return;
        }

        // Fill in Upgrade details
        m_UpgradeDescriptionLabel.text = selectedUpgrade.Description.ToString();
        m_UpgradeNameLabel.text = selectedUpgrade.Name;
        m_UpgradePortrait.style.backgroundImage = new StyleBackground(selectedUpgrade.PortraitImage);
    }
}
