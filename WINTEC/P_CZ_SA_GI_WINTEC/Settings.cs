using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace cz
{
    [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    [CompilerGenerated]
    internal sealed class Settings : ApplicationSettingsBase
    {
        private static Settings defaultInstance = (Settings)SettingsBase.Synchronized((SettingsBase)new Settings());

        private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e)
        {
        }

        private void SettingsSavingEventHandler(object sender, CancelEventArgs e)
        {
        }

        public static Settings Default
        {
            get
            {
                Settings defaultInstance = Settings.defaultInstance;
                return defaultInstance;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("GIR")]
        [UserScopedSetting]
        public string auto_No
        {
            get => (string)this[nameof(auto_No)];
            set => this[nameof(auto_No)] = (object)value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool 의뢰비고적용여부
        {
            get => (bool)this[nameof(의뢰비고적용여부)];
            set => this[nameof(의뢰비고적용여부)] = (object)value;
        }
    }
}