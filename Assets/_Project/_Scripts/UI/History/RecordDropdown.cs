using System;
using System.Collections.Generic;
using SnekTech.Core.History;
using TMPro;
using UnityEngine;
using OptionData = TMPro.TMP_Dropdown.OptionData;

namespace SnekTech.UI.History
{
    [RequireComponent(typeof(TMP_Dropdown))]
    public class RecordDropdown : MonoBehaviour
    {
        [SerializeField]
        private GameHistory history;

        [SerializeField]
        private RecordPanel recordPanel;

        [SerializeField]
        private string winText;

        [SerializeField]
        private string loseText;
        
        private TMP_Dropdown _dropdown;
        private List<Record> _records;
        
        private void Awake()
        {
            _dropdown = GetComponent<TMP_Dropdown>();
            
            GenerateDropdownOptions();
            recordPanel.SetContent(_records[_dropdown.value]);
        }

        private void OnEnable()
        {
            _dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }

        private void OnDisable()
        {
            _dropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);
        }

        private void OnDropdownValueChanged(int index)
        {
            recordPanel.SetContent(_records[index]);
        }

        private void GenerateDropdownOptions()
        {
            List<OptionData> options = new List<OptionData>();
            _records = history.Records;
            foreach (Record record in _records)
            {
                var createdAt = new DateTime(record.CreatedAt);
                string optionText = $"{(record.HasFailed ? loseText : winText)} - {createdAt.ToShortTimeString()}";
                options.Add(new OptionData(optionText));
            }
            _dropdown.options = options;
        }
    }
}
