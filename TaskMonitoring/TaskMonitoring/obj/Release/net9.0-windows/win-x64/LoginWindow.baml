   M S B A M L   `   `   `  ����   TaskMonitoring     TaskMonitoring.LoginWindow   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Login��$	��670��$	��400�� � WindowStartupLocation$ CenterScreen=� � Icon$ ../assets/Icon.ico�����#00a4a0 � 	Resources    FlipToRegister �� 
TargetName �� TargetProperty W� From	 W� To g� Duration	 g� AutoReverse  FlipToLogin
 �� 
TargetType �� Property �� Value��%�  &	        &	 �     '��      'G� �    '�� p    '�� 4    '�� �    �� .��W� -   $ FlipGrid��$9 3(UIElement.RenderTransform).(ScaleTransform.ScaleX)� 1 0$ 0:0:0.2D�$
	 True���� .��W� -   $ FlipGrid��$9 3(UIElement.RenderTransform).(ScaleTransform.ScaleX)� 1 0$ 0:0:0.2D�$
	 True���� "
 ��.���  � ��$ 14(���  � ��$ 10q���  � ��$ 0,0,0,15q���  � ��
 �������  � �� �#B0B0B0��  � ��$ 1q���  � ��$ Center=��� "
 G�.���  � ��$ 14(���  � ��$ 10q���  � ��$ 0,0,0,15q���  � ��
 �������  � �� �#B0B0B0��  � ��$ 1q���  � ��$ Center=��� "
 ��.���  � ��$ 14(���  � ��$ 10q���  � ��$ 0,10,0,0q���  � �� �#4CAF50��  � ��
 �������  � �� �#4CAF50��  �@�Cursor$
 Hand���� "
 ��.���  � ��$ 13(���  � ��$ 0,5,0,3q���  � �� �#444��  � ��$
 Bold"��� "
 ��.���  � ��$ 14(���  � ��$ 10q���  � ��$ 0,0,0,15q���  � ��
 �������  � �� �#B0B0B0��  � ��$ 1q���  � �� �#333��  � ��$ Center=�.��� .+�� -    �Name$ FlipGrid��z���  �� ScaleX$ 1Q�.+�� -   $ 
LoginPanel��.+��� $��40q�.+��� $��შესვლა��$��22(�$
��Bold"�$��0,0,0,20q��� $+��%მომხმარებელი:���� -    ��Name$ UsernameTextBox���� $��პაროლი:��G� -    G�Name$ PasswordBox���� -   $��შესვლა���� -   $'��!რეგისტრაცია��
����  � � Cursor$
 Hand��$��0,20,0,0q�$��Center=�� -	   $ RegisterPanel��$y�	Collapsed=�.+��� $��40q�.+��� $'��!რეგისტრაცია��$��22(�$
��Bold"�$��0,0,0,20q��� $��სახელი:���� -
   $ FirstNameTextBox���� $��გვარი:���� -   $ LastNameTextBox���� $+��%მომხმარებელი:���� -   $ RegisterUsernameTextBox���� $��პაროლი:��G� -   $ RegisterPasswordBox���� $��როლი:���� -    ��Name$ RoleComboBox��.@��� $$��მოდერატორი���� $*��$თანამშრომელი���� -   $'��!რეგისტრაცია���� -   $%��უკან გასვლა��
����  �$
 Hand��$��0,20,0,0q�$��Center=�