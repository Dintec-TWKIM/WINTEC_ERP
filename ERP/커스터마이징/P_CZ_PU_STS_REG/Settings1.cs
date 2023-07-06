// Decompiled with JetBrains decompiler
// Type: pur.Settings1
// Assembly: P_PU_STS_REG, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AE37402A-5961-49F1-AFD9-7DA6930A09D6
// Assembly location: C:\ERPU\Browser\pur\P_PU_STS_REG.dll

using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace cz
{
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
  [CompilerGenerated]
  internal sealed class Settings1 : ApplicationSettingsBase
  {
    private static Settings1 defaultInstance = (Settings1) SettingsBase.Synchronized((SettingsBase) new Settings1());

    public static Settings1 Default
    {
      get
      {
        Settings1 defaultInstance = Settings1.defaultInstance;
        return defaultInstance;
      }
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    [UserScopedSetting]
    public string CD_QTIOTP
    {
      get
      {
        return (string) this["CD_QTIOTP"];
      }
      set
      {
        this["CD_QTIOTP"] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    public string NM_QTIOTP
    {
      get
      {
        return (string) this["NM_QTIOTP"];
      }
      set
      {
        this["NM_QTIOTP"] = (object) value;
      }
    }
  }
}
