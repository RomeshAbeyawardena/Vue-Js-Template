using SystemConsole = System.Console;
using Utility.CommandLine;
using System.Xml;

namespace PackageManager.Console
{
    class Program
    {
        [Argument('s', "solutionName", "Name of the solution")]
        static string SolutionName { get; set; }

        [Argument('o', "output", "Output of solution and projects")]
        static string Output { get; set; }

        [Argument('n', "projectNames", "Output of files")]
        static string ProjectNames { get; set; }

        static void Main(string[] args)
        {
            Arguments.Populate();
            SystemConsole.WriteLine("{0} {1} {2}", SolutionName, Output, ProjectNames);

            var xmlDocument = new XmlDocument();
            xmlDocument.Load("config.xml");
            var nodes = xmlDocument.SelectNodes("/config/build/commands/add[@enabled='true']|remove[@enabled='true']");
            foreach (var item in ProjectNames.Split(','))
            {
                SystemConsole.WriteLine(item);
            }
        }
    }
}
