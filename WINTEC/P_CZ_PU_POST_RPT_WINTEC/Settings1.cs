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

        [UserScopedSetting]
        [DefaultSettingValue("")]
        [DebuggerNonUserCode]
        public string bp_REEMP_SAVE
        {
            get => (string)this[nameof(bp_REEMP_SAVE)];
            set => this[nameof(bp_REEMP_SAVE)] = (object)value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        public string bp_REEMP_SAVE1
        {
            get => (string)this[nameof(bp_REEMP_SAVE1)];
            set => this[nameof(bp_REEMP_SAVE1)] = (object)value;
        }

        [DefaultSettingValue("")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public string bp_REDEPT_SAVE
        {
            get => (string)this[nameof(bp_REDEPT_SAVE)];
            set => this[nameof(bp_REDEPT_SAVE)] = (object)value;
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        [UserScopedSetting]
        public string bp_REDEPT_SAVE1
        {
            get => (string)this[nameof(bp_REDEPT_SAVE1)];
            set => this[nameof(bp_REDEPT_SAVE1)] = (object)value;
        }
    }
}
