using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Synergy.Contracts;
using Synergy.Web.Api.Testing.Json;

namespace Synergy.Web.Api.Testing.Assertions
{
    public class CompareResponseWithPattern : Assertion
    {
        private readonly string _patternFilePath;
        private readonly Ignore? _ignore;
        private readonly Mode _mode;
        private JToken? _savedPattern;

        public CompareResponseWithPattern(string patternFilePath, Ignore? ignore = null, Mode mode = Mode.Default)
        {
            _patternFilePath = patternFilePath;
            _ignore = ignore;
            _mode = mode;
            if (File.Exists(patternFilePath))
            {
                var content = File.ReadAllText(patternFilePath);
                _savedPattern = JObject.Parse(content);
            }
        }

        public override Result Assert(HttpOperation operation)
        {
            // TODO: Add non-nullable annotations to OrFail() - and other contract methods
            var current = operation.Response.Content.ReadJson().OrFail("response");
            if (_savedPattern == null)
            {
                SaveNewPattern(current);
                return Ok;
            }

            JsonComparer patterns = new JsonComparer(_savedPattern, current, _ignore);

            if (patterns.AreEquivalent)
                return Ok;

            switch (_mode)
            {
                case Mode.ContractCheck:
                    // Always save new response (contract) when running in contract-check-mode
                    SaveNewPattern(current);
                    break;
                case Mode.Default:
                    if (operation.TestServer.Repair)
                    {
                        SaveNewPattern(current);
                        return Ok;
                    }

                    break;
                default:
                    throw Fail.BecauseEnumOutOfRange(_mode);
            }

            return Failure($"Response is different than expected. \nVerify the differences: \n\n{patterns.GetDifferences()}");
        }

        private void SaveNewPattern(JToken current)
        {
            _savedPattern = current;
            File.WriteAllText(_patternFilePath, current.ToString(Formatting.Indented));
        }

        public enum Mode
        {
            ContractCheck = 1,
            Default = 2
        }
    }
}