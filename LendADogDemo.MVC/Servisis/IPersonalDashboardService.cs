using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LendADogDemo.MVC.ViewModels;

namespace LendADogDemo.MVC.Servisis
{
    public interface IPersonalDashboardService
    {
        PersonalDashboardViewModel GetMyPersonalDashboardModel(string userId);
    }
}
