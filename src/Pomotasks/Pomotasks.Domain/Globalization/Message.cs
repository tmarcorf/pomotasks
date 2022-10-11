using Pomotasks.Domain.Globalization.Resources;
using System.Resources;

namespace Pomotasks.Domain.Globalization
{
    public class Message
    {
        private static ResourceManager _resourceManager;

        public Message()
        {
        }

        public static string GetMessage(string key)
        {
            Configure();

            ValidateKey(key);

            return _resourceManager.GetString(key);
        }

        public static string GetMessage(string key, string[] componentMessages)
        {
            Configure();

            ValidateKey(key);

            string message = string.Empty;
            var messagesToIncrement = new List<string>();

            foreach (var componentMessage in componentMessages)
            {
                ValidateKey(componentMessage);

                messagesToIncrement.Add(_resourceManager.GetString(componentMessage));
            }

            message = string.Format(_resourceManager.GetString(key), messagesToIncrement.ToArray());

            return message;
        }

        private static void ValidateKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new Exception(_resourceManager.GetString("17"));
            }
        }

        private static void Configure()
        {
            _resourceManager = Messages.ResourceManager;
        }
    }
}
