   M S B A M L   `   `   `  ����   TaskMonitoring    TaskMonitoring.UserWindow   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Task Monitoring��$	��500��$
��1130�� � Icon$ ../assets/Icon.ico�����#00a4a0 � 	Resources    InsertTaskButtonStyle �� 
TargetType �� Property �� Value  ReloadTasksButtonStyle  CenteredCellTextStyle=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%  &	        &	     &	 2    '  �    �� " ��.���  � �� �#4CAF50��  � ��
 �������  � ��$ 14(���  � ��$
 Bold"���  � ��$
 10,5q���  � ��$ 0,0,10,0q���  � �� �#388E3C��  � ��$ 1q���  � ��$	 120����  � ��$ 40����  � ��$
 Left=��� " ��.���  � �� �#2196F3��  � ��
 �������  � ��$ 14(���  � ��$
 Bold"���  � ��$
 10,5q���  � ��$ 0,0,10,0q���  � �� �#1976D2��  � ��$ 1q���  � ��$	 120����  � ��$ 40����  � ��$
 Left=��� " ��.���  � ��$ Center=���  � ��$ Center=���  �@��TextAlignment$ Center=��� "  .���  � ��$ Center=���  � ��$ Center=���  � ��$
 Bold"�.��]� .+�� $	��Top=�$��10q� � ColumnDefinitions �� $��* ��� $
��Auto �.+���  �� Orientation$ 
Horizontal=�.+��� -   $��დამატება��#��[  �� -   $!��განახლება��#��[ �� $��1��$ 
Horizontal=�$��Right=�.+�d� -    d�Name$ UserToggleButton��$
��10,5q����#EEE���#CCC$��1q�$��10,0,0,0q����� *DisplayName+� $��Bottom=�	 � 	StaysOpen$	 False��
 � PlacementTarget
 ��  �� ElementName$ UserToggleButton������ *	IsChecked+$ UserToggleButton��.���� 
�������
�������$��1q� �� CornerRadius$ 4�� �� Padding$ 10q�._��� .+��� -    ��Name$ FullNameText��$
��Bold"��� -   $ UsernameText���� -   $ RoleText���� -   $��გასვლა��
���  ��
�������$��0,10,0,0q�&   System.Windows.Controls.DataGrid     AutoGenerateColumns$ False��$��10q�   
IsReadOnly$
 True��   AlternatingRowBackground
 �����   SelectionMode$ Single=����� *Tasks+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$$ ინიციატორი��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [    Binding �� *	Initiator+  $ პროექტი��$	 100 # [  �� *	Project+  $ შინაარსი��$	 200  �� *	Content+  $ დაწყება��$	 100 # [  �� *	StartDate+ �� StringFormat$ {0:yyyy-MM-dd}��  $$ დამთავრება��$	 150 # [  �� *	EndDate+$ {0:yyyy-MM-dd}��  $ სტატუსი��$
 Auto # [  �� *Status+  $! კომენტარი��$	 250  �� *	Comment+4  .System.Windows.Controls.DataGridTemplateColumn  $ ⚙️��$
 Auto    CellTemplate �� .R�� $��Stretch=�$��Center=�.+��� -   $	��...��$��25��$��25��$��Center=�$��Center=�