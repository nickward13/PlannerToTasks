using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PlannerToTasksConsoleApp
{
    class Program
    {
        static Dictionary<string, string> PlanIdDictionary = new Dictionary<string, string>()
        {
            {"2Zar13z3k0Gsa92f9DOVJZUADBW5", "Advvy" },
            {"47DctZfDlUuzr2yajxNqC5UAFHf8", "Ultradata" },
            {"aKjZDZisQEu4iXCH2fqLmZUABe0M", "Search365" },
            {"bx-X-UrNSky2WnSS9jMV8ZUAE9RL", "GenderFitness" },
            {"CswPE9sd3kOXkRE0KPM5MpUAH53I", "Fred" },
            {"cuJId94N1E65oxuTIw8BCJUAHo36", "Civica Libraries" },
            {"dluM4o_8RUOjNkg74uJDrpUAGd7C", "Civica Local Gov Plan" },
            {"fsS7psoURUSfuK-9S_L6qJUAHp4e", "Civica Libraries" },
            {"iY1iSVQjxUOCDJW5telqupUAFHun", "??" },
            {"Kjy7ohNxMEut2yTQie8rRZUAB4Yl", "StarRez" },
            {"QvAktV-W9kaCcdjwZ0CkJJUAFX12", "Aveva" },
            {"r5-HjGBXU0uPl_27qIUEE5UAGNqK", "Portt" },
            {"S2domxAzK0i_arpqvYSdOJUAFhI6", "Janison" },
            {"S7UWZhmxwEeGY3nSjI4kS5UAECOJ", "Civica Libraries" },
            {"TJjmfoEOak2dHntZ6Gc1NpUAAwPx", "??" },
            {"U6WLoXbGSkeeqzqsiv1nn5UAF9q4", "Panviva" },
            {"y2Gp3I1leE-IrqYDRhYbW5UAHp4r", "The Yield" },
            {"Z829kl-D506QUhKql33eLZUACuFA", "Robobai" },
            {"ZxrJRK_aQU-_z3L5H6mODpUAFtuz", "Clear Dynamics" }
        };
        
        static string[] OldPlans = { "iY1iSVQjxUOCDJW5telqupUAFHun", "W9kaCcdjwZ0CkJJUAFX12", "TJjmfoEOak2dHntZ6Gc1NpUAAwPx", "ZxrJRK_aQU-_z3L5H6mODpUAFtuz" };
        
        static void Main(string[] args)
        {
            WebRequest request = WebRequest.Create("https://prod-03.australiaeast.logic.azure.com:443/workflows/75cfb09ac7ab4520b54d8d167f6a27d9/triggers/manual/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=CdePPDa0z3Ebx4s8QAlam8KDJ6os9NnaMidzczSRszc");
            request.Method = "POST";
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            Stream dataStream = response.GetResponseStream();
            String data;
            using (StreamReader reader = new StreamReader(dataStream))
            {
                data = reader.ReadToEnd();
            }
            TasksApiResponse taskApiResponse = JsonConvert.DeserializeObject<TasksApiResponse>(data);
            response.Close();
            
            bool oldPlan = false;
            List<PlannerTask> tasksInCurrentPlans = new List<PlannerTask>();
            foreach(PlannerTask plannerTask in taskApiResponse.value)
            {
                oldPlan = false;
                foreach(string oldPlanId in OldPlans)
                {
                    if (plannerTask.planId == oldPlanId)
                        oldPlan = true;
                }
                if(!oldPlan && plannerTask.percentComplete != 100)
                {
                    tasksInCurrentPlans.Add(plannerTask);
                }
            }
            
            foreach (PlannerTask plannerTask in tasksInCurrentPlans)
            {
                plannerTask.planName = PlanIdDictionary.GetValueOrDefault<string,string>(plannerTask.planId, "Unknown Plan");
            }

            tasksInCurrentPlans.Sort((x, y) => string.Compare(string.Concat(x.planName,x.title), string.Concat(y.planName,y.title)));

            foreach (PlannerTask plannerTask in tasksInCurrentPlans)
            {
                Console.WriteLine($"{plannerTask.planName}, {plannerTask.title}: https://tasks.office.com/microsoft.onmicrosoft.com/en-us/Home/Task/{plannerTask.id}");
            }

        }

    }
}
