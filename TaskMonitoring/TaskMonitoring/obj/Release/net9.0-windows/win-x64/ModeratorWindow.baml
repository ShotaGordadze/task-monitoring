   M S B A M L   `   `   `  ���� I  ETaskMonitoring, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:TaskMonitoringTaskMonitoring  $    TaskMonitoring.ModeratorWindow   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .localclr-namespace:TaskMonitoring       � Title$  Task Monitoring��$	��450��$	��930�� � Icon$ ../assets/Icon.ico�����#00a4a0 � 	Resources    ReloadTasksButtonStyle �� 
TargetType �� Property �� Value  UserFilterComboBoxStyle  CenteredCellTextStyle=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%3  &	        &	     &	 I    '  �    �� " ��.���  � �� �#2196F3��  � ��
 �������  � ��$ 14(���  � ��$
 Bold"���  � ��$
 10,5q���  � ��$ 0,0,10,0q���  � �� �#1976D2��  � ��$ 1q���  � ��$	 120����  � ��$ 40����  � ��$
 Left=��� " ��.���  � ��
 �������  � ��
 �   ���  � ��$ 14(���  � ��$ Normal"���  � ��$	 5,2q���  � ��$ 0,0,10,0q���  � �� �#1976D2��  � ��$ 1q���  � ��$	 150����  � ��$ 40����  � ��$
 Left=���  � ��$ Center=��� " ��.���  � ��$ Center=���  � ��$ Center=���  �@��TextAlignment$ Center=��� "  .���  � ��$ Center=���  � ��$ Center=���  � ��$
 Bold"�.��]� .+�� $	��Top=�$��10q� � ColumnDefinitions �� $��* ��� $
��Auto �.+���  �� Orientation$ 
Horizontal=�.+��� -   $!��განახლება��#��[  �� -    ��Name$ UserFilterComboBox��#��[ 	 �� DisplayMemberPath$	 DisplayName��
 �� SelectedValuePath$
 Username��$��Center=��� $��1��$ 
Horizontal=�$��Right=�.+�d� -    d�Name$ UserToggleButton��$
��10,5q����#EEE���#CCC$��1q�$��10,0,0,0q����� *DisplayName+� $��Bottom=� � 	StaysOpen$ False�� � PlacementTarget ��  �� ElementName$ UserToggleButton������ *	IsChecked+$ UserToggleButton��.���� 
�������
�������$��1q� �� CornerRadius$ 4�� �� Padding$ 10q�._��� .+��� -    ��Name$ FullNameText��$
��Bold"��� -   $ UsernameText���� -   $ RoleText���� -   $��გასვლა��
���  ��
�������$��0,10,0,0q�&   System.Windows.Controls.DataGrid  -     Name$ TasksDataGrid��   AutoGenerateColumns$ False��$��10q�   
IsReadOnly$
 True��   AlternatingRowBackground
 �����   SelectionMode$ Single=����� *Tasks+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$$ ინიციატორი��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [    Binding �� *	Initiator+  $ პროექტი��$	 100 # [  �� *	Project+4  .System.Windows.Controls.DataGridTemplateColumn  $ შინაარსი��$ 2*    CellTemplate �� .R��� $
��Wrap=� �� Padding$ 5q�$��Center=����� *	Content+  $ დაწყება��$	 100 # [  �� *	StartDate+ �� StringFormat$ {0:yyyy-MM-dd}��  $$ დამთავრება��$	 100 # [  �� *	EndDate+$ {0:yyyy-MM-dd}��  $ სტატუსი��$ *  �� .R��� $
��Wrap=�$ 5q�$��Center=����� *Status+  $! კომენტარი��$	 150 # [  �� *	Comment+