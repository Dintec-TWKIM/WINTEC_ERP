using System.CodeDom.Compiler;
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

        public static Settings Default
        {
            get
            {
                Settings defaultInstance = Settings.defaultInstance;
                return defaultInstance;
            }
        }

        [DefaultSettingValue("")]
        [DebuggerNonUserCode]
        [UserScopedSetting]
        public string 수불형태코드
        {
            get => (string)this[nameof(수불형태코드)];
            set => this[nameof(수불형태코드)] = (object)value;
        }

        [UserScopedSetting]
        [DefaultSettingValue("")]
        [DebuggerNonUserCode]
        public string 수불형태명
        {
            get => (string)this[nameof(수불형태명)];
            set => this[nameof(수불형태명)] = (object)value;
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        [UserScopedSetting]
        public string 회사코드
        {
            get => (string)this[nameof(회사코드)];
            set => this[nameof(회사코드)] = (object)value;
        }

        [UserScopedSetting]
        [DefaultSettingValue("")]
        [DebuggerNonUserCode]
        public string FG_IO
        {
            get => (string)this[nameof(FG_IO)];
            set => this[nameof(FG_IO)] = (object)value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        public string YN_AM
        {
            get => (string)this[nameof(YN_AM)];
            set => this[nameof(YN_AM)] = (object)value;
        }
    }
}
