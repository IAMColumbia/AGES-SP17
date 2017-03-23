using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuButtons : MonoBehaviour
{
    [SerializeField]
    GameObject startPanel;
    [SerializeField]
    GameObject playerSelectPanel;
    [SerializeField]
    GameObject creditsPanel;
    [SerializeField]
    GameObject weaponSelectPanel;
    [SerializeField]
    AbilitySelect AbSelect;
    [SerializeField]
    Toggle readyToggle1;
    [SerializeField]
    Toggle readyToggle2;
    [SerializeField]
    Toggle readyToggle3;
    [SerializeField]
    Toggle readyToggle4;
    [SerializeField]
    GameObject selectionPanel1;
    [SerializeField]
    GameObject selectionPanel2;
    [SerializeField]
    GameObject selectionPanel3;
    [SerializeField]
    GameObject selectionPanel4;
    [SerializeField]
    Dropdown P1AbilitySelect1;
    [SerializeField]
    Dropdown P1AbilitySelect2;
    [SerializeField]
    Dropdown P2AbilitySelect1;
    [SerializeField]
    Dropdown P2AbilitySelect2;
    [SerializeField]
    Dropdown P3AbilitySelect1;
    [SerializeField]
    Dropdown P3AbilitySelect2;
    [SerializeField]
    Dropdown P4AbilitySelect1;
    [SerializeField]
    Dropdown P4AbilitySelect2;

    List<Toggle> readyToggles = new List<Toggle>();
    List<GameObject> selectionPanels = new List<GameObject>();
    List<Dropdown> playerAbilities = new List<Dropdown>();

    private void Start()
    {

    }

    public void StartButtonPressed()
    {
        playerSelectPanel.SetActive(true);
        startPanel.SetActive(false);
    }

    public void CreditsButtonPressed()
    {
        creditsPanel.SetActive(true);
    }

    public void BackCreditsButtonPressed()
    {
        creditsPanel.SetActive(false);
    }

    public void BackPlayerSelectButtonPressed()
    {
        startPanel.SetActive(true);
        playerSelectPanel.SetActive(false);
    }

    public void BackWeaponSelectButtonPressed()
    {
        foreach (GameObject selectionPanel in selectionPanels)
        {
            selectionPanel.SetActive(false);
        }

        foreach (Toggle readyToggle in readyToggles)
        {
            if (readyToggle.isOn)
            {
                readyToggle.isOn = false;
            }
        }

        foreach (Dropdown abilityChoice in playerAbilities)
        {
            abilityChoice.value = 0;
        }

        selectionPanels.Clear();
        readyToggles.Clear();
        playerAbilities.Clear();

        playerSelectPanel.SetActive(true);
        weaponSelectPanel.SetActive(false);
    }

    public void QuitButtonPressed()
    {
        Application.Quit();
    }

    public void TwoPlayersButtonPressed()
    {
        //number of players = 2
        selectionPanels.Add(selectionPanel1);
        selectionPanels.Add(selectionPanel2);

        readyToggles.Add(readyToggle1);
        readyToggles.Add(readyToggle2);

        playerAbilities.Add(P1AbilitySelect1);
        playerAbilities.Add(P1AbilitySelect2);
        playerAbilities.Add(P2AbilitySelect1);
        playerAbilities.Add(P2AbilitySelect2);

        foreach (GameObject selectionPanel in selectionPanels)
        {
            selectionPanel.SetActive(true);
        }

        weaponSelectPanel.SetActive(true);
        playerSelectPanel.SetActive(false);
    }

    public void ThreePlayersButtonPressed()
    {
        //number of players = 3
        selectionPanels.Add(selectionPanel1);
        selectionPanels.Add(selectionPanel2);
        selectionPanels.Add(selectionPanel3);

        readyToggles.Add(readyToggle1);
        readyToggles.Add(readyToggle2);
        readyToggles.Add(readyToggle3);

        playerAbilities.Add(P1AbilitySelect1);
        playerAbilities.Add(P1AbilitySelect2);
        playerAbilities.Add(P2AbilitySelect1);
        playerAbilities.Add(P2AbilitySelect2);
        playerAbilities.Add(P3AbilitySelect1);
        playerAbilities.Add(P3AbilitySelect2);

        foreach (GameObject selectionPanel in selectionPanels)
        {
            selectionPanel.SetActive(true);
        }

        weaponSelectPanel.SetActive(true);
        playerSelectPanel.SetActive(false);
    }

    public void FourPlayersButtonPressed()
    {
        //number of players = 4
        selectionPanels.Add(selectionPanel1);
        selectionPanels.Add(selectionPanel2);
        selectionPanels.Add(selectionPanel3);
        selectionPanels.Add(selectionPanel4);

        readyToggles.Add(readyToggle1);
        readyToggles.Add(readyToggle2);
        readyToggles.Add(readyToggle3);
        readyToggles.Add(readyToggle4);

        playerAbilities.Add(P1AbilitySelect1);
        playerAbilities.Add(P1AbilitySelect2);
        playerAbilities.Add(P2AbilitySelect1);
        playerAbilities.Add(P2AbilitySelect2);
        playerAbilities.Add(P3AbilitySelect1);
        playerAbilities.Add(P3AbilitySelect2);
        playerAbilities.Add(P4AbilitySelect1);
        playerAbilities.Add(P4AbilitySelect2);

        foreach (GameObject selectionPanel in selectionPanels)
        {
            selectionPanel.SetActive(true);
        }

        weaponSelectPanel.SetActive(true);
        playerSelectPanel.SetActive(false);
    }

    public void StartGame()
    {
        int numberOfTogglesOn = 0;

        foreach (Toggle readyToggle in readyToggles)
        {
            if (readyToggle.isOn)
            {
                numberOfTogglesOn++;
            }
        }

        if (numberOfTogglesOn == readyToggles.Count)
        {
            AbSelect.P1chosenAbility1 = AbSelect.abilities[P1AbilitySelect1.value];
            AbSelect.P1chosenAbility2 = AbSelect.abilities[P1AbilitySelect2.value];
            AbSelect.P2chosenAbility1 = AbSelect.abilities[P2AbilitySelect1.value];
            AbSelect.P2chosenAbility2 = AbSelect.abilities[P2AbilitySelect2.value];

            if (selectionPanel3.activeSelf)
            {
                AbSelect.P3chosenAbility1 = AbSelect.abilities[P3AbilitySelect1.value];
                AbSelect.P3chosenAbility2 = AbSelect.abilities[P3AbilitySelect2.value];
            }

            if (selectionPanel4.activeSelf)
            {
                AbSelect.P4chosenAbility1 = AbSelect.abilities[P4AbilitySelect1.value];
                AbSelect.P4chosenAbility2 = AbSelect.abilities[P4AbilitySelect2.value];
            }

            DontDestroyOnLoad(AbSelect);
            SceneManager.LoadScene("Scene1");
        }
    }
}
