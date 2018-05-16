using System;
using System.Diagnostics;

public class ProcessClient
{
    Process[] processList;

    public ProcessClient()
	{
       
    }

    public Process[] ListAllApplications()
    {
        processList = Process.GetProcesses();

        // Loop through the array to show information of every process in your console
        foreach (Process process in processList)
        {
            Debug.Print(@"
        {0} | ID: {1} | Status {2} | Memory (private working set in Bytes) {3}",
                process.ProcessName, process.Id, process.Responding, process.PrivateMemorySize64);
        }
        return processList;
    }

    public void killProcess(String processName) {
        foreach (Process process in Process.GetProcessesByName(processName))
        {
            process.Kill();
        }
    }
}
