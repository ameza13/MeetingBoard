   M S B A M L   `   `   `  ����   MeetingBoard    MeetingBoard.View.ToolBox   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     X TXceed.Wpf.Toolkit, Version=2.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  -     � Title$  Pen Settings��$	��150��$	��200�� � WindowStyle$
 none=� � WindowStartupLocation$ CenterOwner=� � Topmost .  � AllowsTransparency .  � 
ResizeMode$ NoResize=�
������ .���� -   
������� �� CornerRadius$ 5,5,5,5�� �� Padding$ 1q�
�������$��1q�|�.  ._�� -   $��MainGrid�� +� IsFocusScope . 	 � RowDefinitions	 �� $
��Auto ��� $
��Auto ��� $
��Auto �.+�� $��0��.+��� $�� Pen Settings��$��20��
�������$��Stretch=�
����  ��� $��1��$��Color��$��5, 0,5,0q�
����  �.��� -   $��	lineColor��$
��Left=�.+�#  Xceed.Wpf.Toolkit.ColorPicker  -   $��cboColor��$��50��$��3��
   DisplayColorAndName
 .  �� $��2��$��	Thickness��$��5,0,5,0q�
����  �.��� -   $��lineThickness��.+��� $��30��$��Center=�$��Center=�
���   �$��	10,0,10,0q�����  �� ElementName$ BrushSizeSlider�� �� Path$ Value��� -   $��18(�$��30��$��15��
�������.��-�� -   $��BrushSizeSlider��$��50�� � Minimum$ 1Q� � Maximum$ 8Q� �� IsSnapToTickEnabled .  �� TickFrequency$ 1Q� � Value$ 2Q�$��Center=��� -	   $��18(�$��30��$��15��
�������.��+