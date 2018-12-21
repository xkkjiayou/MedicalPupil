using System.Configuration;
using MedicalSys.Framework;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;

namespace MedicalSys.DataAccessor
{
    public static class DAOConfiguration
    {
        public const string DEFAULT_CONNECTION_NAME = "MedicalSystem";
        public const string DEFUALT_CONNECTION_PROVIDER = "System.Data.SqlClient";
        private const string SECTION_NAME_CONNECTION_STRING = "connectionStrings";
        private const string SECTION_NAME_DATA_CONFIGURATION = "dataConfiguration";

        ///// <summary>
        ///// Initials the database configuration.
        ///// </summary>
        public static void InitialDatabaseConfiguration()
        {
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            ConnectionStringSettingsCollection cnnSettingSet = cfg.ConnectionStrings.ConnectionStrings;

            if (null != cnnSettingSet[DEFAULT_CONNECTION_NAME])
            {
                cnnSettingSet[DEFAULT_CONNECTION_NAME].ProviderName = DEFUALT_CONNECTION_PROVIDER;
                cnnSettingSet[DEFAULT_CONNECTION_NAME].ConnectionString = IniSettingConfig.GetInstance().DatabaseDescription;
            }
            else
            {
                ConnectionStringSettings settings =
                new ConnectionStringSettings(DEFAULT_CONNECTION_NAME,
                    IniSettingConfig.GetInstance().DatabaseDescription, DEFUALT_CONNECTION_PROVIDER);
                cfg.ConnectionStrings.ConnectionStrings.Add(settings);
            }

            DatabaseSettings databaseSettings = cfg.GetSection(SECTION_NAME_DATA_CONFIGURATION) as DatabaseSettings;

            if (databaseSettings != null)
            {
                databaseSettings.DefaultDatabase = DEFAULT_CONNECTION_NAME;
            }
            else
            {
                databaseSettings = new DatabaseSettings();
                databaseSettings.DefaultDatabase = DEFAULT_CONNECTION_NAME;
                cfg.Sections.Add(SECTION_NAME_DATA_CONFIGURATION, databaseSettings);
            }

            ConfigurationSection section = cfg.Sections[SECTION_NAME_CONNECTION_STRING];

            section.SectionInformation.ForceSave = true;
            cfg.Save(ConfigurationSaveMode.Full);


            ConfigurationManager.RefreshSection(SECTION_NAME_CONNECTION_STRING);
            ConfigurationManager.RefreshSection(SECTION_NAME_DATA_CONFIGURATION);
        }
    }
}
