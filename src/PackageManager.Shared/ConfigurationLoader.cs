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

            GetConsoleHosts(configuration, xmlDocument);

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

        private static IEnumerable<Command> GetCommands(string action, XmlNodeList nodes,
            IEnumerable<Command> commandList = default)
        {
            var list = commandList?.ToList() ?? new List<Command>();

            foreach (XmlNode node in nodes)
            {
                var command = node.GetValues<Command>();
                command.Action = action;
                list.Add(command);
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
                output.Files = GetFiles(node);
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
                var file = node.GetValues<File>();
                file.FileExtensions = GetFileExtensions(node);
                fileList.Add(file);
            }

            return fileList;
        }

        private static IEnumerable<FileExtension> GetFileExtensions(XmlNode outputNode)
        {
            var fileList = new List<FileExtension>();
            const string getFilesXPath = "path[@enabled='true']";
            var nodes = outputNode.SelectNodes(getFilesXPath);
            foreach (XmlNode node in nodes)
            {
                var fileExtension = node.GetValues<FileExtension>();
                fileList.Add(fileExtension);
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
                var module = node.GetValues<Module>();
                modules.Add(module);
            }

            configuration.Modules = modules;
        }

        private static void GetConsoleHosts(IConfiguration configuration,
            XmlDocument xmlDocument)
        {
            const string consoleHostXPath = "/config/console/hosts/add[@enabled='true']";

            var nodes = xmlDocument.SelectNodes(consoleHostXPath);
            var consoleHosts = new List<IConsoleHost>();
            foreach (XmlNode node in nodes)
            {
                var consoleHost = node.GetValues<ConsoleHost>();
                consoleHosts.Add(consoleHost);
            }

            configuration.ConsoleHosts = consoleHosts;
        }
    }
}
