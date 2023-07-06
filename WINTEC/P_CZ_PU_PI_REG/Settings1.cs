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

        [DebuggerNonUserCode]
        [DefaultSettingValue("N")]
        [UserScopedSetting]
        public string chk_use_lot
        {
            get => (string)this[nameof(chk_use_lot)];
            set => this[nameof(chk_use_lot)] = (object)value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool chk_yn_pjt
        {
            get => (bool)this[nameof(chk_yn_pjt)];
            set => this[nameof(chk_yn_pjt)] = (object)value;
        }
    }
}