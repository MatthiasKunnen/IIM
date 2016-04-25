using System;

namespace IIM.App_Start
{
    public interface ISettingsFile
    {
        void WriteToFile(string data);
        string ReadFile();
    }
}
