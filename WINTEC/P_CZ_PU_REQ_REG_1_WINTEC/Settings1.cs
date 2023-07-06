using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace cz
{
    [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    [CompilerGenerated]
    internal sealed class Settings1 : ApplicationSettingsBase
    {
        private static Settings1 defaultInstance = (Settings1)SettingsBase.Synchronized((SettingsBase)new Settings1());

        public static Settings1 Default
        {
            get
            {
                Settings1 defaultInstance = Settings1.defaultInstance;
                return defaultInstance;
            }
        }

        [DefaultSettingValue("")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public string email_add
        {
            get => (string)this[nameof(email_add)];
            set => this[nameof(email_add)] = (object)value;
        }

        [DefaultSettingValue("")]
        [DebuggerNonUserCode]
        [UserScopedSetting]
        public string chk_barcode_use
        {
            get => (string)this[nameof(chk_barcode_use)];
            set => this[nameof(chk_barcode_use)] = (object)value;
        }

        [DebuggerNonUserCode]
        [UserScopedSetting]
        [DefaultSettingValue("")]
        public string cd_sl_apply
        {
            get => (string)this[nameof(cd_sl_apply)];
            set => this[nameof(cd_sl_apply)] = (object)value;
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        [UserScopedSetting]
        public string nm_sl_apply
        {
            get => (string)this[nameof(nm_sl_apply)];
            set => this[nameof(nm_sl_apply)] = (object)value;
        }

        [DefaultSettingValue("")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public string cd_plant
        {
            get => (string)this[nameof(cd_plant)];
            set => this[nameof(cd_plant)] = (object)value;
        }
    }
}