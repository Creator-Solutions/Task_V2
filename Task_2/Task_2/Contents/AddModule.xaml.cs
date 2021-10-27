using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Task_2.Scripts.Controllers;
using Task_2.Scripts.External;
using TimeStamp;

namespace Task_2.Views
{
    /// <summary>
    /// Interaction logic for AddModule.xaml
    /// </summary>
    public partial class AddModule : Page
    {

        private Modules module;
        private ModuleController moduleController;
        private Calc calculations = new Calc();

        public AddModule()
        {
            module = new Modules();
            moduleController = new ModuleController();
            InitializeComponent();
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            module.ModuleCode = txtModuleCode.Text;
            module.StudentNumber = Application.Current.Properties["Student_Number"].ToString();
            module.ModuleCredits = int.Parse(txtModuleCredits.Text);
            module.ModulesName = txtModuleName.Text;
            module.SemesterLength = int.Parse(txtMSemesterLength.Text);
            module.Semester_Start_Date = "1/3/2021";
            module.ClassHours = int.Parse(txtWeeklyHours.Text);
            module.Self_Study_Hours = calculations.Calculate_Hours(module.ModuleCredits, module.SemesterLength, module.ClassHours);

            module.Module_Exists = await moduleController.Module_Exists(module.ModuleCode, module.StudentNumber);

            if (module.Module_Exists)
            {
                _ = MessageBox.Show("Module Already exists", "Could Not Add Module", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
            else if (!module.Module_Exists)
            {
                await moduleController.Add_Module(module.StudentNumber, module.SemesterLength, module.Semester_Start_Date, module.ModuleCode, module.ModulesName, module.ModuleCredits, module.ClassHours, module.Self_Study_Hours);
                Navigation nav = new Navigation();
                nav.frmView.Source = new Uri("../Contents/Dashboard.xaml", UriKind.Relative);
            }
        }

        private async void btnUpdateModule_Click(object sender, RoutedEventArgs e)
        {
            //Class object
            var custom_hours = new CustomModuleHours();

            //Assign Fields to property values
            custom_hours.Module_Code = txtModCode.Text;
            custom_hours.Student_Number = Application.Current.Properties["Student_Number"].ToString();
            custom_hours.Hours_Studied = int.Parse(txtHoursStudied.Text);

            //Async Method Call
            module.Self_Study_Hours = await moduleController.Get_Studied_Hours(custom_hours.Module_Code, custom_hours.Student_Number);
            int remainder = moduleController.Subtract_Hours(custom_hours.Hours_Studied, module.Self_Study_Hours);

            await moduleController.Update_Module_Hours(custom_hours.Module_Code, custom_hours.Student_Number, remainder);

            Navigation nav = new Navigation();
            nav.frmView.Source = new Uri("../Contents/Dashboard.xaml", UriKind.Relative);
        }
    }
}
