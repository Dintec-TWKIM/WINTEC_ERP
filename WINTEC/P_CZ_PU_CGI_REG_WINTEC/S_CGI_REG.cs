using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace cz
{
    [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    [CompilerGenerated]
    internal sealed class S_CGI_REG : ApplicationSettingsBase
    {
        private static S_CGI_REG defaultInstance = (S_CGI_REG)SettingsBase.Synchronized((SettingsBase)new S_CGI_REG());

        public static S_CGI_REG Default
        {
            get
            {
                S_CGI_REG defaultInstance = S_CGI_REG.defaultInstance;
                return defaultInstance;
            }
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        [UserScopedSetting]
        public string CGI_UM_SETTING
        {
            get => (string)this[nameof(CGI_UM_SETTING)];
            set => this[nameof(CGI_UM_SETTING)] = (object)value;
        }
    }
}
