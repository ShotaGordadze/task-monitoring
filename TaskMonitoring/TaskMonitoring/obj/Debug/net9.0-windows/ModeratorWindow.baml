   M S B A M L   `   `   `  ����I  ETaskMonitoring, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:TaskMonitoringTaskMonitoring  5   	   $    TaskMonitoring.ModeratorWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   .localclr-namespace:TaskMonitoring     5   	     � Title$  Task Monitoring��5   	   $	��450��6!   $	��930��6.    � Icon$ ../assets/Icon.ico��5	   	   ���#00a4a0 � 	Resources 5         ReloadTasksButtonStyle �� 
TargetType �� Property �� Value  UserFilterComboBoxStyle  CenteredCellTextStyle=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%%  &	        &	 �    &	 �    '  c    �� 5   
   " ��6/   .�5      ��  � �� �#2196F36+   �� 5       � ��
 �����6+   �� 5       � ��$ 14(�6)   �� 5       � ��$
 Bold"�6+   �� 5       � ��$
 10,5q�6(   �� 5       � ��$ 0,0,10,0q�6'   �� 5       � �� �#1976D26,   �� 5       � ��$ 1q�60   �� 5       � ��$	 120��6&   �� 5       � ��$ 40��6'   �� 5       � ��$
 Left=�64   5      �� 5   
   " ��60   .�5      ��  � ��
 �����6+   �� 5        � ��
 �   �6+   �� 5!       � ��$ 14(�6)   �� 5"       � ��$ Normal"�6+   �� 5#       � ��$	 5,2q�6(   �� 5$       � ��$ 0,0,10,0q�6'   �� 5%       � �� �#1976D26,   �� 5&       � ��$ 1q�60   �� 5'       � ��$	 150��6&   �� 5(       � ��$ 40��6'   �� 5)       � ��$
 Left=�64   �� 5*       � ��$ Center=�69   5+      �� 5-   
   " ��6.   .�5.      ��  � ��$ Center=�62   �� 5/       � ��$ Center=�64   �� 50       �@��TextAlignment$ Center=�6.   51      �� 53   
   "  6   .�54      ��  � ��$ Center=�6;   �� 55       � ��$ Center=�69   �� 56       � ��$
 Bold"�6+   57      58      .��5:      ]� .+�5<   
   � $	��Top=�6   $��10q�6$    � ColumnDefinitions 5=      �� 5>      $��* �6#   �� 5?      $
��Auto �6#   .+�5C      ��  �� Orientation$ 
Horizontal=�6   .+�5D      �� -   5        $!��განახლება��5D      #��[  5E      �� 5G      -   5         ��Name$ UserFilterComboBox��5G      #��[ 5H      	 �� DisplayMemberPath$	 DisplayName��5I      
 �� SelectedValuePath$
 Username��5J      $��Center=�5L      5M      �� 5P      $��1��6   $ 
Horizontal=�6)   $��Right=�6B   .+�5Q      d� -   5         d�Name$ UserToggleButton��5Q      $
��10,5q�5R      ���#EEE���#CCC$��1q�5S      $��10,0,0,0q�63   ��5Q   9   �� *DisplayName+5S   3   � 5T      $��Bottom=�5U       � 	StaysOpen$ False��5V       � PlacementTarget 5T      ��  �� ElementName$ UserToggleButton����5U   +   �� *	IsChecked+$ UserToggleButton��.��5W      �� 
�������
�������$��1q�6C    �� CornerRadius$ 4��6W    �� Padding$ 10q�6h   ._�5X      �� .+�5Y      �� -   5         ��Name$ FullNameText��5Y   (   $
��Bold"�6>   �� 5Z      -   5        $ UsernameText��5Z   (   �� 5[      -   5        $ RoleText��5[   (   �� 5\      -   5        $��გასვლა��5\   %   
���  ��
�������$��0,10,0,0q�5]   I   5^      5_      5`      5a      5b      &   System.Windows.Controls.DataGrid  5e   
   -   5          Name$ TasksDataGrid��5e         AutoGenerateColumns$ False��5f   1   $��10q�6M      
IsReadOnly$
 True��5g         AlternatingRowBackground
 �����   SelectionMode$ Single=�5h      ��5f      �� *Tasks+   Columns 5i      0  *System.Windows.Controls.DataGridTextColumn  5j      ,  &System.Windows.Controls.DataGridColumn   Header$$ ინიციატორი��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6W   1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [ 5k   %      Binding 5j   9   �� *	Initiator+5k   %     5l      $ პროექტი��6%   $	 100 6R   # [ 5m   %    5l   6   �� *	Project+5m   %   4  .System.Windows.Controls.DataGridTemplateColumn  5p      $ შინაარსი��6)   $ 2* 6;      CellTemplate 5q      �� 5r      .R�5s      �� $
��Wrap=�6A    �� Padding$ 5q�6U   $��Center=�5t   (   ��5s   (   �� *	Content+5t   (   5u      5w        5y      $ დაწყება��6%   $	 100 5z   %   # [ 61    5y   6   �� *	StartDate+ �� StringFormat$ {0:yyyy-MM-dd}��5z   1     5{      $$ დამთავრება��6%   $	 100 5|   %   # [ 61    5{   9   �� *	EndDate+$ {0:yyyy-MM-dd}��5|   1     5�      $ სტატუსი��6)   $ * 6:    5�      �� 5�      .R�5�      �� $
��Wrap=�6@   $ 5q�6T   $��Center=�5�   (   ��5�   (   �� *Status+5�   (   5�      5�        5�      $! კომენტარი��6%   $	 150 6T   # [ 5�   %    5�   8   �� *	Comment+5�   %   5�      5�      5�      