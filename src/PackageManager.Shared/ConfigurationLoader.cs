using PackageManager.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using PackageManager.Shared.Extensions;

namespace PackageManager.Shared.Domain.Models
{
    public class ConfigurationLoader : IConfigurationLoader
    {
        private const string Action_Add = "add";
        private const string Action_Remove = "remove";

        public IConfiguration LoadConfigurationFromXml(IConfiguration configuration, string defaultConfigurationPath = null)
        {
            var xmlDocument = new XmlDocument();

            xmlDocument.Load(configuration.ConfigurationPath ?? defaultConfigurationPath);

            GetCommands(configuration, xmlDocument);

            return configuration;
        }

        private static void GetCommands(IConfiguration configuration,
            XmlDocument xmlDocument)
        {
            const string commandXPath = "/config/build/commands/{0}[@enabled='true']";
            configuration.Commands = GetCommands(Action_Add, xmlDocument
                .SelectNodes(commandXPath.Format(Action_Add)));

            configuration.Commands = GetCommands(Action_Remove, xmlDocument
                .SelectNodes(commandXPath.Format(Action_Remove)), configuration.Commands);

        }

        private static IEnumerable<Command> GetCommands(string action, XmlNodeList commands,
            IEnumerable<Command> commandList = default)
        {
            var list = commandList?.ToList() ?? new List<Command>();

            foreach (XmlNode command in commands)
            {
                list.Add(new Command
                {
                    Action = action,
                    Enabled = command.Attributes.GetNamedItem("enabled").InnerText.TryParseBool(),
                    Key = command.Attributes.GetNamedItem("key").InnerText,
                    Value = command.Attributes.GetNamedItem("value")?.InnerText,
                });
            }

            return list;
        }
    }
}
