using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData;

public class CharacterCreationScreenObject : MonoBehaviour {

	private Button _buttonCreateCharacter;

    private Dropdown _dropdownClass;
    private Dropdown _dropdownRace;
    private InputField _inputName;
    private Text _textError;

	// Use this for initialization
	void Start () {
        _textError = transform.Find("TextError").gameObject.GetComponent<Text>();
        _inputName = transform.Find("Name").gameObject.GetComponent<InputField>();

		/* Fill dropdowns */
        _dropdownRace = transform.Find("Selection/DropdownRace/Dropdown").gameObject.GetComponent<Dropdown>();
		_dropdownClass = transform.Find("Selection/DropdownClass/Dropdown").gameObject.GetComponent<Dropdown>();

		List<Dropdown.OptionData> optionsRace = new List<Dropdown.OptionData>();
		optionsRace.Add(new Dropdown.OptionData(RaceType.Goblin.ToString()));
        optionsRace.Add(new Dropdown.OptionData(RaceType.Human.ToString()));
        optionsRace.Add(new Dropdown.OptionData(RaceType.Orc.ToString()));
        optionsRace.Add(new Dropdown.OptionData(RaceType.Troll.ToString()));
        _dropdownRace.AddOptions(optionsRace);

		List<Dropdown.OptionData> optionsClass = new List<Dropdown.OptionData>();
        optionsClass.Add(new Dropdown.OptionData(ClassType.Mage.ToString()));
        optionsClass.Add(new Dropdown.OptionData(ClassType.Priest.ToString()));
        optionsClass.Add(new Dropdown.OptionData(ClassType.Rouge.ToString()));
        optionsClass.Add(new Dropdown.OptionData(ClassType.Warrior.ToString()));
        _dropdownClass.AddOptions(optionsClass);

		_buttonCreateCharacter = GetComponentInChildren<Button> ();
		_buttonCreateCharacter.onClick.AddListener (delegate {
            ClearError();
			Global.Client.SendBytes(Network.NetworkHelper.Serialize(new Network.Packages.CreateCharacterRequest(){
				CharacterData = new Network.Data.Character(){
                    Class =  (ClassType)Enum.Parse(typeof(ClassType), optionsClass[_dropdownClass.value].text),
					Fraction = GameData.FractionType.C,
                    Name = _inputName.text,
                    Race = (RaceType)Enum.Parse(typeof(RaceType), optionsRace[_dropdownRace.value].text)
				}
			}));
		});
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowError(string text)
    {
        _textError.text = text;   
    }

    public void ClearError()
    {
        ShowError("");
    }
}
