using Pomotasks.Domain.Globalization.Resources;

namespace Pomotasks.Domain.Globalization
{
    public static class Message
    {
        public static string GetMessage(string key)
        {
            if (IsValid(key))
            {
                return Messages.ResourceManager.GetString(key);
            }

            return string.Empty;
        }

        public static string GetMessage(string key, string[] componentMessages)
        {
            if (!componentMessages.Any())
            {
                return string.Empty;
            }

            if (IsValid(key))
            {
                var messagesToIncrement = new List<string>();

                messagesToIncrement.AddRange(from componentMessage in componentMessages
                                             where IsValid(componentMessage)
                                             select Messages.ResourceManager.GetString(componentMessage));


                string message = string.Format(Messages.ResourceManager.GetString(key), messagesToIncrement);

                return message;
            }

            return string.Empty;
        }

        private static bool IsValid(string key)
        {
            return !string.IsNullOrEmpty(key);
        }
    }
}
