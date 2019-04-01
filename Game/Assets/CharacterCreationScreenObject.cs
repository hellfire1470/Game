using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameData.Environment.Entity;
using GameData.Network;
using GameData.Network.Packages;

public class CharacterCreationScreenObject : MonoBehaviour
{

    private Button _buttonCreateCharacter;

    private CharacterAppearance _appearance;

    private Dropdown _dropdownClass;
    private Dropdown _dropdownRace;
    private InputField _inputName;
    private Text _textError;
    private Slider _sliderNose;
    private Slider _sliderMouth;

    // Use this for initialization
    void Start()
    {
        _appearance = transform.Find("CharacterPreview/Character").GetComponent<CharacterAppearance>();

        _textError = transform.Find("TextError").gameObject.GetComponent<Text>();
        _inputName = transform.Find("Name").gameObject.GetComponent<InputField>();

        /* Fill dropdowns */
        _dropdownRace = transform.Find("Selection/DropdownRace/Dropdown").gameObject.GetComponent<Dropdown>();
        _dropdownClass = transform.Find("Selection/DropdownClass/Dropdown").gameObject.GetComponent<Dropdown>();

        _sliderNose = transform.Find("Selection/SliderNose").gameObject.GetComponent<Slider>();
        _sliderMouth = transform.Find("Selection/SliderMouth").gameObject.GetComponent<Slider>();

        _sliderNose.onValueChanged.AddListener((arg0) =>
        {
            _appearance.ChangeNose((int)arg0);
        });

        _sliderMouth.onValueChanged.AddListener((arg0) =>
        {
            _appearance.ChangeMouth((int)arg0);
        });

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

        _buttonCreateCharacter = GetComponentInChildren<Button>();
        _buttonCreateCharacter.onClick.AddListener(delegate
        {
            ClearError();
            Global.Client.SendBytes(Network.NetworkHelper.Serialize(new CreateCharacterRequest()
            {
                CharacterData = new Character()
                {
                    Class = (ClassType)Enum.Parse(typeof(ClassType), optionsClass[_dropdownClass.value].text),
                    Fraction = FractionType.C,
                    Name = _inputName.text,
                    Race = (RaceType)Enum.Parse(typeof(RaceType), optionsRace[_dropdownRace.value].text),
                    Sex = SexType.Male,
                    Aye = 0,
                    Mouth = (int)_sliderMouth.value,
                    Nose = (int)_sliderNose.value
                }
            }));
        });
    }

    // Update is called once per frame
    void Update()
    {

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
