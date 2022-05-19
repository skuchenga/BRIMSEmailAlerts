using System.Configuration;
using System.Xml;
using BRIMS.EmailAlerts.Common.Utils;

namespace BRIMS.EmailAlerts.BusinessLogic.Configuration
{
    public partial class Config : IConfigurationSectionHandler
    {
    #region Field
    private static XmlNode _scheduleTasks;
    private static bool _initialized;
    private static string _connectionString = "";

    #endregion


    #region Methods

    /// Creates a configuration section handler.
    /// </summary>
    /// <param name="parent">Parent object.</param>
    /// <param name="configContext">Configuration context object.</param>
    /// <param name="section">Section XML node.</param>
    /// <returns>The created section handler object.</returns>
    public object Create(object parent, object configContext, XmlNode section)
    {
        _connectionString = CommonHelper.GetConnectionString();
        _scheduleTasks = section.SelectSingleNode("ScheduleTasks");

        return null;
    }

    /// <summary>
    /// Initializes the NopConfig object
    /// </summary>
    public static void Init()
    {
        if (!_initialized)
        {
            ConfigurationManager.GetSection("BRIMSConfig");
            _initialized = true;
        }
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the connection string that is used to connect to the storage
    /// </summary>
    public static string ConnectionString
    {
        get
        {
            return _connectionString;
        }
        set
        {
            _connectionString = value;
        }
    }

    /// <summary>
    /// Gets or sets a schedule tasks section
    /// </summary>
    public static XmlNode ScheduleTasks
    {
        get
        {
            return _scheduleTasks;
        }
        set
        {
            _scheduleTasks = value;
        }
    }

    #endregion

    }
}
