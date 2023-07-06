using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace cz
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
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

    [DefaultSettingValue("")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
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

    [UserScopedSetting]
    [DefaultSettingValue("")]
    [DebuggerNonUserCode]
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
    [DefaultSettingValue("")]
    [DebuggerNonUserCode]
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

    [DefaultSettingValue("")]
    [UserScopedSetting]
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

    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    [UserScopedSetting]
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
    public string 단가유형
    {
      get
      {
        return (string) this[nameof (단가유형)];
      }
      set
      {
        this[nameof (단가유형)] = (object) value;
      }
    }
  }
}
