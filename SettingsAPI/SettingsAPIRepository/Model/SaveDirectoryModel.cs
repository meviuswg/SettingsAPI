﻿using System.ComponentModel;

namespace SettingsAPIRepository.Model
{
    [DisplayName("SaveDirectory")]
    public class SaveDirectoryModel
    {
        public string Description { get; set; }
        public string Name { get; set; }
    }
}