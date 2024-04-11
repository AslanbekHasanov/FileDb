//----------------------------------------
// Tarteeb School (c) All rights reserved
//----------------------------------------


namespace FileDB.Brokers.Loggings
{
    internal interface ILoggingBroker
    {
        void LogInforamation(string message);
        void LogError(string userMessage);
        void LogError(Exception exception);
    }
}
