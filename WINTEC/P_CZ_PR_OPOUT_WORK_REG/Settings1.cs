using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace prd
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
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool Wo_DcRmk_Apply_YN
        {
            get => (bool)this[nameof(Wo_DcRmk_Apply_YN)];
            set => this[nameof(Wo_DcRmk_Apply_YN)] = (object)value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("True")]
        public bool 재작업잔량여부
        {
            get => (bool)this[nameof(재작업잔량여부)];
            set => this[nameof(재작업잔량여부)] = (object)value;
        }
    }
}