using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace cz
{
    [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    [CompilerGenerated]
    internal sealed class P_PU_ITR_REG_st : ApplicationSettingsBase
    {
        private static P_PU_ITR_REG_st defaultInstance = (P_PU_ITR_REG_st)SettingsBase.Synchronized((SettingsBase)new P_PU_ITR_REG_st());

        public static P_PU_ITR_REG_st Default
        {
            get
            {
                P_PU_ITR_REG_st defaultInstance = P_PU_ITR_REG_st.defaultInstance;
                return defaultInstance;
            }
        }

        [DebuggerNonUserCode]
        [UserScopedSetting]
        [DefaultSettingValue("")]
        public string PARAMETER_um
        {
            get => (string)this[nameof(PARAMETER_um)];
            set => this[nameof(PARAMETER_um)] = (object)value;
        }

        [DefaultSettingValue("")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public string PARAMETER_EX
        {
            get => (string)this[nameof(PARAMETER_EX)];
            set => this[nameof(PARAMETER_EX)] = (object)value;
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

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        public string ITR_UM_SETTING
        {
            get => (string)this[nameof(ITR_UM_SETTING)];
            set => this[nameof(ITR_UM_SETTING)] = (object)value;
        }
    }
}
