   M S B A M L   `   `   `  ����A  =Task 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Task_2.ViewsTask_2.Views  5          Task_2.Views.Navigation   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:Task_2.Views     5      $	��685��5      $
��1280��5	        N� 
FontFamily$  Microsoft Yahei)�5
      .��5      �� -   5        ���#fff �� CornerRadius$ 0, 0, 15, 15��5   	   ._�5   
   � -   5         �Name$ myGrid��5       � RowDefinitions 5      �� 5      $��35 �6    �� 5      $	��650 �6    .+�5      �� -   5         ��Name$ 
rectTopBar��5      ���#333B495      �� 5       �� Orientation$ 
Horizontal=�5      $��Center=�5      .+�5      �  � Orientation$ 
Horizontal=�5       $��Center=�5!      .+�5#      �� -   5         ��Name$ imgDashboard��5$      $=��7pack://Application:,,,/Assets/Icons/dashboard_white.png��5%      $��20��5&      $��20��5'      $��
5, 5, 0, 0q�5(      �� 5*      -   5         ��Name$ tbDashboard��5+      $��	Dashboard��5,      ���#fff$��10, 5, 0, 0q�5.      	 � ForceCursor$
	 True��5/      
 � Cursor$

 Hand��50      53      55      � 57      $ 
Horizontal=�58      $��Center=�59      $��10, 0, 0, 0q�5:      .+�5<      �� -   5        $ 
imgModules��5=      $@��:pack://Application:,,,/Assets/Icons/View_Modules_white.png��5>      $��20��5?      $��20��5@      $��
5, 5, 0, 0q�5A      �� 5C      -   5        $ tbViewModules��5D      $��View Modules��5E      ���#fff$��10, 5, 0, 0q�5G      $
	 True��5H      $

 Hand��5I      5L      5N      � 5P      $ 
Horizontal=�5Q      $��Center=�5R      $��10, 0, 0, 0q�5S      .+�5U      �� -   5        $ imgAddModules��5V      $;��5pack://Application:,,,/Assets/Icons/modules_white.png��5W      $��20��5X      $��20��5Y      $��
5, 5, 0, 0q�5Z      �� 5\      -	   5        $ tbAddModule��5]      $��Add Modules��5^      ���#fff$��10, 5, 0, 0q�5`      $
	 True��5a      $

 Hand��5b      5e      5g      5i      � 5k      $ 
Horizontal=�5l      $��Right=�5m      $��0��5n      .+�5p      �� -
   5        $ imgUser��5q      $8��2pack://Application:,,,/Assets/Icons/user_white.png��5r      $��20��5s      $��20��5t      $��
5, 5, 0, 0q�5u      �� 5w      -   5         ��Name$ lblUser��5x      ���#fff$��10, 5, 0, 0q�5{      $
	 True��5|      $

 Hand��5}      ��5y      �� *Student_Email+5~      5�       � 5�      -   5          �Name$ frmView��5�        � NavigationUIVisibility$ Hidden=�5�      $��1��5�      5�      5�      5�      