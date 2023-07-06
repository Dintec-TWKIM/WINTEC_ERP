using System;
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
        public string CD_PURGRP_SET
        {
            get => (string)this[nameof(CD_PURGRP_SET)];
            set => this[nameof(CD_PURGRP_SET)] = (object)value;
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        [UserScopedSetting]
        public string CD_TPPO_SET
        {
            get => (string)this[nameof(CD_TPPO_SET)];
            set => this[nameof(CD_TPPO_SET)] = (object)value;
        }

        [UserScopedSetting]
        [DefaultSettingValue("")]
        [DebuggerNonUserCode]
        public string FG_PAYMENT_SET
        {
            get => (string)this[nameof(FG_PAYMENT_SET)];
            set => this[nameof(FG_PAYMENT_SET)] = (object)value;
        }

        [DebuggerNonUserCode]
        [UserScopedSetting]
        [DefaultSettingValue("")]
        public string PARAMETER_um
        {
            get => (string)this[nameof(PARAMETER_um)];
            set => this[nameof(PARAMETER_um)] = (object)value;
        }

        [DefaultSettingValue("000")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public string CD_EXCH
        {
            get => (string)this[nameof(CD_EXCH)];
            set => this[nameof(CD_EXCH)] = (object)value;
        }

        [DefaultSettingValue("0")]
        [DebuggerNonUserCode]
        [UserScopedSetting]
        public Decimal RT_EXCH
        {
            get => (Decimal)this[nameof(RT_EXCH)];
            set => this[nameof(RT_EXCH)] = (object)value;
        }

        [UserScopedSetting]
        [DefaultSettingValue("")]
        [DebuggerNonUserCode]
        public string MAIL_ADD
        {
            get => (string)this[nameof(MAIL_ADD)];
            set => this[nameof(MAIL_ADD)] = (object)value;
        }

        [DefaultSettingValue("")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public string DC_RMK_TEXT
        {
            get => (string)this[nameof(DC_RMK_TEXT)];
            set => this[nameof(DC_RMK_TEXT)] = (object)value;
        }

        [UserScopedSetting]
        [DefaultSettingValue("")]
        [DebuggerNonUserCode]
        public string DC_RMK_TEXT2
        {
            get => (string)this[nameof(DC_RMK_TEXT2)];
            set => this[nameof(DC_RMK_TEXT2)] = (object)value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("002")]
        public string TP_UM_TAX
        {
            get => (string)this[nameof(TP_UM_TAX)];
            set => this[nameof(TP_UM_TAX)] = (object)value;
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("")]
        [UserScopedSetting]
        public string MAIL_TEXT
        {
            get => (string)this[nameof(MAIL_TEXT)];
            set => this[nameof(MAIL_TEXT)] = (object)value;
        }
    }
}
