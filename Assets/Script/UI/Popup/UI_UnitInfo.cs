using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class UI_UnitInfo : UI_Popup
{
    [SerializeField] private UI_UnitCard _unitCard;
    [SerializeField] private GameObject _selectButton;
    [SerializeField] private GameObject _fallGaugePrefab;
    [SerializeField] private GameObject _stigmaPrefab;
    [SerializeField] private GameObject _squarePrefab;

    [SerializeField] private TextMeshProUGUI _unitInfoName;
    [SerializeField] private TextMeshProUGUI _unitInfoCost;
    [SerializeField] private Transform _unitInfoFallGrid;
    [SerializeField] private TextMeshProUGUI _unitInfoStat;
    [SerializeField] private Transform _unitInfoStigmaGrid;
    [SerializeField] private Transform _unitInfoSkillRangeGrid;
    [SerializeField] private TextMeshProUGUI _unitInfoSkillDescrption;
    [SerializeField] private Transform _unitInfoSkillimage;

    private DeckUnit _unit;
    private Action<DeckUnit> _onSelect;

    public void SetUnit(DeckUnit unit)
    {
        _unit = unit;
    }

    public void Init(Action<DeckUnit> onSelect=null)
    {
        _onSelect = onSelect;
        _selectButton.SetActive(onSelect != null);

        _unitCard.Set(_unit.Data.Image, _unit.Data.Name, _unit.Stat.ManaCost.ToString());

        _unitInfoName.text = _unit.Data.Name;
        _unitInfoCost.text = _unit.Stat.ManaCost.ToString();

        _unitInfoStat.text = "HP:     " + _unit.Stat.HP.ToString() + "\n" +
                                 "Attack: " + _unit.Stat.ATK.ToString() + "\n" +
                                 "Speed:  " + _unit.Stat.SPD.ToString();

        _unitInfoSkillDescrption.text = _unit.Data.Description.Replace("(ATK)", _unit.Stat.ATK.ToString());

        for (int i = 0; i < _unit.Stat.FallMaxCount; i++)
        {
            UI_FallGauge fg = GameObject.Instantiate(_fallGaugePrefab, _unitInfoFallGrid).GetComponent<UI_FallGauge>();
            if (i < _unit.Stat.FallCurrentCount)
                fg.Set(true);
            else
                fg.Set(false);

            fg.Init();
        }

        _unit.SetStigma();

        foreach (Passive sti in _unit.Stigmata)
        {
            ���� stig = _unit.PassiveToStigma(sti);

            GameObject.Instantiate(_stigmaPrefab, _unitInfoStigmaGrid).GetComponent<UI_Stigma>().SetImage(_unit.GetStigmaImage(stig), _unit.GetStigmaText(stig));
        }

        foreach (bool range in _unit.Data.AttackRange)
        {
            Image block = GameObject.Instantiate(_squarePrefab, _unitInfoSkillRangeGrid).GetComponent<Image>();
            if (range)
                block.color = Color.red;
            else
                block.color = Color.grey;
        }
    }

    public void Quit()
    {
        GameManager.UI.ClosePopup();
    }

    public void Select()
    {
        _onSelect(_unit);
    }
}