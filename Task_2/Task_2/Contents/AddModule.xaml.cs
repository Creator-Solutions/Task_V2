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
        //Class objects
        private Modules module;
        private ModuleController moduleController;
        private Calc calculations = new Calc();

        public AddModule()
        {
            //Initialize the modules
            module = new Modules();
            moduleController = new ModuleController();
            InitializeComponent();
        }

        /// <summary>
        /// Main Login To Add Module To Database
        /// async Task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Assign Values to Class Properties
                module.ModuleCode = txtModuleCode.Text;
                module.StudentNumber = Properties.Settings.Default.Student_Number;
                module.ModuleCredits = int.Parse(txtModuleCredits.Text);
                module.ModulesName = txtModuleName.Text;
                module.SemesterLength = int.Parse(txtMSemesterLength.Text);
                module.Semester_Start_Date = dpLocal.Part_Text.Text;
                module.ClassHours = int.Parse(txtWeeklyHours.Text);
                module.Self_Study_Hours = calculations.Calculate_Hours(module.ModuleCredits, module.SemesterLength, module.ClassHours);

                //Result of module exists to bool
                module.Module_Exists = await moduleController.Module_Exists(module.ModuleCode, module.StudentNumber);

                if (module.Module_Exists)
                {
                    _ = MessageBox.Show("Module Already exists", "Could Not Add Module", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                }
                else if (!module.Module_Exists)
                {
                    //Add Module To Database
                    await moduleController.Add_Module(module.StudentNumber, module.SemesterLength, module.Semester_Start_Date, module.ModuleCode, module.ModulesName, module.ModuleCredits, module.ClassHours, module.Self_Study_Hours);
                    
                    //Set Navigation To The Dashboard
                    Navigation nav = new Navigation();
                    nav.frmView.Source = new Uri("../Contents/Dashboard.xaml", UriKind.Relative);
                }

            }catch (Exception ex) 
            {
                if (ex is FormatException)
                {
                    _ = MessageBox.Show($"{ex.Message}", "Could Not Add Module", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                }
            }            
        }

        /// <summary>
        /// Update Module In Database
        /// With Time Already Studied
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnUpdateModule_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Class object
                var custom_hours = new CustomModuleHours();

                //Assign Fields to property values
                custom_hours.Module_Code = txtModCode.Text;
                custom_hours.Student_Number = Properties.Settings.Default.Student_Number;
                custom_hours.Hours_Studied = int.Parse(txtHoursStudied.Text);

                //Async Method Call
                module.Self_Study_Hours = await moduleController.Get_Studied_Hours(custom_hours.Module_Code, custom_hours.Student_Number);
                int remainder = moduleController.Subtract_Hours(custom_hours.Hours_Studied, module.Self_Study_Hours);

                //Call Async Method To Update Module
                await moduleController.Update_Module_Hours(custom_hours.Module_Code, custom_hours.Student_Number, remainder);

                //Set Navigation To The Dashboard
                Navigation nav = new Navigation();
                nav.frmView.Source = new Uri("../Contents/Dashboard.xaml", UriKind.Relative);

            }catch (Exception ex)
            {
                if (ex is FormatException)
                {
                    _ = MessageBox.Show($"{ex.Message}", "Could Not Add Module", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                }
            } 
      
        }
    }
}
