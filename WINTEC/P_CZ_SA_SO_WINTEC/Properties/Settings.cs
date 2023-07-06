using System;
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
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        Settings defaultInstance = Settings.defaultInstance;
        return defaultInstance;
      }
    }

    [DebuggerNonUserCode]
    [UserScopedSetting]
    [DefaultSettingValue("")]
    public string 영업그룹코드
    {
      get
      {
        return (string) this[nameof (영업그룹코드)];
      }
      set
      {
        this[nameof (영업그룹코드)] = (object) value;
      }
    }

    [DefaultSettingValue("")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public string 영업그룹명
    {
      get
      {
        return (string) this[nameof (영업그룹명)];
      }
      set
      {
        this[nameof (영업그룹명)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    public string 수주형태코드
    {
      get
      {
        return (string) this[nameof (수주형태코드)];
      }
      set
      {
        this[nameof (수주형태코드)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DefaultSettingValue("")]
    [DebuggerNonUserCode]
    public string 수주형태명
    {
      get
      {
        return (string) this[nameof (수주형태명)];
      }
      set
      {
        this[nameof (수주형태명)] = (object) value;
      }
    }

    [DefaultSettingValue("")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public string 회사코드
    {
      get
      {
        return (string) this[nameof (회사코드)];
      }
      set
      {
        this[nameof (회사코드)] = (object) value;
      }
    }

    [DefaultSettingValue("")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public string 화폐단위
    {
      get
      {
        return (string) this[nameof (화폐단위)];
      }
      set
      {
        this[nameof (화폐단위)] = (object) value;
      }
    }

    [DefaultSettingValue("")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public string 부가세포함
    {
      get
      {
        return (string) this[nameof (부가세포함)];
      }
      set
      {
        this[nameof (부가세포함)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    [UserScopedSetting]
    public string 계산서처리
    {
      get
      {
        return (string) this[nameof (계산서처리)];
      }
      set
      {
        this[nameof (계산서처리)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    public string MAIL_ADDR
    {
      get
      {
        return (string) this[nameof (MAIL_ADDR)];
      }
      set
      {
        this[nameof (MAIL_ADDR)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("1")]
    public Decimal 환율
    {
      get
      {
        return (Decimal) this[nameof (환율)];
      }
      set
      {
        this[nameof (환율)] = (object) value;
      }
    }
  }
}
