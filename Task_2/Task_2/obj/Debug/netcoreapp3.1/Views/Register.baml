   M S B A M L   `   `   `  ����A  =Task 2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Task_2.ViewsTask_2.Views  5          Task_2.Views.Register   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:Task_2.Views     5      _ [MaterialDesignThemes.Wpf, Version=4.2.1.2, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      $	��685��5	      $
��1280��5
      .��5      �� ���#252B35._�5   
   �   � ColumnDefinitions  5      �� 5      $	��550 �6#   �� 5      $	��730 �6#   .+�5      �� -   5         ��Name$ 
imgRegIcon��5      $6��0pack://Application:,,,/Assets/Icons/studying.png��5      $	��150��5      $	��150��5      $	��Top=�5      $��0, 90, 0, 0q�5      �� 5       �� Orientation$ Vertical=�5      $��Center=�5      $��Center=�5       $	��450��5!      $	��200��5"      .+�5$      �� -   5         ��Name$ lblTitle��5%      $��Welcome��5&      ���#fff$��32(�5(      $��Center=�5)      $	��Top=�5*      �� 5,      $
��Wrap=�5-      ���#fff$��Center=�5/      $��Center=�50      $��18(�51      $��0, 25, 0, 0q�52      $��Microsoft Yahei UI)�53      $����Welcome to Student Study Course Manager. To get started please create an account and start managing your course study information with a few simple clicks.��5:   &   5;      5<      � 5>       � Orientation$ 
Horizontal=�5?      $��Center=�5@      $��0, 225, 0, 0q�5A      $��35��5B      $	��500��5C      .+�5E      �� ���#fff$	��168��5G      $��2��5H      $��
0, 4, 0, 0q�5I      �� 5K      $��Connect With Us��5L      ���#fff$	��Top=�5N      $��Center=�5O      $��20(�5P      $��
5, 0, 5, 0q�5Q      �� 5S      ���#fff$	��168��5U      $��2��5V      $��
0, 4, 0, 0q�5W      5Y      �� 5[      $ Vertical=�5\      $	��500��5]      $	��100��5^      $��0, 350, 0, 0q�5_      $��0��5`      .+�5b      �� $��Already Have An Account?��5c      
�������$��18(�5e      $��Center=�5f      $��Center=�5g      �� 5i      $	��150��5j      $��40��5k      $��2q�5l      ���#fff �� CornerRadius$ 25��5n      $��
0, 5, 0, 0q�5o      ._�5q      �� -   5         ��Name$ TbLogin��5r      $��Login��5s      $��24(�5t      ���#fff$��Center=�5v       � ForceCursor$
 True��5w       � Cursor$
 Hand��5x      5{      5|      5~      �� 5�      $ Vertical=�5�      $��1��5�      $��Center=�5�      $��Center=�5�      $	��500��5�      $	��670��5�      .+�5�      �� -   5        $ 
lblCaption��5�      $��Register��5�      $��23(�5�      ���#fff$
��Left=�5�      $��25, 0, 0, 0q�5�      �� 5�      -   5        $ lblFullName��5�      $��	FULL NAME��5�      $��16(�5�      $��0, 55, 0, 0q�5�      ���	#FFF9EEEE6*   �� 5�      -   5        	 ��Name$	 txtFullName��5�      )  #MaterialDesignThemes.Wpf.HintAssist
   Hint$
 Enter Your Full Name��5�      $��16(�5�      $��
0, 5, 0, 0q�5�      $��
5, 5, 5, 5q�5�      ���	#FFF9EEEE6*   �� 5�      -   5        $ lblStudentNumber��5�      $��STUDENT NUMBER��5�      $��16(�5�      $��0, 25, 0, 0q�5�      ���	#FFF9EEEE6*   �� 5�      -   5        $	 txtStudentNumber��5�      $
 Enter Your Student Number��5�      $��16(�5�      $��
0, 5, 0, 0q�5�      $��
5, 5, 5, 5q�5�      ���	#FFF9EEEE6*   �� 5�      -	   5        $ lblStudentEmail��5�      $��STUDENT EMAIL��5�      $��16(�5�      $��0, 25, 0, 0q�5�      ���	#FFF9EEEE6*   �� 5�      -
   5        $	 txtStudentEmail��5�      $
 Enter Your Student Email��5�      $��16(�5�      $��
0, 5, 0, 0q�5�      $��
5, 5, 5, 5q�5�      ���	#FFF9EEEE6*   �� 5�      -   5        $ lblStudentPassword��5�      $��STUDENT PASSWORD��5�      $��16(�5�      $��0, 25, 0, 0q�5�      ���	#FFF9EEEE6*   G� 5�      -   5         G�Name$ txtStudentPassword��5�      $
 
**********��5�      $��16(�5�      $��
0, 5, 0, 0q�5�      $��
5, 5, 5, 5q�5�      ���	#FFF9EEEE6*   �� 5�      -   5        $ lblConfirmPassword��5�      $��CONFIRM PASSWORD��5�      $��16(�5�      $��0, 25, 0, 0q�5�      ���	#FFF9EEEE6*   G� 5�      -   5        $ txtConfirmPassword��5�      $
 ***********��5�      $��16(�5�      $��
0, 5, 0, 0q�5�      $��
5, 5, 5, 5q�5�      ���	#FFF9EEEE6*   �� 5�      $	��150��5�      $��45��5�      $��2q�5�      ���#4169e1$ 25��5�      $��0, 25, 0, 0q�5�      ._�5�      �� -   5        $ 
TbRegister��5�      $��Register��5�      $��26(�5�      ���#fff$��Center=�5�      $
 True��5�      $
 Hand��5�      5�      5�      5�      5�      5�      5�      