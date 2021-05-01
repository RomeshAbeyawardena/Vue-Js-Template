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

            GetOutputs(configuration, xmlDocument);

            GetModules(configuration, xmlDocument);

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
    
        private static void GetOutputs(IConfiguration configuration,
            XmlDocument xmlDocument)
        {
            const string getOutputsXPath = "/config/outputs/{0}[@enabled='true']";
            
            var outputNodes = xmlDocument.SelectNodes(getOutputsXPath.Format(Action_Add));

            var outputList = new List<Output>();
            
            foreach (XmlNode node in outputNodes)
            {
                var output = node.GetValues<Output>();
                output.Action = Action_Add;
                outputList.Add(output);
            }
            configuration.Outputs = outputList;
        }

        private static IEnumerable<File> GetFiles(XmlNode outputNode)
        {
            var fileList = new List<File>();
            const string getFilesXPath = "//add/files/paths/add";
            var nodes = outputNode.SelectNodes(getFilesXPath);
            foreach (XmlNode node in nodes)
            {
                fileList.Add(new File
                {
                    Filter = node.Attributes["filter"].Value,
                    From = node.Attributes["from"].Value,
                    To = node.Attributes["to"].Value
                });
            }

            return fileList;
        }

        private static IEnumerable<FileExtension> GetFileExtensions(XmlNode outputNode)
        {
            var fileList = new List<FileExtension>();
            const string getFilesXPath = "//add/files/extensions/add[@enabled='true']";
            var nodes = outputNode.SelectNodes(getFilesXPath);
            foreach (XmlNode node in nodes)
            {
                fileList.Add(new FileExtension
                {
                    Enabled = node.Attributes["enabled"].Value.TryParseBool(),
                    Value = node.Attributes["value"].Value,
                    Type = node.Attributes["type"].Value
                });
            }

            return fileList;
        }

        private static void GetModules(IConfiguration configuration,
            XmlDocument xmlDocument)
        {
            var modules = new List<Module>();

            var nodes = xmlDocument.SelectNodes("/config/modules/add[@enabled='true']");

            foreach (XmlNode node in nodes)
            {
                modules.Add(new Module
                {
                    Enabled = node.Attributes["enabled"].Value.TryParseBool(),
                    AssemblyName = node.Attributes["assembly"].Value,
                    Type = node.Attributes["type"].Value
                });
            }

            configuration.Modules = modules;
        }
    }
}
